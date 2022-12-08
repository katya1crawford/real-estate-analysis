import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, MinLengthValidator, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { data } from 'jquery';
import { Observable, Subscription } from 'rxjs';
import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { ReadNeighborhood } from '../../dtos/reads/readNeighborhood';
import { WriteNeighborhood } from '../../dtos/writes/writeNeighborhood';
import { CityService } from '../../services/cityService/city.service';
import { NeighborhoodService } from '../../services/neighborhoodService/neighborhood.service';

@Component({
  selector: 'app-add-neighborhood',
  templateUrl: './add-neighborhood.component.html',
  styleUrls: ['./add-neighborhood.component.css']
})
export class AddNeighborhoodComponent implements OnInit, OnDestroy {

  description = "Add or Edit Neighborhood";
  neighborhood: ReadNeighborhood;
  isAddingEditingNeighborhood = false;
  addEditNeighborhoodForm: FormGroup;
  states$: Observable<ReadState[]>;
  subscription: Subscription;

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddNeighborhoodComponent>,
    private cityService: CityService,
    private neighborhoodService: NeighborhoodService,

    @Inject(MAT_DIALOG_DATA) data: any | null | undefined) {
    this.createAddNewNeighborhoodForm();
    data ? this.neighborhood = data.neighborhood : this.neighborhood = undefined;
  }



  ngOnDestroy(): void {
    if (this.subscription !== undefined) {
      this.subscription.unsubscribe();
    }
  }

  ngOnInit(): void {

    this.states$ = this.cityService.getAllStates();

    if (this.neighborhood !== undefined) {
      this.createEditForm();
    }

  }

  save(): void {
    const apiModel = new WriteNeighborhood(
      this.neighborhoodName.value,
      this.city.value,
      +this.medianHouseholdIncome.value,
      +this.medianContractRent.value,
      +this.cityUnemploymentRate.value,
      +this.neighborhoodUnemploymentRate.value,
      +this.povertyRate.value,
      +this.ethnicMixLargestSlicePercent.value,
      +this.homesMedianDaysOnMarket.value,
      +this.stateId.value);

    if (this.neighborhood !== undefined) {

      this.dialogRef.close(true);
      this.subscription = this.neighborhoodService.updateNeighborhood(this.neighborhood.id, apiModel).subscribe((result) => {
        this.dialogRef.close(result);
      });
    } else {
      this.subscription = this.neighborhoodService.saveNewNeighborhood(apiModel).subscribe((result) => {
        this.dialogRef.close(result);
      });

    }
  }

  close(): void {
    this.dialogRef.close();
  }

  public createAddNewNeighborhoodForm(): void {

    this.addEditNeighborhoodForm = this.fb.group({
      neighborhoodName: ['', [Validators.required, Validators.minLength(2)]],
      stateId: ['', Validators.required],
      city: ['', Validators.required],
      medianHouseholdIncome: ['', Validators.required],
      medianContractRent: ['', Validators.required],
      cityUnemploymentRate: ['', Validators.required],
      povertyRate: ['', Validators.required],
      ethnicMixLargestSlicePercent: ['', Validators.required],
      homesMedianDaysOnMarket: ['', Validators.required],
      neighborhoodUnemploymentRate: ['', Validators.required],

    });
  }


  createEditForm(): void {
    this.neighborhoodName.patchValue(this.neighborhood.neighborhoodName);
    this.stateId.patchValue(this.neighborhood.state.id);
    this.city.patchValue(this.neighborhood.city);
    this.medianHouseholdIncome.patchValue(this.neighborhood.medianHouseholdIncome);
    this.medianContractRent.patchValue(this.neighborhood.medianContractRent);
    this.cityUnemploymentRate.patchValue(this.neighborhood.cityUnemploymentRate);
    this.povertyRate.patchValue(this.neighborhood.povertyRate);
    this.ethnicMixLargestSlicePercent.patchValue(this.neighborhood.ethnicMixLargestSlicePercent);
    this.homesMedianDaysOnMarket.patchValue(this.neighborhood.homesMedianDaysOnMarket);
    this.neighborhoodUnemploymentRate.patchValue(this.neighborhood.neighborhoodUnemploymentRate);
  }




  get stateId(): FormControl {
    return this.addEditNeighborhoodForm.get('stateId') as FormControl;
  }


  get city(): FormControl {
    return this.addEditNeighborhoodForm.get('city') as FormControl;
  }

  get neighborhoodName(): FormControl {
    return this.addEditNeighborhoodForm.get('neighborhoodName') as FormControl;
  }


  get medianHouseholdIncome(): FormControl {
    return this.addEditNeighborhoodForm.get('medianHouseholdIncome') as FormControl;
  }

  get medianContractRent(): FormControl {
    return this.addEditNeighborhoodForm.get('stateId') as FormControl;
  }


  get cityUnemploymentRate(): FormControl {
    return this.addEditNeighborhoodForm.get('cityUnemploymentRate') as FormControl;
  }

  get neighborhoodUnemploymentRate(): FormControl {
    return this.addEditNeighborhoodForm.get('neighborhoodUnemploymentRate') as FormControl;
  }

  get povertyRate(): FormControl {
    return this.addEditNeighborhoodForm.get('povertyRate') as FormControl;
  }

  get ethnicMixLargestSlicePercent(): FormControl {
    return this.addEditNeighborhoodForm.get('ethnicMixLargestSlicePercent') as FormControl;
  }

  get homesMedianDaysOnMarket(): FormControl {
    return this.addEditNeighborhoodForm.get('homesMedianDaysOnMarket') as FormControl;
  }




}
