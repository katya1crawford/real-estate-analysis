import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { ReadCity } from '../../dtos/reads/readCity';
import { WriteCity } from '../../dtos/writes/writeCity';
import { CityService } from '../../services/cityService/city.service';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})
export class AddCityComponent implements OnInit, OnDestroy {


  public description = 'Add or Edit City';
  public city: ReadCity;
  public addEditCityForm: FormGroup;
  public states$: Observable<ReadState[]>
  alive = true;
  subscription: Subscription;

  constructor(private fb: FormBuilder,
    public cityService: CityService,
    private dialogRef: MatDialogRef<AddCityComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) {
    this.createForm();
    // this.city = data.city;
    data ? this.city = data.city : this.city = undefined;
  }


  ngOnInit(): void {
    this.states$ = this.cityService.getAllStates();
    if (this.city !== undefined) {
      this.createEditForm();
      console.log(this.city);
    }
  }

  save(): void {
    const value = this.addEditCityForm.value;

    const apiModel: WriteCity = new WriteCity(
      value.cityName,
      +value.populationInYearStart,
      +value.populationInYearEnd,
      +value.medianHouseholdIncomeInYearStart,
      +value.medianHouseholdIncomeInYearEnd,
      +value.medianHouseOrCondoValueInYearStart,
      +value.medianHouseOrCondoValueInYearEnd,
      +value.crimeIndexInYearStart,
      +value.crimeIndexInYearEnd,
      +value.recentYearJobsGrowthRate,
      +value.numberOfJobsAdded,
      +value.stateId,
      +value.populationYearEnd,
      +value.medianHouseholdIncomeYearEnd,
      +value.medianHouseOrCondoValueYearEnd,
      +value.crimeIndexYearStart,
      +value.crimeIndexYearEnd,
      +value.medianHouseholdIncomeYearStart,
      +value.medianHouseOrCondoValueYearStart,
      +value.populationYearStart
    )


    if (this.city !== undefined) {

      this.dialogRef.close(true);
      this.subscription = this.cityService.updateCity(this.city.id, apiModel).subscribe((result) => {
        this.dialogRef.close(result);
      });

    } else {
      this.subscription = this.cityService.saveNewCity(apiModel).subscribe((result) => {
        this.dialogRef.close(result);
      });

    }

  }

  public createForm(): void {
    this.addEditCityForm = this.fb.group({
      cityName: ['', Validators.required],
      stateId: ['', Validators.required],
      populationYearStart: ['', Validators.required],
      populationInYearStart: ['', Validators.required],
      populationYearEnd: ['', [Validators.required, Validators.min(2017)]],
      populationInYearEnd: ['', Validators.required],
      medianHouseholdIncomeYearStart: ['', Validators.required],
      medianHouseholdIncomeInYearStart: ['', Validators.required],
      medianHouseholdIncomeYearEnd: ['', [Validators.required, Validators.min(2016)]],
      medianHouseholdIncomeInYearEnd: ['', Validators.required],
      medianHouseOrCondoValueYearStart: ['', Validators.required],
      medianHouseOrCondoValueInYearStart: ['', Validators.required],
      medianHouseOrCondoValueYearEnd: ['', [Validators.required, Validators.min(2016)]],
      medianHouseOrCondoValueInYearEnd: ['', Validators.required],
      crimeIndexInYearStart: ['', Validators.required],
      crimeIndexInYearEnd: ['', Validators.required],
      crimeIndexYearStart: ['', Validators.required],
      crimeIndexYearEnd: ['', Validators.required],
      numberOfJobsAdded: ['', Validators.required],
      recentYearJobsGrowthRate: ['', Validators.required],
    });
  }

  public createEditForm(): void {
    this.cityName.patchValue(this.city.cityName);
    this.stateId.patchValue(this.city.state.id);
    this.populationYearEnd.patchValue(this.city.populationYearEnd);
    this.populationYearStart.patchValue(this.city.populationYearStart);
    this.populationInYearStart.patchValue(this.city.populationInYearStart);
    this.populationInYearEnd.patchValue(this.city.populationInYearEnd);
    this.medianHouseholdIncomeInYearEnd.patchValue(this.city.medianHouseholdIncomeInYearEnd);
    this.medianHouseholdIncomeInYearStart.patchValue(this.city.medianHouseholdIncomeInYearStart);
    this.medianHouseholdIncomeYearStart.patchValue(this.city.medianHouseholdIncomeYearStart);
    this.medianHouseholdIncomeYearEnd.patchValue(this.city.medianHouseholdIncomeYearEnd);

    this.medianHouseOrCondoValueInYearEnd.patchValue(this.city.medianHouseOrCondoValueInYearEnd);
    this.medianHouseOrCondoValueYearStart.patchValue(this.city.medianHouseOrCondoValueYearStart);
    this.medianHouseOrCondoValueInYearStart.patchValue(this.city.medianHouseOrCondoValueInYearStart);
    this.medianHouseOrCondoValueYearEnd.patchValue(this.city.medianHouseOrCondoValueYearEnd);

    this.crimeIndexInYearStart.patchValue(this.city.crimeIndexInYearStart);
    this.crimeIndexInYearEnd.patchValue(this.city.crimeIndexInYearEnd);
    this.crimeIndexYearStart.patchValue(this.city.crimeIndexYearStart);
    this.crimeIndexYearEnd.patchValue(this.city.crimeIndexYearEnd);

    this.numberOfJobsAdded.patchValue(this.city.numberOfJobsAdded);
    this.recentYearJobsGrowthRate.patchValue(this.city.recentYearJobsGrowthRate);

  }

  close(): void {
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.alive = false;
    this.subscription.unsubscribe();
  }


  get cityName(): FormControl {
    return this.addEditCityForm.get('cityName') as FormControl;
  }


  get stateId(): FormControl {
    return this.addEditCityForm.get('stateId') as FormControl;
  }

  get populationYearStart(): FormControl {
    return this.addEditCityForm.get('populationYearStart') as FormControl;
  }
  get populationInYearStart(): FormControl {
    return this.addEditCityForm.get('populationInYearStart') as FormControl;
  }

  get populationInYearEnd(): FormControl {
    return this.addEditCityForm.get('populationInYearEnd') as FormControl;
  }

  get populationYearEnd(): FormControl {
    return this.addEditCityForm.get('populationYearEnd') as FormControl;
  }

  get medianHouseholdIncomeYearStart(): FormControl {
    return this.addEditCityForm.get('medianHouseholdIncomeYearStart') as FormControl;
  }

  get medianHouseholdIncomeInYearStart(): FormControl {
    return this.addEditCityForm.get('medianHouseholdIncomeInYearStart') as FormControl;
  }


  get medianHouseholdIncomeYearEnd(): FormControl {
    return this.addEditCityForm.get('medianHouseholdIncomeYearEnd') as FormControl;
  }

  get medianHouseholdIncomeInYearEnd(): FormControl {
    return this.addEditCityForm.get('medianHouseholdIncomeInYearStart') as FormControl;
  }


  get medianHouseOrCondoValueInYearEnd(): FormControl {
    return this.addEditCityForm.get('medianHouseOrCondoValueInYearEnd') as FormControl;
  }

  get medianHouseOrCondoValueYearEnd(): FormControl {
    return this.addEditCityForm.get('medianHouseOrCondoValueYearEnd') as FormControl;
  }


  get medianHouseOrCondoValueInYearStart(): FormControl {
    return this.addEditCityForm.get('medianHouseOrCondoValueInYearStart') as FormControl;
  }

  get medianHouseOrCondoValueYearStart(): FormControl {
    return this.addEditCityForm.get('medianHouseOrCondoValueYearStart') as FormControl;
  }

  get crimeIndexInYearStart(): FormControl {
    return this.addEditCityForm.get('crimeIndexInYearStart') as FormControl;
  }

  get crimeIndexInYearEnd(): FormControl {
    return this.addEditCityForm.get('crimeIndexInYearEnd') as FormControl;
  }

  get crimeIndexYearStart(): FormControl {
    return this.addEditCityForm.get('crimeIndexYearStart') as FormControl;
  }

  get crimeIndexYearEnd(): FormControl {
    return this.addEditCityForm.get('crimeIndexYearEnd') as FormControl;
  }

  get numberOfJobsAdded(): FormControl {
    return this.addEditCityForm.get('numberOfJobsAdded') as FormControl;
  }

  get recentYearJobsGrowthRate(): FormControl {
    return this.addEditCityForm.get('recentYearJobsGrowthRate') as FormControl;
  }


}
