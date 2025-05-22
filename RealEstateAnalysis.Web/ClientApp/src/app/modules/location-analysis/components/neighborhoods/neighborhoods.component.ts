import { Component, OnInit, OnDestroy, IterableDiffer, IterableDiffers, DoCheck } from '@angular/core';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { Title } from '@angular/platform-browser';
import { appConfig } from '../../../../app.config';
import { NeighborhoodService } from '../../services/neighborhood.service';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { takeWhile } from 'rxjs/operators';
import { MessageType } from '../../../../shared/enums/messageType';
import { ReadNeighborhood } from '../../dtos/reads/readNeighborhood';
import { HttpErrorResponse } from '@angular/common/http';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { AddEditNeighborhoodComponent } from '../../modals/add-edit-neighborhood/add-edit-neighborhoods.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
    templateUrl: './neighborhoods.component.html'
})
export class NeighborhoodsComponent implements OnInit, OnDestroy, DoCheck {
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public pageLoading = true;
    public states: ReadState[];
    public cities: string[];
    public passingColumnsNumbers = [1, 2, 3, 4, 5];
    public neighborhoodsSearchForm: UntypedFormGroup;
    public filteredNeighborhoods: ReadNeighborhood[];

    private alive = true;
    private selectedNeighborhood: ReadNeighborhood;
    private iterableDiffer: IterableDiffer<unknown>;
    private neighborhoods: ReadNeighborhood[];

    constructor(titleService: Title,
        private neighborhoodService: NeighborhoodService,
        private messagingService: MessagingService,
        private modalService: ModalService,
        private formBuilder: UntypedFormBuilder,
        iterableDiffers: IterableDiffers
    ) {
        titleService.setTitle(`${appConfig.businessName}: Location Analysis: Neighborhoods`);
        this.iterableDiffer = iterableDiffers.find([]).create(null);
        this.createNeighborhoodsSearchForm();
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.loadNeighborhoods();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    ngDoCheck(): void {
        const neighborhoodsHaveChange = this.iterableDiffer.diff(this.neighborhoods);

        if (neighborhoodsHaveChange) {
            this.setStates();
            this.setCities();
            this.filterNeighborhoods();
        }
    }

    public onAddNewClick(): void {
        const addModal = this.modalService.show(AddEditNeighborhoodComponent, { sizeClass: 'modal-lg' });
        addModal.messageType = MessageType.locationAnalysisNewNeighborhoodAdded;
    }

    public onEditClick(neighborhood: ReadNeighborhood): void {
        const editModal = this.modalService.show(AddEditNeighborhoodComponent, { sizeClass: 'modal-lg' });
        editModal.setEditNeighborhoodFormValues(neighborhood);
        editModal.messageType = MessageType.locationAnalysisNeighborhoodUpdated;
    }

    public onDeleteClick(neighborhood: ReadNeighborhood): void {
        this.selectedNeighborhood = neighborhood;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${neighborhood.neighborhoodName} neighborhood?`, MessageType.locationAnalysisDeleteNeighborhood);
    }

    public loadNeighborhoods(): void {
        this.neighborhoodService.getAllNeighborhoods()
            .pipe(takeWhile(() => this.alive))
            .subscribe(neighborhoods => {
                this.neighborhoods = neighborhoods;
                this.pageLoading = false;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.pageLoading = false;
                });
    }

    public filterNeighborhoods(): void {
        const city = this.city.value;
        const stateId = +this.stateId.value;
        const numberOfPassingColumns = +this.numberOfPassingColumns.value;
        this.filteredNeighborhoods = this.neighborhoods;

        if (stateId !== 0) {
            this.filteredNeighborhoods = this.filteredNeighborhoods.filter(x => x.state.id === stateId);
        }

        if (numberOfPassingColumns !== 0) {
            this.filteredNeighborhoods = this.filteredNeighborhoods.filter(x => this.getNumberOfPassingColumns(x) === numberOfPassingColumns);
        }

        this.filteredNeighborhoods = this.filteredNeighborhoods.sort((a, b) => this.orderByDesc(a.id, b.id));
    }

    private getNumberOfPassingColumns(neighborhood: ReadNeighborhood): number {
        let number = 0;

        if (neighborhood.medianHouseholdIncomeIsGood) {
            number++;
        }

        if (neighborhood.medianContractRentIsGood) {
            number++;
        }

        if (neighborhood.cityToNeighborhoodUnemploymentRateDifferenceIsGood) {
            number++;
        }

        if (neighborhood.povertyRateIsGood) {
            number++;
        }

        if (neighborhood.ethnicMixLargestSlicePercentIsGood) {
            number++;
        }

        return number;
    }

    private createNeighborhoodsSearchForm(): void {
        this.neighborhoodsSearchForm = this.formBuilder.group({
            city: [''],
            stateId: [''],
            numberOfPassingColumns: ['']
        });
    }

    private setStates(): void {
        const dupStates = this.neighborhoods.map(x => x.state);
        this.states = [];

        for (let i = 0; i < dupStates.length; i++) {
            const existingState = this.states.find(x => x.id === dupStates[i].id);

            if (existingState === undefined) {
                this.states.push(dupStates[i]);
            }
        }

        this.states = this.states.sort((a, b) => this.orderByAsc(a.name, b.name));
    }

    private setCities(): void {
        const dupCities = this.neighborhoods.map(x => x.city);
        this.cities = [];

        for (let i = 0; i < dupCities.length; i++) {
            const existingCity = this.cities.find(x => x === dupCities[i]);

            if (existingCity === undefined) {
                this.cities.push(dupCities[i]);
            }
        }

        this.cities = this.cities.sort((a, b) => this.orderByAsc(a, b));
    }

    private addNeighborhood(newNeighborhood: ReadNeighborhood): void {
        this.neighborhoods.push(newNeighborhood);
    }

    private updateNeighborhood(updatedCity: ReadNeighborhood): void {
        const itemIndex = this.neighborhoods.findIndex(x => x.id === updatedCity.id);
        this.neighborhoods.splice(itemIndex, 1);
        this.neighborhoods.push(updatedCity);
    }

    private deleteNeighborhood() {
        this.neighborhoodService.deleteNeighborhood(this.selectedNeighborhood.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.neighborhoods.findIndex(x => x.id === this.selectedNeighborhood.id);
                this.neighborhoods.splice(itemIndex, 1);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private subscribeToMessagingService(): void {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.locationAnalysisNewNeighborhoodAdded) {
                    this.addNeighborhood(message.content as ReadNeighborhood);
                }

                if (message.messageType === MessageType.locationAnalysisDeleteNeighborhood) {
                    this.deleteNeighborhood();
                }

                if (message.messageType === MessageType.locationAnalysisNeighborhoodUpdated) {
                    this.updateNeighborhood(message.content as ReadNeighborhood);
                }
            });
    }

    private orderByDesc(a: any, b: any): number {
        if (a > b) {
            return -1;
        }

        if (a < b) {
            return 1;
        }

        return 0;
    }

    private orderByAsc(a: any, b: any): number {
        if (a > b) {
            return 1;
        }

        if (a < b) {
            return -1;
        }

        return 0;
    }

    get city(): UntypedFormControl {
        return this.neighborhoodsSearchForm.get('city') as UntypedFormControl;
    }

    get stateId(): UntypedFormControl {
        return this.neighborhoodsSearchForm.get('stateId') as UntypedFormControl;
    }

    get numberOfPassingColumns(): UntypedFormControl {
        return this.neighborhoodsSearchForm.get('numberOfPassingColumns') as UntypedFormControl;
    }
}
