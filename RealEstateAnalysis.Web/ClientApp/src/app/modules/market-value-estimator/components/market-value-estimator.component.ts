import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { UntypedFormBuilder, UntypedFormGroup, Validators, NgForm, UntypedFormControl, FormGroupDirective } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReadLookup } from 'src/app/shared/dtos/reads/readLookup';
import { ReadValidationResult } from 'src/app/shared/dtos/reads/readValidationResult';
import { ReadProperty } from '../../rental-properties/dtos/reads/readProperty';
import { RentalPropertyService } from '../../rental-properties/services/rental-property.service';
import { MessagingService } from 'src/app/shared/services/messaging.service';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { appConfig } from 'src/app/app.config';
import { AddEditPropertyComponent } from '../../rental-properties/modals/add-edit-property/add-edit-property.component';
import { MessageType } from 'src/app/shared/enums/messageType';
import { ConfirmComponent } from 'src/app/shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from 'src/app/shared/dtos/confirmModalContentDto';
import { IncomeStatementComponent } from '../../rental-properties/modals/income-statement/income-statement.component';

@Component({
    templateUrl: './market-value-estimator.component.html',
    styleUrls: ['./market-value-estimator.component.css']
})
export class MarketValueEstimatorComponent implements OnInit, OnDestroy {
    public isRunningReport = false;
    public subjectPropertiesLookups: ReadLookup[];
    public groupNames: string[];
    public marketValueForm: UntypedFormGroup;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public comparableProperties: ReadProperty[];
    public subjectProperty: ReadProperty;

    private alive = true;
    private selectedProperty: ReadProperty;

    constructor(titleService: Title,
        private router: Router,
        private propertyService: RentalPropertyService,
        private formBuilder: UntypedFormBuilder,
        private messagingService: MessagingService,
        private modalService: ModalService
    ) {
        titleService.setTitle(`${appConfig.businessName}: Market Value`);
        this.createMarketValueForm();
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.setSubjectPropertiesLookups();
        this.setGroupNames();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onMarketValueFormSubmit(marketValueNgForm: FormGroupDirective): void {
        if (marketValueNgForm.invalid) {
            return;
        }

        this.isRunningReport = true;

        this.propertyService.getByGroupName(this.groupName.value)
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => {
                this.comparableProperties = response;
                this.isRunningReport = false;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.isRunningReport = false;
                });

        const subjectPropertyId = +this.subjectPropertyId.value;

        if (subjectPropertyId !== 0) {
            this.propertyService.getProperty(subjectPropertyId)
                .pipe(takeWhile(() => this.alive))
                .subscribe(response => this.subjectProperty = response,
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }
                    });
        } else {
            this.subjectProperty = null;
        }
    }

    public getRatio(numerator: number, denominator: number): number {
        if (this.nullUndefinedOrZero(numerator) || this.nullUndefinedOrZero(denominator)) {
            return 0;
        } else {
            return +(numerator / denominator).toFixed(2);
        }
    }

    public getTotalActiveUnitsSqFts(property: ReadProperty): number {
        const vacancyLossSqFts = property.totalUnitsSquareFootage * (property.annualVacancyRate / 100);
        const totalActiveUnitsSqFts = property.totalUnitsSquareFootage - vacancyLossSqFts;
        return totalActiveUnitsSqFts;
    }

    private nullUndefinedOrZero(value: number | undefined | null): boolean {
        return value === undefined || value === null || value === 0;
    }

    public getMinimumSalesPrice(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
        return Math.min(...values);
    }

    public getMaximumSalesPrice(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
        return Math.max(...values);
    }

    public getMinimumNumberOfUnits(): number {
        const values = this.comparableProperties.map(x => x.totalUnits);
        return Math.min(...values);
    }

    public getMaximumNumberOfUnits(): number {
        const values = this.comparableProperties.map(x => x.totalUnits);
        return Math.max(...values);
    }

    public getMinimumPricePerUnit(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
        return Math.min(...values);
    }

    public getMaximumPricePerUnit(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
        return Math.max(...values);
    }

    public getMinimumAverageUnitSize(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
        return Math.min(...values);
    }

    public getMaximumAverageUnitSize(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
        return Math.max(...values);
    }

    public getMinimumPricePerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
        return Math.min(...values);
    }

    public getMaximumPricePerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
        return Math.max(...values);
    }

    public getMinimumYearBuilt(): number {
        const values = this.comparableProperties.map(x => x.yearBuiltIn);
        return Math.min(...values);
    }

    public getMaximumYearBuilt(): number {
        const values = this.comparableProperties.map(x => x.yearBuiltIn);
        return Math.max(...values);
    }

    public getMinimumRentalIncomePerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
        return Math.min(...values);
    }

    public getMaximumRentalIncomePerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
        return Math.max(...values);
    }

    public getMinimumOperatingExpensesPerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, this.getTotalActiveUnitsSqFts(x)));
        return Math.min(...values);
    }

    public getMaximumOperatingExpensesPerUnitSqFt(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, this.getTotalActiveUnitsSqFts(x)));
        return Math.max(...values);
    }

    public getMinimumGRM(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
        return Math.min(...values);
    }

    public getMaximumGRM(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
        return Math.max(...values);
    }

    public getGRMMedian(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
        return this.getMedian(values);
    }

    public getGRMAverage(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
        return this.getAverage(values);
    }

    public getMinimumCapRate(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
        return Math.min(...values);
    }

    public getMaximumCapRate(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
        return Math.max(...values);
    }

    public getCapRatesMedian(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
        return this.getMedian(values);
    }

    public getCapRatesAverage(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
        return this.getAverage(values);
    }

    public getPricePerUnitSqFtAverage(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
        return this.getAverage(values);
    }

    public getRentalIncomePerUnitSqFtAverage(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
        return this.getAverage(values);
    }

    public getOperatingExpensesPerUnitSqFtAverage(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, this.getTotalActiveUnitsSqFts(x)));
        return this.getAverage(values);
    }

    public getSalesPriceMedian(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
        return this.getMedian(values);
    }

    public getSalesPriceAverage(): number {
        const values = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
        return this.getAverage(values);
    }

    public getNumberOfUnitsMedian(): number {
        const values = this.comparableProperties.map(x => x.totalUnits);
        return this.getMedian(values);
    }

    public getNumberOfUnitsAverage(): number {
        const values = this.comparableProperties.map(x => x.totalUnits);
        return this.getAverage(values);
    }

    public getPricePerUnitMedian(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
        return this.getMedian(values);
    }

    public getPricePerUnitAverage(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
        return this.getAverage(values);
    }

    public getAverageUnitSizeMedian(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
        return this.getMedian(values);
    }

    public getYearBuiltAverage(): number {
        const values = this.comparableProperties.map(x => x.yearBuiltIn);
        return this.getAverage(values);
    }

    public getYearBuiltMedian(): number {
        const values = this.comparableProperties.map(x => x.yearBuiltIn);
        return this.getMedian(values);
    }

    public getAverageUnitSizeAverage(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
        return this.getAverage(values);
    }

    public getPricePerUnitSqFtMedian(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
        return this.getMedian(values);
    }

    public getRentalIncomePerUnitSqFtMedian(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
        return this.getMedian(values);
    }

    public getOperatingExpensesPerUnitSqFtMedian(): number {
        const values = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, this.getTotalActiveUnitsSqFts(x)));
        return this.getMedian(values);
    }

    private getAverage(values: number[]): number {
        const sum = values.reduce((previous, current) => current += previous) as number;
        const avg = sum / this.comparableProperties.length;

        return +avg.toFixed(2);
    }

    public getMedian(values: number[]): number {
        values.sort((a, b) => a - b);
        const lowMiddle = Math.floor((values.length - 1) / 2);
        const highMiddle = Math.ceil((values.length - 1) / 2);
        const median = (values[lowMiddle] + values[highMiddle]) / 2;

        return +median.toFixed(2);
    }

    public onAddNewClick(): void {
        const addModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        addModal.groupNameIsRequired = true;
        addModal.messageType = MessageType.marketValueNewPropertyAdded;
    }

    public onEditClick(property: ReadProperty): void {
        const editModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        editModal.setEditPropertyFormValues(property);
        editModal.groupNameIsRequired = true;
        editModal.messageType = MessageType.marketValuePropertyUpdated;
    }

    public onEditSubjectPropertyClick(subjectProperty: ReadProperty): void {
        const editModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        editModal.setEditPropertyFormValues(subjectProperty);
        editModal.messageType = MessageType.marketValuePropertyUpdated;
    }

    public onDeleteClick(property: ReadProperty): void {
        this.selectedProperty = property;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${property.address.address} property?`, MessageType.marketValueDeleteProperty);
    }

    public onIncomeStatementClick(property: ReadProperty): void {
        const incomeStatementModal = this.modalService.show(IncomeStatementComponent, { sizeClass: 'modal-lg' });
        incomeStatementModal.initialize(property);
    }

    public onDetailsClick(property: ReadProperty): void {
        this.router.navigate(['/rental-property-details', property.id]);
    }

    private setGroupNames(): void {
        this.propertyService.getAllGroupNames()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.groupNames = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setSubjectPropertiesLookups(): void {
        this.propertyService.getAllSubjectPropertyLookups()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.subjectPropertiesLookups = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private createMarketValueForm(): void {
        this.marketValueForm = this.formBuilder.group({
            groupName: ['', [Validators.required]],
            subjectPropertyId: ['']
        });
    }

    private addProperty(newProperty: ReadProperty): void {
        if (newProperty.groupName.toLowerCase().trim() === (this.groupName.value as string).toLowerCase()) {
            this.comparableProperties.push(newProperty);
        }
    }

    private updateProperty(updatedProperty: ReadProperty): void {
        if (this.subjectProperty && this.subjectProperty.id === updatedProperty.id) {
            this.subjectProperty = updatedProperty;
        } else {
            const itemIndex = this.comparableProperties.findIndex(x => x.id === updatedProperty.id);
            this.comparableProperties.splice(itemIndex, 1);
        }

        if (updatedProperty.groupName.toLowerCase().trim() === (this.groupName.value as string).toLowerCase()) {
            this.comparableProperties.push(updatedProperty);
        }
    }

    private deleteProperty() {
        this.propertyService.deleteProperty(this.selectedProperty.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.comparableProperties.findIndex(x => x.id === this.selectedProperty.id);
                this.comparableProperties.splice(itemIndex, 1);
                this.setGroupNames();
                this.setSubjectPropertiesLookups();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.marketValueNewPropertyAdded) {
                    this.addProperty(message.content as ReadProperty);
                    this.setGroupNames();
                }

                if (message.messageType === MessageType.marketValueDeleteProperty) {
                    this.deleteProperty();
                }

                if (message.messageType === MessageType.marketValuePropertyUpdated) {
                    this.updateProperty(message.content as ReadProperty);
                    this.setGroupNames();
                }
            });
    }

    get groupName(): UntypedFormControl {
        return this.marketValueForm.get('groupName') as UntypedFormControl;
    }

    get subjectPropertyId(): UntypedFormControl {
        return this.marketValueForm.get('subjectPropertyId') as UntypedFormControl;
    }
}
