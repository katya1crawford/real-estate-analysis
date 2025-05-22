import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators, NgForm, FormGroupDirective } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { ReadNeighborhood } from '../../dtos/reads/readNeighborhood';
import { MessageType } from '../../../../shared/enums/messageType';
import { ReadLookup } from '../../../../shared/dtos/reads/readLookup';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { LookupService } from '../../../../shared/services/lookup.service';
import { NeighborhoodService } from '../../services/neighborhood.service';
import { WriteNeighborhood } from '../../dtos/writes/writeNeighborhood';
import { MessageDto } from '../../../../shared/dtos/messageDto';
import { ModalService } from '../../../../shared/services/modal/modal.service';


@Component({
    templateUrl: './add-edit-neighborhoods.component.html'
})
export class AddEditNeighborhoodComponent implements OnDestroy, OnInit {
    public messageType: MessageType;
    public modalTitle = 'Add New Neighborhood';
    public addEditNeighborhoodForm: UntypedFormGroup;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public isAddingEditingNeighborhood = false;
    public states: ReadLookup[];

    private alive = true;
    private neighborhoodId = 0;

    constructor(public modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder,
        private lookupService: LookupService,
        private neighborhoodService: NeighborhoodService
    ) {
        this.createAddEditNeighborhoodForm();
    }

    ngOnInit(): void {
        this.setStates();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public setEditNeighborhoodFormValues(neighborhood: ReadNeighborhood): void {
        this.modalTitle = 'Edit Neighborhood';
        this.neighborhoodId = neighborhood.id;

        this.city.patchValue(neighborhood.city);
        this.stateId.patchValue(neighborhood.state.id);
        this.neighborhoodName.patchValue(neighborhood.neighborhoodName);
        this.medianHouseholdIncome.patchValue(neighborhood.medianHouseholdIncome);
        this.medianContractRent.patchValue(neighborhood.medianContractRent);
        this.cityUnemploymentRate.patchValue(neighborhood.cityUnemploymentRate);
        this.neighborhoodUnemploymentRate.patchValue(neighborhood.neighborhoodUnemploymentRate);
        this.povertyRate.patchValue(neighborhood.povertyRate);
        this.ethnicMixLargestSlicePercent.patchValue(neighborhood.ethnicMixLargestSlicePercent);
        this.homesMedianDaysOnMarket.patchValue(neighborhood.homesMedianDaysOnMarket);
    }

    public onAddEditNeighborhoodFormSubmit(addEditNeighborhoodNgForm: FormGroupDirective): void {
        if (addEditNeighborhoodNgForm.invalid) {
            return;
        }

        this.isAddingEditingNeighborhood = true;
        this.validationErrorResult = null;
        this.serverError = false;

        const writeNeighborhood = new WriteNeighborhood(this.neighborhoodName.value,
            this.city.value,
            +this.medianHouseholdIncome.value,
            +this.medianContractRent.value,
            +this.cityUnemploymentRate.value,
            +this.neighborhoodUnemploymentRate.value,
            +this.povertyRate.value,
            +this.ethnicMixLargestSlicePercent.value,
            +this.homesMedianDaysOnMarket.value,
            +this.stateId.value);

        if (this.neighborhoodId !== 0) {
            this.neighborhoodService.updateNeighborhood(this.neighborhoodId, writeNeighborhood)
                .pipe(takeWhile(() => this.alive))
                .subscribe(updatedNeighborhood => {
                    this.isAddingEditingNeighborhood = false;
                    const message = new MessageDto(this.messageType, updatedNeighborhood);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingNeighborhood = false;
                    });
        } else {
            this.neighborhoodService.saveNewNeighborhood(writeNeighborhood)
                .pipe(takeWhile(() => this.alive))
                .subscribe(addedNeighborhood => {
                    this.isAddingEditingNeighborhood = false;
                    const message = new MessageDto(this.messageType, addedNeighborhood);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingNeighborhood = false;
                    });
        }
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

    private createAddEditNeighborhoodForm(): void {
        this.addEditNeighborhoodForm = this.formBuilder.group({
            neighborhoodName: ['', [Validators.required, Validators.maxLength(250)]],
            city: ['', [Validators.required, Validators.maxLength(500)]],
            stateId: ['', [Validators.required, Validators.min(1)]],
            medianHouseholdIncome: ['', [Validators.required, Validators.min(1)]],
            medianContractRent: ['', [Validators.required, Validators.min(1)]],
            cityUnemploymentRate: ['', [Validators.required, Validators.min(0.01), Validators.max(100)]],
            neighborhoodUnemploymentRate: ['', [Validators.required, Validators.min(0.01), Validators.max(100)]],
            povertyRate: ['', [Validators.required, Validators.min(0.01), Validators.max(100)]],
            ethnicMixLargestSlicePercent: ['', [Validators.required, Validators.min(0.01), Validators.max(100)]],
            homesMedianDaysOnMarket: ['', [Validators.required, Validators.min(0)]]
        });
    }

    get stateId(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('stateId') as UntypedFormControl;
    }

    get neighborhoodName(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('neighborhoodName') as UntypedFormControl;
    }

    get city(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('city') as UntypedFormControl;
    }

    get medianHouseholdIncome(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('medianHouseholdIncome') as UntypedFormControl;
    }

    get medianContractRent(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('medianContractRent') as UntypedFormControl;
    }

    get cityUnemploymentRate(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('cityUnemploymentRate') as UntypedFormControl;
    }

    get neighborhoodUnemploymentRate(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('neighborhoodUnemploymentRate') as UntypedFormControl;
    }

    get povertyRate(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('povertyRate') as UntypedFormControl;
    }

    get ethnicMixLargestSlicePercent(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('ethnicMixLargestSlicePercent') as UntypedFormControl;
    }

    get homesMedianDaysOnMarket(): UntypedFormControl {
        return this.addEditNeighborhoodForm.get('homesMedianDaysOnMarket') as UntypedFormControl;
    }
}
