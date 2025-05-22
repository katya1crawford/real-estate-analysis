import { Component, OnInit, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormArray, UntypedFormGroup, Validators, AbstractControl, NgForm, FormGroupDirective } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadLookup } from '../../../../shared/dtos/reads/readLookup';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { ReadFinancialSummary } from '../../dtos/reads/readFinancialSummary';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { LookupService } from '../../../../shared/services/lookup.service';
import { RentalPropertyService } from '../../services/rental-property.service';
import { CalculatorService } from '../../services/calculator.service';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { WriteAddress } from '../../../../shared/dtos/writes/writeAddress';
import { WriteProperty } from '../../dtos/writes/writeProperty';
import { MessageDto } from '../../../../shared/dtos/messageDto';
import { MessageType } from '../../../../shared/enums/messageType';
import { WriteFinancialSummary } from '../../dtos/writes/writeFinancialSummary';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { WriteAnnualOperatingExpense } from '../../dtos/writes/writeAnnualOperatingExpense';
import { WriteClosingCost } from '../../../../shared/dtos/writes/writeClosingCost';
import { WriteExteriorRepairExpense } from '../../../../shared/dtos/writes/writeExteriorRepairExpense';
import { WriteGeneralRepairExpense } from '../../../../shared/dtos/writes/writeGeneralRepairExpense';
import { WriteInteriorRepairExpense } from '../../../../shared/dtos/writes/writeInteriorRepairExpense';
import { thumbnailImageValidator, loanValidator } from '../../../../shared/validators/shared.validators';
import { WriteUnitGroup } from '../../../../shared/dtos/writes/writeUnitGroup';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    styleUrls: ['./add-edit-property.component.css'],
    templateUrl: './add-edit-property.component.html'
})
export class AddEditPropertyComponent implements OnInit, OnDestroy {
    public addEditPropertyForm: UntypedFormGroup;
    public states: ReadLookup[];
    public operatingExpenseTypes: ReadLookup[];
    public closingCostTypes: ReadLookup[];
    public interiorRepairExpenseTypes: ReadLookup[];
    public exteriorRepairExpenseTypes: ReadLookup[];
    public generalRepairExpenseTypes: ReadLookup[];
    public propertyTypes: ReadLookup[];
    public propertyStatuses: ReadLookup[];
    public validationErrorResult: ReadValidationResult | null = null;
    public refreshRatesValidationErrorResult: ReadValidationResult | null = null;
    public refreshRatesServerError = false;
    public serverError = false;
    public financialSummary: ReadFinancialSummary;
    public isAddingEditingProperty = false;
    public isCalculatingRates = false;
    public modalTitle = 'Add New Property';
    public pageLoading = false;
    public hasThumbnailImage = false;
    public selectedThumbnailImage: string | null;
    public annualPropertyManagementFee = 0;
    public annualVacancyLoss = 0;
    public groupNameIsRequired = false;
    public messageType: MessageType;

    private alive = true;
    private propertyId = 0;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder,
        private lookupService: LookupService,
        private propertyService: RentalPropertyService,
        private calculatorService: CalculatorService) {
        this.subscribeToMessagingService();
        this.createAddEditPropertyForm();
        this.financialSummary = new ReadFinancialSummary();
    }

    ngOnInit(): void {
        this.setStates();
        this.setOperatingExpenseTypes();
        this.setPropertyTypes();
        this.setPropertyStatuses();
        this.setClosingCostTypes();
        this.setFormControlsObservables();
        this.setInteriorRepairExpenseTypes();
        this.setExteriorRepairExpenseTypes();
        this.setGeneralRepairExpenseTypes();
        this.setGroupNameFormControlValidators();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public setEditPropertyFormValues(property: ReadProperty): void {
        this.modalTitle = 'Edit Property';
        this.propertyId = property.id;
        this.pageLoading = true;
        this.hasThumbnailImage = property.thumbnailImageBase64.length > 0;
        this.address.patchValue(property.address.address);
        this.city.patchValue(property.address.city);
        this.stateId.patchValue(property.address.state.id);
        this.zipCode.patchValue(property.address.zipCode);
        this.propertyTypeId.patchValue(property.propertyType.id);
        this.propertyStatusId.patchValue(property.propertyStatus.id);
        this.lotSquareFootage.patchValue(property.lotSquareFootage);
        this.yearBuiltIn.patchValue(property.yearBuiltIn);
        this.buildingSquareFootage.patchValue(property.buildingSquareFootage);
        this.purchasePrice.patchValue(property.purchasePrice);
        this.downPayment.patchValue(property.downPayment);
        this.annualGrossScheduledRentalIncome.patchValue(property.annualGrossScheduledRentalIncome);
        this.otherAnnualIncome.patchValue(property.otherAnnualIncome);
        this.annualVacancyRate.patchValue(property.annualVacancyRate);
        this.annualPropertyManagementFeeRate.patchValue(property.annualPropertyManagementFeeRate);
        this.loanApr.patchValue(property.loanApr);
        this.loanYears.patchValue(property.loanYears);
        this.annualGrossScheduledRentalIncomeGrowthRate.patchValue(property.annualGrossScheduledRentalIncomeGrowthRate);
        this.annualOperatingExpensesGrowthRate.patchValue(property.annualOperatingExpensesGrowthRate);
        this.marketCapitalizationRate.patchValue(property.marketCapitalizationRate);
        this.notes.patchValue(property.notes);
        this.groupName.patchValue(property.groupName);

        const unitGroupFrmArray: UntypedFormGroup[] = [];

        if (property.unitGroups !== null) {
            for (let i = 0; i < property.unitGroups.length; i++) {
                unitGroupFrmArray.push(this.buildUnitGroupFormGroup(property.unitGroups[i].id,
                    property.unitGroups[i].bathrooms,
                    property.unitGroups[i].bedrooms,
                    property.unitGroups[i].numberOfUnits,
                    property.unitGroups[i].squareFootage));
            }
        }

        if (unitGroupFrmArray.length === 0) {
            unitGroupFrmArray.push(this.buildEmptyUnitGroupFormGroup());
        }

        this.addEditPropertyForm.setControl('unitGroups', this.formBuilder.array(unitGroupFrmArray));

        const operatingExpensesFrmArray: UntypedFormGroup[] = [];

        if (property.annualOperatingExpenses !== null) {
            for (let i = 0; i < property.annualOperatingExpenses.length; i++) {
                operatingExpensesFrmArray.push(this.buildAnnualOperatingExpenseFormGroup(property.annualOperatingExpenses[i].operatingExpenseTypeId,
                    property.annualOperatingExpenses[i].amount));
            }
        }

        if (operatingExpensesFrmArray.length === 0) {
            operatingExpensesFrmArray.push(this.buildEmptyAnnualOperatingExpenseFormGroup());
        }

        this.financialSummaryGroup.setControl('annualOperatingExpenses', this.formBuilder.array(operatingExpensesFrmArray));

        const closingCostsFrmArray: UntypedFormGroup[] = [];

        if (property.closingCosts !== null) {
            for (let i = 0; i < property.closingCosts.length; i++) {
                closingCostsFrmArray.push(this.buildClosingCostFormGroup(property.closingCosts[i].closingCostTypeId,
                    property.closingCosts[i].amount));
            }
        }

        if (closingCostsFrmArray.length === 0) {
            closingCostsFrmArray.push(this.buildEmptyClosingCostFormGroup());
        }

        this.financialSummaryGroup.setControl('closingCosts', this.formBuilder.array(closingCostsFrmArray));

        const interiorRepairExpensesFrmArray: UntypedFormGroup[] = [];

        if (property.interiorRepairExpenses !== null) {
            for (let i = 0; i < property.interiorRepairExpenses.length; i++) {
                interiorRepairExpensesFrmArray.push(this.buildInteriorRepairExpenseFormGroup(property.interiorRepairExpenses[i].interiorRepairExpenseTypeId,
                    property.interiorRepairExpenses[i].amount));
            }
        }

        if (interiorRepairExpensesFrmArray.length === 0) {
            interiorRepairExpensesFrmArray.push(this.buildEmptyInteriorRepairExpenseFormGroup());
        }

        this.financialSummaryGroup.setControl('interiorRepairExpenses', this.formBuilder.array(interiorRepairExpensesFrmArray));

        const exteriorExpensesFrmArray: UntypedFormGroup[] = [];

        if (property.exteriorRepairExpenses !== null) {
            for (let i = 0; i < property.exteriorRepairExpenses.length; i++) {
                exteriorExpensesFrmArray.push(this.buildExteriorRepairExpenseFormGroup(property.exteriorRepairExpenses[i].exteriorRepairExpenseTypeId,
                    property.exteriorRepairExpenses[i].amount));
            }
        }

        if (exteriorExpensesFrmArray.length === 0) {
            exteriorExpensesFrmArray.push(this.buildEmptyExteriorRepairExpenseFormGroup());
        }

        this.financialSummaryGroup.setControl('exteriorRepairExpenses', this.formBuilder.array(exteriorExpensesFrmArray));

        const generalRepairExpensesFrmArray: UntypedFormGroup[] = [];

        if (property.generalRepairExpenses !== null) {
            for (let i = 0; i < property.generalRepairExpenses.length; i++) {
                generalRepairExpensesFrmArray.push(this.buildGeneralRepairExpenseFormGroup(property.generalRepairExpenses[i].generalRepairExpenseTypeId,
                    property.generalRepairExpenses[i].amount));
            }
        }

        if (generalRepairExpensesFrmArray.length === 0) {
            generalRepairExpensesFrmArray.push(this.buildEmptyGeneralRepairExpenseFormGroup());
        }

        this.financialSummaryGroup.setControl('generalRepairExpenses', this.formBuilder.array(generalRepairExpensesFrmArray));

        this.onRefreshRatesClick();
        this.pageLoading = false;
    }

    public onAddEditPropertyFormSubmit(addEditPropertyNgForm: FormGroupDirective): void {
        if (addEditPropertyNgForm.invalid) {
            return;
        }

        this.isAddingEditingProperty = true;
        this.validationErrorResult = null;
        this.serverError = false;

        const writeAddress = new WriteAddress(this.address.value, this.city.value, +this.stateId.value, this.zipCode.value);
        const writeUnitGroups = this.getWriteUnitGroups();
        const writeAnnualOperatingExpenses = this.getWriteAnnualOperatingExpenses();
        const writeClosingCosts = this.getWriteClosingCosts();
        const writeExteriorRepairExpenses = this.getWriteExteriorRepairExpenses();
        const writeGeneralRepairExpenses = this.getWriteGeneralRepairExpenses();
        const writeInteriorRepairExpenses = this.getWriteInteriorRepairExpenses();

        const apiModel = new WriteProperty(this.thumbnailImage.value,
            +this.yearBuiltIn.value,
            +this.buildingSquareFootage.value,
            +this.lotSquareFootage.value,
            +this.propertyTypeId.value,
            +this.propertyStatusId.value,
            this.notes.value,
            writeAddress,
            writeUnitGroups,
            writeAnnualOperatingExpenses,
            +this.annualOperatingExpensesGrowthRate.value,
            +this.marketCapitalizationRate.value,
            +this.annualGrossScheduledRentalIncomeGrowthRate.value,
            +this.annualGrossScheduledRentalIncome.value,
            +this.annualVacancyRate.value,
            +this.annualPropertyManagementFeeRate.value,
            writeClosingCosts,
            +this.downPayment.value,
            writeExteriorRepairExpenses,
            writeGeneralRepairExpenses,
            writeInteriorRepairExpenses,
            +this.loanApr.value,
            +this.loanYears.value,
            +this.otherAnnualIncome.value,
            +this.purchasePrice.value,
            this.groupName.value);

        if (this.propertyId !== 0) {
            this.propertyService.updateProperty(this.propertyId, apiModel)
                .pipe(takeWhile(() => this.alive))
                .subscribe(updatedProperty => {
                    this.isAddingEditingProperty = false;
                    const message = new MessageDto(this.messageType, updatedProperty);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                    this.resetForm();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingProperty = false;
                    });
        } else {
            this.propertyService.saveNewProperty(apiModel)
                .pipe(takeWhile(() => this.alive))
                .subscribe(addedProperty => {
                    this.isAddingEditingProperty = false;
                    const message = new MessageDto(this.messageType, addedProperty);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                    this.resetForm();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingProperty = false;
                    });
        }
    }

    public onRefreshRatesClick(): void {
        this.isCalculatingRates = true;
        this.refreshRatesServerError = false;
        this.refreshRatesValidationErrorResult = null;
        const apiModel = this.getWriteAnnualFinancialSummary();

        this.calculatorService.getFinancialSummary(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => {
                this.financialSummary = response;
                this.isCalculatingRates = false;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.refreshRatesValidationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.refreshRatesServerError = true;
                    }

                    this.isCalculatingRates = false;
                });
    }

    public onAddUnitGroupClick(): void {
        this.unitGroups.push(this.buildEmptyUnitGroupFormGroup());
    }

    public onRemoveUnitGroupClick(unitGroup: AbstractControl): void {
        const index = this.unitGroups.controls.indexOf(unitGroup);
        this.unitGroups.controls.splice(index, 1);
    }

    public onAddAnnualOperatingExpenseClick(): void {
        this.annualOperatingExpenses.push(this.buildEmptyAnnualOperatingExpenseFormGroup());
    }

    public onRemoveAnnualOperatingExpenseClick(annualOperatingExpense: AbstractControl): void {
        const index = this.annualOperatingExpenses.controls.indexOf(annualOperatingExpense);
        this.annualOperatingExpenses.controls.splice(index, 1);
    }

    public onAddClosingCostClick(): void {
        this.closingCosts.push(this.buildEmptyClosingCostFormGroup());
    }

    public onRemoveClosingCostClick(closingCost: AbstractControl): void {
        const index = this.closingCosts.controls.indexOf(closingCost);
        this.closingCosts.controls.splice(index, 1);
    }

    public onAddInteriorRepairExpenseClick(): void {
        this.interiorRepairExpenses.push(this.buildEmptyInteriorRepairExpenseFormGroup());
    }

    public onRemoveInteriorRepairExpenseClick(interiorRepairExpense: AbstractControl): void {
        const index = this.interiorRepairExpenses.controls.indexOf(interiorRepairExpense);
        this.interiorRepairExpenses.controls.splice(index, 1);
    }

    public onAddExteriorRepairExpenseClick(): void {
        this.exteriorRepairExpenses.push(this.buildEmptyExteriorRepairExpenseFormGroup());
    }

    public onRemoveExteriorRepairExpenseClick(exteriorRepairExpense: AbstractControl): void {
        const index = this.exteriorRepairExpenses.controls.indexOf(exteriorRepairExpense);
        this.exteriorRepairExpenses.controls.splice(index, 1);
    }

    public onAddGeneralRepairExpenseClick(): void {
        this.generalRepairExpenses.push(this.buildEmptyGeneralRepairExpenseFormGroup());
    }

    public onRemoveGeneralRepairExpenseClick(generalRepairExpense: AbstractControl): void {
        const index = this.generalRepairExpenses.controls.indexOf(generalRepairExpense);
        this.generalRepairExpenses.controls.splice(index, 1);
    }

    public resetForm() {
        this.addEditPropertyForm.reset();
        this.createAddEditPropertyForm();
        this.validationErrorResult = null;
        this.serverError = false;
        this.refreshRatesValidationErrorResult = null;
        this.refreshRatesServerError = false;
        this.financialSummary = new ReadFinancialSummary();
    }

    public onThumbnailImageChange(event): void {
        if (event.target.files.length > 0) {
            const files: File[] | null = event.target.files as File[];
            const fileNames = [];

            for (let i = 0; i < files.length; i++) {
                fileNames.push(files[i].name);
            }

            this.selectedThumbnailImage = fileNames.join(', ');
            this.thumbnailImage.setValue(files[0]);
        } else {
            this.selectedThumbnailImage = null;
            this.thumbnailImage.setValue(null);
        }

        this.addEditPropertyForm.markAsDirty();
    }

    public onRemoveThumbnailImageClick(): void {
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete thumbnail image for this property?`, MessageType.deleteRentalPropertyThumbnailImage);
    }

    private deleteThumbnailImage() {
        this.propertyService.deleteThumbnailImage(this.propertyId)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.hasThumbnailImage = false;
                const message = new MessageDto(MessageType.deletedRentalPropertyThumbnailImage, null);
                this.messagingService.sendMessage(message);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setGroupNameFormControlValidators(): void {
        if (this.groupNameIsRequired) {
            this.groupName.setValidators([Validators.required]);
            this.groupName.updateValueAndValidity();
        }
    }

    private getWriteUnitGroups(): WriteUnitGroup[] {
        const writeUnitGroups: WriteUnitGroup[] = [];

        for (let i = 0; i < this.unitGroups.controls.length; i++) {
            const id = +(this.unitGroups.controls[i].get('id') as UntypedFormControl).value;
            const bathrooms = +(this.unitGroups.controls[i].get('bathrooms') as UntypedFormControl).value;
            const bedrooms = +(this.unitGroups.controls[i].get('bedrooms') as UntypedFormControl).value;
            const numberOfUnits = +(this.unitGroups.controls[i].get('numberOfUnits') as UntypedFormControl).value;
            const squareFootage = +(this.unitGroups.controls[i].get('squareFootage') as UntypedFormControl).value;

            writeUnitGroups.push(new WriteUnitGroup(id, bathrooms, bedrooms, numberOfUnits, squareFootage));
        }

        return writeUnitGroups;
    }

    private getWriteAnnualOperatingExpenses(): WriteAnnualOperatingExpense[] {
        const annualOperatingExpensesApiModel: WriteAnnualOperatingExpense[] = [];

        for (let i = 0; i < this.annualOperatingExpenses.controls.length; i++) {
            const operatingExpenseTypeId = +(this.annualOperatingExpenses.controls[i].get('operatingExpenseTypeId') as UntypedFormControl).value;
            const amount = +(this.annualOperatingExpenses.controls[i].get('amount') as UntypedFormControl).value;

            const annualOperatingExpenseApiModel = new WriteAnnualOperatingExpense(operatingExpenseTypeId, amount);
            annualOperatingExpensesApiModel.push(annualOperatingExpenseApiModel);
        }

        return annualOperatingExpensesApiModel;
    }

    private getWriteClosingCosts(): WriteClosingCost[] {
        const writeClosingCosts: WriteClosingCost[] = [];

        for (let i = 0; i < this.closingCosts.controls.length; i++) {
            const closingCostTypeId = +(this.closingCosts.controls[i].get('closingCostTypeId') as UntypedFormControl).value;
            const amount = +(this.closingCosts.controls[i].get('amount') as UntypedFormControl).value;

            const writeClosingCost = new WriteClosingCost(closingCostTypeId, amount);
            writeClosingCosts.push(writeClosingCost);
        }

        return writeClosingCosts;
    }

    private getWriteExteriorRepairExpenses(): WriteExteriorRepairExpense[] {
        const writeExteriorRepairExpenses: WriteExteriorRepairExpense[] = [];

        for (let i = 0; i < this.exteriorRepairExpenses.controls.length; i++) {
            const exteriorRepairExpenseTypeId = +(this.exteriorRepairExpenses.controls[i].get('exteriorRepairExpenseTypeId') as UntypedFormControl).value;
            const amount = +(this.exteriorRepairExpenses.controls[i].get('amount') as UntypedFormControl).value;

            const writeExteriorRepairExpense = new WriteExteriorRepairExpense(exteriorRepairExpenseTypeId, amount);
            writeExteriorRepairExpenses.push(writeExteriorRepairExpense);
        }

        return writeExteriorRepairExpenses;
    }

    private getWriteGeneralRepairExpenses(): WriteGeneralRepairExpense[] {
        const writeGeneralRepairExpenses: WriteGeneralRepairExpense[] = [];

        for (let i = 0; i < this.generalRepairExpenses.controls.length; i++) {
            const generalRepairExpenseTypeId = +(this.generalRepairExpenses.controls[i].get('generalRepairExpenseTypeId') as UntypedFormControl).value;
            const amount = +(this.generalRepairExpenses.controls[i].get('amount') as UntypedFormControl).value;

            const writeGeneralRepairExpense = new WriteGeneralRepairExpense(generalRepairExpenseTypeId, amount);
            writeGeneralRepairExpenses.push(writeGeneralRepairExpense);
        }

        return writeGeneralRepairExpenses;
    }

    private getWriteInteriorRepairExpenses(): WriteInteriorRepairExpense[] {
        const writeInteriorRepairExpenses: WriteInteriorRepairExpense[] = [];

        for (let i = 0; i < this.interiorRepairExpenses.controls.length; i++) {
            const interiorRepairExpenseTypeId = +(this.interiorRepairExpenses.controls[i].get('interiorRepairExpenseTypeId') as UntypedFormControl).value;
            const amount = +(this.interiorRepairExpenses.controls[i].get('amount') as UntypedFormControl).value;

            const writeInteriorRepairExpense = new WriteInteriorRepairExpense(interiorRepairExpenseTypeId, amount);
            writeInteriorRepairExpenses.push(writeInteriorRepairExpense);
        }

        return writeInteriorRepairExpenses;
    }

    private getWriteAnnualFinancialSummary(): WriteFinancialSummary {
        const writeAnnualOperatingExpenses = this.getWriteAnnualOperatingExpenses();
        const writeClosingCosts = this.getWriteClosingCosts();
        const writeExteriorRepairExpenses = this.getWriteExteriorRepairExpenses();
        const writeGeneralRepairExpenses = this.getWriteGeneralRepairExpenses();
        const writeInteriorRepairExpenses = this.getWriteInteriorRepairExpenses();
        const writeFinancialSummary = new WriteFinancialSummary(+this.purchasePrice.value,
            +this.downPayment.value,
            +this.annualGrossScheduledRentalIncome.value,
            +this.otherAnnualIncome.value,
            +this.annualVacancyRate.value,
            +this.annualPropertyManagementFeeRate.value,
            +this.loanApr.value,
            +this.loanYears.value,
            +this.annualGrossScheduledRentalIncomeGrowthRate.value,
            +this.annualOperatingExpensesGrowthRate.value,
            +this.marketCapitalizationRate.value,
            writeAnnualOperatingExpenses,
            writeClosingCosts,
            writeExteriorRepairExpenses,
            writeGeneralRepairExpenses,
            writeInteriorRepairExpenses);

        return writeFinancialSummary;
    }

    private setStates(): void {
        this.lookupService.getAllStates()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.states = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setOperatingExpenseTypes(): void {
        this.lookupService.getAllOperatingExpenseTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.operatingExpenseTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setClosingCostTypes(): void {
        this.lookupService.getAllClosingCostTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.closingCostTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setInteriorRepairExpenseTypes(): void {
        this.lookupService.getAllInteriorRepairExpenseTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.interiorRepairExpenseTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setExteriorRepairExpenseTypes(): void {
        this.lookupService.getAllExteriorRepairExpenseTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.exteriorRepairExpenseTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setGeneralRepairExpenseTypes(): void {
        this.lookupService.getAllGeneralRepairExpenseTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.generalRepairExpenseTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setPropertyTypes(): void {
        this.lookupService.getAllPropertyTypes()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.propertyTypes = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setPropertyStatuses(): void {
        this.lookupService.getAllPropertyStatuses()
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => this.propertyStatuses = response,
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private createAddEditPropertyForm(): void {
        this.addEditPropertyForm = this.formBuilder.group({
            thumbnailImage: [null, [thumbnailImageValidator]],
            address: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(500)]],
            city: ['', [Validators.required, Validators.maxLength(500)]],
            stateId: ['', Validators.required],
            zipCode: ['', [Validators.required]],
            propertyTypeId: ['', Validators.required],
            propertyStatusId: [1, Validators.required],
            buildingSquareFootage: ['', [Validators.required, Validators.min(1), Validators.max(100000000)]],
            lotSquareFootage: ['', [Validators.min(0), Validators.max(1000000)]],
            unitGroups: this.formBuilder.array([
                this.buildEmptyUnitGroupFormGroup()
            ]),
            yearBuiltIn: ['', [Validators.required, Validators.min(1800), Validators.max(new Date().getFullYear())]],
            financialSummaryGroup: this.formBuilder.group({
                purchasePrice: ['', [Validators.required, Validators.min(1), Validators.max(1000000000000)]],
                downPaymentPercent: ['', [Validators.min(0), Validators.max(100)]],
                downPayment: ['', [Validators.min(0), Validators.max(1000000000000)]],
                annualGrossScheduledRentalIncome: ['', [Validators.required, Validators.min(1), Validators.max(1000000000000)]],
                otherAnnualIncome: ['', [Validators.min(0), Validators.max(1000000000000)]],
                annualVacancyRate: ['', [Validators.min(0), Validators.max(100)]],
                annualPropertyManagementFeeRate: ['', [Validators.min(0), Validators.max(100)]],
                loanApr: ['', [Validators.min(0), Validators.max(100)]],
                loanYears: ['', [Validators.min(0), Validators.max(50)]],
                annualGrossScheduledRentalIncomeGrowthRate: ['', [Validators.min(0), Validators.max(100)]],
                annualOperatingExpensesGrowthRate: ['', [Validators.min(0), Validators.max(100)]],
                marketCapitalizationRate: ['', [Validators.min(0), Validators.max(100)]],
                annualOperatingExpenses: this.formBuilder.array([
                    this.buildEmptyAnnualOperatingExpenseFormGroup()
                ]),
                closingCosts: this.formBuilder.array([
                    this.buildEmptyClosingCostFormGroup()
                ]),
                exteriorRepairExpenses: this.formBuilder.array([
                    this.buildEmptyExteriorRepairExpenseFormGroup()
                ]),
                generalRepairExpenses: this.formBuilder.array([
                    this.buildEmptyGeneralRepairExpenseFormGroup()
                ]),
                interiorRepairExpenses: this.formBuilder.array([
                    this.buildEmptyInteriorRepairExpenseFormGroup()
                ])
            }, { validator: Validators.compose([loanValidator('loanApr', 'loanYears', 'purchasePrice', 'downPayment')]) }),
            notes: [''],
            groupName: ['', [Validators.maxLength(50)]]
        });
    }

    private buildEmptyUnitGroupFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            id: [0, Validators.required],
            bathrooms: ['', [Validators.required, Validators.min(0), Validators.max(1000000000000)]],
            bedrooms: ['', [Validators.required, Validators.min(0), Validators.max(1000000000000)]],
            numberOfUnits: ['', [Validators.required, Validators.min(0), Validators.max(1000000000000)]],
            squareFootage: ['', [Validators.required, Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildUnitGroupFormGroup(id: number, bathrooms: number, bedrooms: number, numberOfUnits: number, squareFootage: number): UntypedFormGroup {
        return this.formBuilder.group({
            id: [id, Validators.required],
            bathrooms: [bathrooms, [Validators.min(0), Validators.max(1000000000000)]],
            bedrooms: [bedrooms, [Validators.min(0), Validators.max(1000000000000)]],
            numberOfUnits: [numberOfUnits, [Validators.min(0), Validators.max(1000000000000)]],
            squareFootage: [squareFootage, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildEmptyAnnualOperatingExpenseFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            operatingExpenseTypeId: ['1', Validators.required],
            amount: ['', [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildAnnualOperatingExpenseFormGroup(operatingExpenseTypeId: number, amount: number): UntypedFormGroup {
        return this.formBuilder.group({
            operatingExpenseTypeId: [operatingExpenseTypeId, Validators.required],
            amount: [amount, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildEmptyClosingCostFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            closingCostTypeId: ['1', Validators.required],
            amount: ['', [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildClosingCostFormGroup(closingCostTypeId: number, amount: number): UntypedFormGroup {
        return this.formBuilder.group({
            closingCostTypeId: [closingCostTypeId, Validators.required],
            amount: [amount, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildEmptyExteriorRepairExpenseFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            exteriorRepairExpenseTypeId: ['2', Validators.required],
            amount: ['', [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildExteriorRepairExpenseFormGroup(exteriorRepairExpenseTypeId: number, amount: number): UntypedFormGroup {
        return this.formBuilder.group({
            exteriorRepairExpenseTypeId: [exteriorRepairExpenseTypeId, Validators.required],
            amount: [amount, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildEmptyGeneralRepairExpenseFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            generalRepairExpenseTypeId: ['3', Validators.required],
            amount: ['', [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildGeneralRepairExpenseFormGroup(generalRepairExpenseTypeId: number, amount: number): UntypedFormGroup {
        return this.formBuilder.group({
            generalRepairExpenseTypeId: [generalRepairExpenseTypeId, Validators.required],
            amount: [amount, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildEmptyInteriorRepairExpenseFormGroup(): UntypedFormGroup {
        return this.formBuilder.group({
            interiorRepairExpenseTypeId: ['8', Validators.required],
            amount: ['', [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private buildInteriorRepairExpenseFormGroup(interiorRepairExpenseTypeId: number, amount: number): UntypedFormGroup {
        return this.formBuilder.group({
            interiorRepairExpenseTypeId: [interiorRepairExpenseTypeId, Validators.required],
            amount: [amount, [Validators.min(0), Validators.max(1000000000000)]]
        });
    }

    private setFormControlsObservables(): void {
        this.purchasePrice.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe((value) => {
                if (this.purchasePrice.valid && this.downPaymentPercent.valid) {
                    let downPaymentAmount = 0;

                    if (+this.downPaymentPercent.value === 0) {
                        downPaymentAmount = +value;
                    } else {
                        downPaymentAmount = +value * (+this.downPaymentPercent.value / 100);
                    }

                    this.downPayment.patchValue(downPaymentAmount);
                }
            });

        this.downPaymentPercent.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe((value) => {
                if (this.purchasePrice.value && this.purchasePrice.valid && this.downPaymentPercent.valid) {
                    let downPaymentAmount = 0;

                    if (+value === 0) {
                        downPaymentAmount = +this.purchasePrice.value;
                    } else {
                        downPaymentAmount = +this.purchasePrice.value * (+value / 100);
                    }
                    this.downPayment.patchValue(downPaymentAmount);
                }
            });

        this.annualGrossScheduledRentalIncome.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.updateVariableExpensesAmounts(+this.annualVacancyRate.value);
            });

        this.otherAnnualIncome.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.updateVariableExpensesAmounts(+this.annualVacancyRate.value);
            });

        this.annualVacancyRate.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe((value: number) => {
                this.updateVariableExpensesAmounts(value);
            });

        this.annualPropertyManagementFeeRate.valueChanges
            .pipe(takeWhile(() => this.alive))
            .subscribe((value: number) => {
                this.setAnnualPropertyManagementFee(value);
            });
    }

    private updateVariableExpensesAmounts(annualVacancyRate: number): void {
        if (this.annualGrossScheduledRentalIncome.valid && this.annualVacancyRate.valid) {
            this.annualVacancyLoss = +this.annualGrossScheduledRentalIncome.value * (+annualVacancyRate / 100);
        }

        this.setAnnualPropertyManagementFee(+this.annualPropertyManagementFeeRate.value);
    }

    private setAnnualPropertyManagementFee(annualPropertyManagementFeeRate: number): void {
        if (this.annualGrossScheduledRentalIncome.valid && this.otherAnnualIncome.valid && this.annualVacancyRate.valid && this.annualPropertyManagementFeeRate.valid) {
            const vacancyLoss = +this.annualGrossScheduledRentalIncome.value * (+this.annualVacancyRate.value / 100);
            const netRentalIncome = +this.annualGrossScheduledRentalIncome.value - vacancyLoss;
            const effectiveGrossIncome = netRentalIncome + +this.otherAnnualIncome.value;
            this.annualPropertyManagementFee = effectiveGrossIncome * (annualPropertyManagementFeeRate / 100);
        }
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.deleteRentalPropertyThumbnailImage) {
                    this.deleteThumbnailImage();
                }
            });
    }

    get thumbnailImage(): UntypedFormControl {
        return this.addEditPropertyForm.get('thumbnailImage') as UntypedFormControl;
    }

    get address(): UntypedFormControl {
        return this.addEditPropertyForm.get('address') as UntypedFormControl;
    }

    get city(): UntypedFormControl {
        return this.addEditPropertyForm.get('city') as UntypedFormControl;
    }

    get stateId(): UntypedFormControl {
        return this.addEditPropertyForm.get('stateId') as UntypedFormControl;
    }

    get zipCode(): UntypedFormControl {
        return this.addEditPropertyForm.get('zipCode') as UntypedFormControl;
    }

    get propertyTypeId(): UntypedFormControl {
        return this.addEditPropertyForm.get('propertyTypeId') as UntypedFormControl;
    }

    get propertyStatusId(): UntypedFormControl {
        return this.addEditPropertyForm.get('propertyStatusId') as UntypedFormControl;
    }

    get lotSquareFootage(): UntypedFormControl {
        return this.addEditPropertyForm.get('lotSquareFootage') as UntypedFormControl;
    }

    get buildingSquareFootage(): UntypedFormControl {
        return this.addEditPropertyForm.get('buildingSquareFootage') as UntypedFormControl;
    }

    get yearBuiltIn(): UntypedFormControl {
        return this.addEditPropertyForm.get('yearBuiltIn') as UntypedFormControl;
    }

    get unitGroups(): UntypedFormArray {
        return this.addEditPropertyForm.get('unitGroups') as UntypedFormArray;
    }

    get financialSummaryGroup(): UntypedFormGroup {
        return this.addEditPropertyForm.get('financialSummaryGroup') as UntypedFormGroup;
    }

    get purchasePrice(): UntypedFormControl {
        return this.financialSummaryGroup.get('purchasePrice') as UntypedFormControl;
    }

    get downPaymentPercent(): UntypedFormControl {
        return this.financialSummaryGroup.get('downPaymentPercent') as UntypedFormControl;
    }

    get downPayment(): UntypedFormControl {
        return this.financialSummaryGroup.get('downPayment') as UntypedFormControl;
    }

    get loanApr(): UntypedFormControl {
        return this.financialSummaryGroup.get('loanApr') as UntypedFormControl;
    }

    get loanYears(): UntypedFormControl {
        return this.financialSummaryGroup.get('loanYears') as UntypedFormControl;
    }

    get annualGrossScheduledRentalIncome(): UntypedFormControl {
        return this.financialSummaryGroup.get('annualGrossScheduledRentalIncome') as UntypedFormControl;
    }

    get otherAnnualIncome(): UntypedFormControl {
        return this.financialSummaryGroup.get('otherAnnualIncome') as UntypedFormControl;
    }

    get annualVacancyRate(): UntypedFormControl {
        return this.financialSummaryGroup.get('annualVacancyRate') as UntypedFormControl;
    }

    get annualPropertyManagementFeeRate(): UntypedFormControl {
        return this.financialSummaryGroup.get('annualPropertyManagementFeeRate') as UntypedFormControl;
    }

    get annualGrossScheduledRentalIncomeGrowthRate(): UntypedFormControl {
        return this.financialSummaryGroup.get('annualGrossScheduledRentalIncomeGrowthRate') as UntypedFormControl;
    }

    get annualOperatingExpensesGrowthRate(): UntypedFormControl {
        return this.financialSummaryGroup.get('annualOperatingExpensesGrowthRate') as UntypedFormControl;
    }

    get marketCapitalizationRate(): UntypedFormControl {
        return this.financialSummaryGroup.get('marketCapitalizationRate') as UntypedFormControl;
    }

    get annualOperatingExpenses(): UntypedFormArray {
        return this.financialSummaryGroup.get('annualOperatingExpenses') as UntypedFormArray;
    }

    get closingCosts(): UntypedFormArray {
        return this.financialSummaryGroup.get('closingCosts') as UntypedFormArray;
    }

    get exteriorRepairExpenses(): UntypedFormArray {
        return this.financialSummaryGroup.get('exteriorRepairExpenses') as UntypedFormArray;
    }

    get generalRepairExpenses(): UntypedFormArray {
        return this.financialSummaryGroup.get('generalRepairExpenses') as UntypedFormArray;
    }

    get interiorRepairExpenses(): UntypedFormArray {
        return this.financialSummaryGroup.get('interiorRepairExpenses') as UntypedFormArray;
    }

    get notes(): UntypedFormControl {
        return this.addEditPropertyForm.get('notes') as UntypedFormControl;
    }

    get groupName(): UntypedFormControl {
        return this.addEditPropertyForm.get('groupName') as UntypedFormControl;
    }
}
