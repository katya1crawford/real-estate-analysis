import { Component, OnInit, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators, NgForm, FormGroupDirective } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadCity } from '../../dtos/reads/readCity';
import { MessageType } from '../../../../shared/enums/messageType';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { ReadLookup } from '../../../../shared/dtos/reads/readLookup';
import { LookupService } from '../../../../shared/services/lookup.service';
import { WriteCity } from '../../dtos/writes/writeCity';
import { CityService } from '../../services/city.service';
import { MessageDto } from '../../../../shared/dtos/messageDto';
import { ModalService } from '../../../../shared/services/modal/modal.service';


@Component({
    templateUrl: './add-edit-city.component.html'
})
export class AddEditCityComponent implements OnInit, OnDestroy {
    public messageType: MessageType;
    public modalTitle = 'Add New City';
    public addEditCityForm: UntypedFormGroup;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public isAddingEditingCity = false;
    public states: ReadLookup[];

    private alive = true;
    private cityId = 0;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder,
        private lookupService: LookupService,
        private cityService: CityService
    ) {
        this.createAddEditCityForm();
    }

    ngOnInit(): void {
        this.setStates();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public setEditCityFormValues(city: ReadCity): void {
        this.modalTitle = 'Edit City';
        this.cityId = city.id;

        this.cityName.patchValue(city.cityName);
        this.populationInYearStart.patchValue(city.populationInYearStart);
        this.populationInYearEnd.patchValue(city.populationInYearEnd);
        this.medianHouseholdIncomeInYearStart.patchValue(city.medianHouseholdIncomeInYearStart);
        this.medianHouseholdIncomeInYearEnd.patchValue(city.medianHouseholdIncomeInYearEnd);
        this.medianHouseOrCondoValueInYearStart.patchValue(city.medianHouseOrCondoValueInYearStart);
        this.medianHouseOrCondoValueInYearEnd.patchValue(city.medianHouseOrCondoValueInYearEnd);
        this.crimeIndexInYearStart.patchValue(city.crimeIndexInYearStart);
        this.crimeIndexInYearEnd.patchValue(city.crimeIndexInYearEnd);
        this.crimeIndexYearStart.patchValue(city.crimeIndexYearStart);
        this.crimeIndexYearEnd.patchValue(city.crimeIndexYearEnd);
        this.recentYearJobsGrowthRate.patchValue(city.recentYearJobsGrowthRate);
        this.numberOfJobsAdded.patchValue(city.numberOfJobsAdded);
        this.stateId.patchValue(city.state.id);
        this.populationYearEnd.patchValue(city.populationYearEnd);
        this.medianHouseholdIncomeYearEnd.patchValue(city.medianHouseholdIncomeYearEnd);
        this.medianHouseOrCondoValueYearEnd.patchValue(city.medianHouseOrCondoValueYearEnd);
        this.populationYearStart.patchValue(city.populationYearStart);
        this.medianHouseholdIncomeYearStart.patchValue(city.medianHouseholdIncomeYearStart);
        this.medianHouseOrCondoValueYearStart.patchValue(city.medianHouseOrCondoValueYearStart);
    }

    public onAddEditCityFormSubmit(addEditCityNgForm: FormGroupDirective): void {
        if (addEditCityNgForm.invalid) {
            return;
        }

        this.isAddingEditingCity = true;
        this.validationErrorResult = null;
        this.serverError = false;

        const writeCity = new WriteCity(this.cityName.value,
            +this.populationInYearStart.value,
            +this.populationInYearEnd.value,
            +this.medianHouseholdIncomeInYearStart.value,
            +this.medianHouseholdIncomeInYearEnd.value,
            +this.medianHouseOrCondoValueInYearStart.value,
            +this.medianHouseOrCondoValueInYearEnd.value,
            +this.crimeIndexInYearStart.value,
            +this.crimeIndexInYearEnd.value,
            +this.recentYearJobsGrowthRate.value,
            +this.numberOfJobsAdded.value,
            +this.stateId.value,
            +this.populationYearEnd.value,
            +this.medianHouseholdIncomeYearEnd.value,
            +this.medianHouseOrCondoValueYearEnd.value,
            +this.crimeIndexYearStart.value,
            +this.crimeIndexYearEnd.value,
            +this.medianHouseholdIncomeYearStart.value,
            +this.medianHouseOrCondoValueYearStart.value,
            +this.populationYearStart.value);

        if (this.cityId !== 0) {
            this.cityService.updateCity(this.cityId, writeCity)
                .pipe(takeWhile(() => this.alive))
                .subscribe(updatedCity => {
                    this.isAddingEditingCity = false;
                    const message: MessageDto = new MessageDto(this.messageType, updatedCity);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingCity = false;
                    });
        } else {
            this.cityService.saveNewCity(writeCity)
                .pipe(takeWhile(() => this.alive))
                .subscribe(addedCity => {
                    this.isAddingEditingCity = false;
                    const message = new MessageDto(this.messageType, addedCity);
                    this.messagingService.sendMessage(message);
                    this.modalService.hide();
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }

                        this.isAddingEditingCity = false;
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

    private createAddEditCityForm(): void {
        this.addEditCityForm = this.formBuilder.group({
            cityName: ['', [Validators.required, Validators.maxLength(500)]],
            populationInYearStart: ['', [Validators.required, Validators.min(1), Validators.max(999999998)]],
            populationInYearEnd: ['', [Validators.required, Validators.min(1), Validators.max(999999998)]],
            medianHouseholdIncomeInYearStart: ['', [Validators.required, Validators.min(1)]],
            medianHouseholdIncomeInYearEnd: ['', [Validators.required, Validators.min(1)]],
            medianHouseOrCondoValueInYearStart: ['', [Validators.required, Validators.min(1)]],
            medianHouseOrCondoValueInYearEnd: ['', [Validators.required, Validators.min(1)]],
            crimeIndexInYearStart: ['', [Validators.required, Validators.min(1)]],
            crimeIndexInYearEnd: ['', [Validators.required, Validators.min(1)]],
            recentYearJobsGrowthRate: ['', [Validators.required]],
            numberOfJobsAdded: ['', [Validators.required]],
            populationYearEnd: ['', [Validators.required, Validators.min(2017)]],
            medianHouseholdIncomeYearEnd: ['', [Validators.required, Validators.min(2016)]],
            medianHouseOrCondoValueYearEnd: ['', [Validators.required, Validators.min(2016)]],
            stateId: ['', [Validators.required, Validators.min(1)]],
            crimeIndexYearStart: ['', [Validators.required, Validators.min(1)]],
            crimeIndexYearEnd: ['', [Validators.required, Validators.min(1)]],
            medianHouseholdIncomeYearStart: ['', [Validators.required]],
            medianHouseOrCondoValueYearStart: ['', [Validators.required]],
            populationYearStart: ['', [Validators.required]]
        });
    }

    get cityName(): UntypedFormControl {
        return this.addEditCityForm.get('cityName') as UntypedFormControl;
    }

    get populationInYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('populationInYearStart') as UntypedFormControl;
    }

    get populationInYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('populationInYearEnd') as UntypedFormControl;
    }

    get medianHouseholdIncomeInYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseholdIncomeInYearStart') as UntypedFormControl;
    }

    get medianHouseholdIncomeInYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseholdIncomeInYearEnd') as UntypedFormControl;
    }

    get medianHouseOrCondoValueInYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseOrCondoValueInYearStart') as UntypedFormControl;
    }

    get medianHouseOrCondoValueInYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseOrCondoValueInYearEnd') as UntypedFormControl;
    }

    get crimeIndexInYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('crimeIndexInYearStart') as UntypedFormControl;
    }

    get crimeIndexInYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('crimeIndexInYearEnd') as UntypedFormControl;
    }

    get recentYearJobsGrowthRate(): UntypedFormControl {
        return this.addEditCityForm.get('recentYearJobsGrowthRate') as UntypedFormControl;
    }

    get numberOfJobsAdded(): UntypedFormControl {
        return this.addEditCityForm.get('numberOfJobsAdded') as UntypedFormControl;
    }

    get stateId(): UntypedFormControl {
        return this.addEditCityForm.get('stateId') as UntypedFormControl;
    }

    get populationYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('populationYearEnd') as UntypedFormControl;
    }

    get medianHouseholdIncomeYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseholdIncomeYearEnd') as UntypedFormControl;
    }

    get medianHouseOrCondoValueYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseOrCondoValueYearEnd') as UntypedFormControl;
    }

    get crimeIndexYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('crimeIndexYearStart') as UntypedFormControl;
    }

    get crimeIndexYearEnd(): UntypedFormControl {
        return this.addEditCityForm.get('crimeIndexYearEnd') as UntypedFormControl;
    }

    get medianHouseholdIncomeYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseholdIncomeYearStart') as UntypedFormControl;
    }

    get medianHouseOrCondoValueYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('medianHouseOrCondoValueYearStart') as UntypedFormControl;
    }

    get populationYearStart(): UntypedFormControl {
        return this.addEditCityForm.get('populationYearStart') as UntypedFormControl;
    }
}
