import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, UntypedFormArray, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { finalize, takeWhile } from 'rxjs/operators';
import { LookupService } from 'src/app/shared/services/lookup.service';
import { ReadAnnualOperatingExpense } from '../../dtos/reads/readAnnualOperatingExpense';
import { ReadClosingCost } from '../../dtos/reads/readClosingCost';
import { ReadExteriorRepairExpense } from '../../dtos/reads/readExteriorRepairExpense';
import { ReadFinancialSummary } from '../../dtos/reads/readFinancialSummary';
import { ReadGeneralRepairExpense } from '../../dtos/reads/readGeneralRepairExpense';
import { ReadInteriorRepairExpense } from '../../dtos/reads/readInteriorRepairExpense';
import { ReadLookup } from '../../dtos/reads/readLookUp';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { ReadUnitGroup } from '../../dtos/reads/readUnitGroups';
import { WriteAddress } from '../../dtos/writes/writeAddress';
import { WriteFinancialSummary } from '../../dtos/writes/writeFinancialSummary';
import { WriteProperty } from '../../dtos/writes/WriteProperty';
import { WriteUnitGroup } from '../../dtos/writes/writeUnitGroups';
import { CalculatorService } from '../../services/calculator.service';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-add-edit-property',
  templateUrl: './add-edit-property.component.html',
  styleUrls: ['./add-edit-property.component.css']
})
export class AddEditNewPropertyComponent implements OnInit {

  public pageLoading = false;
  public property: null | ReadProperty;
  public addEditPropertyForm: FormGroup;
  public passingColumnsNumbers = [1, 2, 3, 4, 5];
  public interiorRepairExpenseTypes$: Observable<ReadLookup[]>
  public states$: Observable<ReadLookup[]>;
  public propertyStatuses$: Observable<ReadLookup[]>;
  public propertyTypes$: Observable<ReadLookup[]>;
  public generalRepairExpenseTypes$: Observable<ReadLookup[]>;
  public exteriorRepairExpenseTypes$: Observable<ReadLookup[]>;
  public closingCostTypes$: Observable<ReadLookup[]>;
  public operatingExpenseTypes$: Observable<ReadLookup[]>;
  public isAddingEditingProperty = false;
  public alive = true;
  public financialSummary: ReadFinancialSummary;
  public propertyId: number;
  public selectedImage: string | null | File;



  constructor(private fb: FormBuilder,
    private calculatorService: CalculatorService,
    private activatedRoute: ActivatedRoute,
    private propertyService: PropertyService,
    private lookUpService: LookupService,
    private route: Router) {

  }


  ngOnInit(): void {
    this.createNewForm();

    this.interiorRepairExpenseTypes$ = this.lookUpService.getAllInteriorRepairExpenseTypes();
    this.propertyTypes$ = this.lookUpService.getAllPropertyTypes();
    this.propertyStatuses$ = this.lookUpService.getAllPropertyStatuses();
    this.exteriorRepairExpenseTypes$ = this.lookUpService.getAllExteriorRepairExpenseTypes();
    this.states$ = this.lookUpService.getAllStates();
    this.closingCostTypes$ = this.lookUpService.getAllClosingCostTypes();
    this.generalRepairExpenseTypes$ = this.lookUpService.getAllGeneralRepairExpenseTypes();
    this.operatingExpenseTypes$ = this.lookUpService.getAllOperatingExpenseTypes();

    this.activatedRoute.paramMap
      .pipe(
        takeWhile(() => this.alive)
      )
      .subscribe((params: ParamMap) => {
        this.propertyId = +params.get('id');
        if (this.propertyId !== 0 && this.propertyId !== null) {
          this.getData(this.propertyId);
        }

      }, error => {
        console.log(error);
      });
  }


  createFinancialSummary(): WriteFinancialSummary {
    const purchasePrice = +(this.purchasePrice.value);
    const downPayment = +(this.downPayment.value);
    const annualGrossScheduledRentalIncome = +(this.annualGrossScheduledRentalIncome.value);
    const otherAnnualIncome = +(this.otherAnnualIncome.value);
    const annualVacancyRate = +(this.annualVacancyRate.value);
    const annualPropertyManagementFeeRate = +(this.annualPropertyManagementFeeRate.value);
    const loanApr = +(this.loanApr.value);
    const loanYears = +(this.loanYears.value);
    const annualGrossScheduledRentalIncomeGrowthRate = +(this.annualGrossScheduledRentalIncomeGrowthRate.value);
    const annualOperatingExpensesGrowthRate = +(this.annualOperatingExpensesGrowthRate.value);
    const marketCapitalizationRate = +(this.marketCapitalizationRate.value);
    const annualOperatingExpenses = this.getExpensesArray(this.annualOperatingExpenses);
    const closingCosts = this.getExpensesArray(this.closingCosts);
    const exteriorRepairExpenses = this.getExpensesArray(this.exteriorRepairExpenses);
    const generalRepairExpenses = this.getExpensesArray(this.generalRepairExpenses);
    const interiorRepairExpenses = this.getExpensesArray(this.interiorRepairExpenses);

    const financialSummaryApi = new WriteFinancialSummary(purchasePrice, downPayment,
      annualGrossScheduledRentalIncome, otherAnnualIncome, annualVacancyRate, annualPropertyManagementFeeRate,
      loanApr, loanYears, annualGrossScheduledRentalIncomeGrowthRate,
      annualOperatingExpensesGrowthRate, marketCapitalizationRate, annualOperatingExpenses,
      closingCosts, exteriorRepairExpenses, generalRepairExpenses, interiorRepairExpenses);

    console.log(financialSummaryApi);
    return financialSummaryApi;

  }

  public createNewForm(): void {
    this.addEditPropertyForm = this.fb.group({
      thumbnailImage: [''],
      groupName: ['', Validators.required],
      propertyStatusId: ['', Validators.required],
      address: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(500)]],
      city: ['', [Validators.required, Validators.maxLength(500)]],
      stateId: ['', Validators.required],
      zipCode: ['', [Validators.required, Validators.min(5)]],
      propertyTypeId: ['', Validators.required],
      yearBuiltIn: ['', Validators.required],
      buildingSquareFootage: ['', Validators.required],
      lotSquareFootage: ['', Validators.required],
      notes: [''],

      unitGroups: this.fb.array([
        this.buildUnitGroups()
      ]),

      financialSummaryGroup: this.fb.group({
        purchasePrice: [0, [Validators.required, Validators.min(1), Validators.max(1000000000000)]],
        downPaymentPercent: ['', [Validators.min(0), Validators.max(100)]],
        downPayment: [0, [Validators.min(0), Validators.max(1000000000000)]],
        loanApr: ['', [Validators.min(0), Validators.max(100)]],
        loanYears: ['', [Validators.min(0), Validators.max(50)]],

        closingCosts: this.fb.array([
          this.buildClosingCosts()
        ]),

        interiorRepairExpenses: this.fb.array([
          this.buildInteriorExpense()
        ]),


        exteriorRepairExpenses: this.fb.array([
          this.buildExteriorExpense()
        ]),

        generalRepairExpenses: this.fb.array([
          this.buildGeneralExpense()
        ]),

        annualOperatingExpenses: this.fb.array([
          this.buildAnnualOperatingExpense()
        ]),

        annualGrossScheduledRentalIncome: ['', Validators.required],
        otherAnnualIncome: ['', Validators.required],
        annualVacancyRate: ['', Validators.required],
        annualPropertyManagementFeeRate: ['', Validators.required],

        annualGrossScheduledRentalIncomeGrowthRate: ['', Validators.required],
        annualOperatingExpensesGrowthRate: ['', Validators.required],
        marketCapitalizationRate: ['', Validators.required],
      }),


    });
  }

  private getData(id: number) {
    this.pageLoading = true;
    this.propertyService.getProperty(id)
      .pipe(

        finalize(() => {
          this.pageLoading = false
        })
      )
      .subscribe(data => {
        this.property = data,
          this.createEditForm(this.property);
      });
  }


  public createEditForm(property: ReadProperty): void {
    this.groupName.patchValue(property.groupName);
    this.propertyStatusId.patchValue(property.propertyStatus.id);
    this.thumbnailImage.patchValue(property.thumbnailImageBase64);
    this.address.patchValue(property.address.address);
    this.city.patchValue(property.address.city);
    this.stateId.patchValue(property.address.state.id);
    this.zipCode.patchValue(property.address.zipCode);
    this.addEditPropertyForm.setControl('unitGroups', this.setValuesUnitGroups(property.unitGroups))
    this.propertyTypeId.patchValue(property.propertyType.id);
    this.yearBuiltIn.patchValue(property.yearBuiltIn);
    this.buildingSquareFootage.patchValue(property.buildingSquareFootage);
    this.lotSquareFootage.patchValue(property.lotSquareFootage);
    this.loanApr.patchValue(property.loanApr);
    this.downPayment.patchValue(property.downPayment);
    this.loanYears.patchValue(property.loanYears);
    this.purchasePrice.patchValue(property.purchasePrice);
    this.annualVacancyRate.patchValue(property.annualVacancyRate);
    this.annualPropertyManagementFeeRate.patchValue(property.annualPropertyManagementFeeRate);
    this.annualGrossScheduledRentalIncome.patchValue(property.annualGrossScheduledRentalIncome);
    this.otherAnnualIncome.patchValue(property.otherAnnualIncome);
    this.annualGrossScheduledRentalIncomeGrowthRate.patchValue(property.annualGrossScheduledRentalIncomeGrowthRate);
    this.annualOperatingExpensesGrowthRate.patchValue(property.annualOperatingExpensesGrowthRate);
    this.marketCapitalizationRate.patchValue(property.marketCapitalizationRate);
    this.notes.patchValue(property.notes);
    this.downPaymentPercent.patchValue(((property.downPayment / property.purchasePrice) * 100).toFixed(2));
    this.financialSummaryGroup.setControl('exteriorRepairExpenses', this.setValuesExteriorRepairExpenses(property.exteriorRepairExpenses));
    this.financialSummaryGroup.setControl('annualOperatingExpenses', this.setValuesOperatingRepairExpenses(property.annualOperatingExpenses));
    this.financialSummaryGroup.setControl('generalRepairExpenses', this.setValuesGeneralRepairExpenses(property.generalRepairExpenses));
    this.financialSummaryGroup.setControl('interiorRepairExpenses', this.setValuesInteriorRepairExpenses(property.interiorRepairExpenses));
    this.financialSummaryGroup.setControl('closingCosts', this.setValuesClosingCosts(property.closingCosts));



  }

  onResetForm(): void {
    this.addEditPropertyForm.reset();
    this.createNewForm();
    this.financialSummary = new ReadFinancialSummary();
  }


  onRefreshRatesClick(e: Event): void {
    e.preventDefault();
    const api: WriteFinancialSummary = this.createFinancialSummary();
    this.calculatorService.getFinancialSummary(api).subscribe(data => {
      this.financialSummary = data;
    },
      error => {
        console.log(error);
      }
    );

  }

  onDeleteThumbnailImage(e: Event) {
    e.preventDefault();
    this.thumbnailImage.setValue(null);
  }

  onAddEditPropertyFormSubmit(): void {
    const apiModel = new WriteProperty(
      this.thumbnailImage.value,
      + this.yearBuiltIn.value,
      +this.buildingSquareFootage.value,
      +this.lotSquareFootage.value,
      + this.propertyTypeId.value,
      +this.propertyStatusId.value,
      this.notes.value,
      this.writeAddressGroup(),
      this.writeUnitGroups(),
      this.writeArrayGroup(this.annualOperatingExpenses),
      +this.annualOperatingExpensesGrowthRate.value,
      +this.marketCapitalizationRate.value,
      +this.annualGrossScheduledRentalIncomeGrowthRate.value,
      +this.annualGrossScheduledRentalIncome.value,
      +this.annualVacancyRate.value,
      +this.annualPropertyManagementFeeRate.value,
      this.writeArrayGroup(this.closingCosts),
      +this.downPayment.value,
      this.writeArrayGroup(this.exteriorRepairExpenses),
      this.writeArrayGroup(this.generalRepairExpenses),
      this.writeArrayGroup(this.interiorRepairExpenses),
      +this.loanApr.value,
      +this.loanYears.value,
      +this.otherAnnualIncome.value,
      +this.purchasePrice.value,
      this.groupName.value,

    );

    this.isAddingEditingProperty = true;

    if (this.propertyId !== 0 && this.propertyId !== null) {
      this.propertyService.updateProperty(this.propertyId, apiModel).subscribe(() => {
        this.isAddingEditingProperty = false;
        this.route.navigate(['rentals/listing']);
      },
        (error: HttpErrorResponse) => {
          if (error.status === 400) {
            console.log(error.message, error.name);
            this.isAddingEditingProperty = false;

          }
        }
      );

    } else {
      this.propertyService.saveNewProperty(apiModel)
        .pipe(takeWhile(() => this.alive),
          finalize(() => {
            this.isAddingEditingProperty = false;
            this.route.navigate(['rentals/listing']);
          }))
        .subscribe(() => { this.onResetForm() },
          (error: HttpErrorResponse) => {
            if (error.status === 400) {
              console.log(error.message, error.name);
            }
          }
        );
    }
  }



  public onThumbnailImageChange(event: any): void {
    event.preventDefault();

    if (event.target.files.length > 0) {
      const file: File | null = event.target.files[0] as File;

      this.selectedImage = file;
      this.thumbnailImage.setValue(file);

    } else {
      this.selectedImage = null;
      this.thumbnailImage.setValue(null);
    }

    this.addEditPropertyForm.markAsDirty();
  }

  getExpensesArray(formArray: FormArray): any {
    const expensesArray: any[] = [];
    formArray.controls.forEach(control => {
      expensesArray.push(control.value);
    });

    return expensesArray;
  }

  writeAddressGroup(): WriteAddress {
    const address = new WriteAddress(this.address.value, this.city.value, +this.stateId.value, this.zipCode.value);
    return address;
  }

  writeUnitGroups(): WriteUnitGroup[] {
    const groups: WriteUnitGroup[] = [];

    for (let index = 0; index < this.unitGroups.length; index++) {
      const id = +(this.unitGroups.controls[index].get('id') as FormControl).value;
      const numberOfUnits = +(this.unitGroups.controls[index].get('numberOfUnits') as FormControl).value;
      const squareFootage = +(this.unitGroups.controls[index].get('squareFootage') as FormControl).value;
      const bedrooms = +(this.unitGroups.controls[index].get('bedrooms') as FormControl).value;
      const bathrooms = +(this.unitGroups.controls[index].get('bathrooms') as FormControl).value;
      groups.push(new WriteUnitGroup(id, numberOfUnits, squareFootage, bedrooms, bathrooms));
    }
    return groups;
  }


  writeArrayGroup(formArray: FormArray): any[] {
    const expenses: any = [];
    formArray.controls.forEach(control => {
      expenses.push(control.value);
    });
    return expenses;
  }

  setValuesUnitGroups(array: ReadUnitGroup[]): FormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          id: [0, Validators.required],
          numberOfUnits: value.numberOfUnits,
          squareFootage: value.squareFootage,
          bedrooms: value.bedrooms,
          bathrooms: value.bathrooms
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildUnitGroups());
    }
    return arrayOfValues;
  }



  setValuesOperatingRepairExpenses(array: ReadAnnualOperatingExpense[]): FormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          operatingExpenseTypeId: value.operatingExpenseTypeId,
          amount: value.amount
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildAnnualOperatingExpense());
    }
    return arrayOfValues;
  }



  setValuesExteriorRepairExpenses(array: ReadExteriorRepairExpense[]): FormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          exteriorRepairExpenseTypeId: value.exteriorRepairExpenseTypeId,
          amount: value.amount
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildExteriorExpense());
    }
    return arrayOfValues;
  }


  setValuesGeneralRepairExpenses(array: ReadGeneralRepairExpense[]): FormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          generalRepairExpenseTypeId: value.generalRepairExpenseTypeId,
          amount: value.amount
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildGeneralExpense());
    }
    return arrayOfValues;
  }


  setValuesInteriorRepairExpenses(array: ReadInteriorRepairExpense[]): FormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          interiorRepairExpenseTypeId: value.interiorRepairExpenseTypeId,
          amount: value.amount
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildInteriorExpense());
    }
    return arrayOfValues;
  }


  setValuesClosingCosts(array: ReadClosingCost[]): UntypedFormArray {
    const arrayOfValues = new UntypedFormArray([]);

    if (array.length > 0) {
      array.forEach(value => {
        arrayOfValues.push(this.fb.group({
          closingCostTypeId: value.closingCostTypeId,
          amount: value.amount
        })
        );
      });
    } else {
      arrayOfValues.push(this.buildClosingCosts());
    }
    return arrayOfValues;
  }


  buildClosingCosts(): FormGroup {
    return this.fb.group({
      closingCostTypeId: ['1', Validators.required],
      amount: ['0', Validators.required]
    });
  }

  buildInteriorExpense(): FormGroup {
    return this.fb.group({
      interiorRepairExpenseTypeId: ['1', Validators.required],
      amount: ['0', Validators.required]
    });
  }

  buildAnnualOperatingExpense(): UntypedFormGroup {
    return this.fb.group({
      operatingExpenseTypeId: ['1', Validators.required],
      amount: ['0', Validators.required]
    });
  }


  buildExteriorExpense(): UntypedFormGroup {
    return this.fb.group({
      exteriorRepairExpenseTypeId: ['1', Validators.required],
      amount: ['0', Validators.required]
    });
  }

  buildGeneralExpense(): UntypedFormGroup {
    return this.fb.group({
      generalRepairExpenseTypeId: ['1', Validators.required],
      amount: ['0', Validators.required]
    });
  }

  buildUnitGroups(): UntypedFormGroup {
    return this.fb.group({
      id: [0, Validators.required],
      numberOfUnits: ['', Validators.required],
      squareFootage: ['', Validators.required],
      bedrooms: ['', Validators.required],
      bathrooms: ['', Validators.required],
    });
  }


  editUnitGroups(unitGroups: ReadUnitGroup[]): FormArray<any> {
    let unitGroupsArray = new FormArray([]);
    if (unitGroups.length > 0) {
      unitGroups.forEach(value => {
        return unitGroupsArray.push(this.fb.group({
          id: value.id,
          numberOfUnits: value.numberOfUnits,
          squareFootage: value.squareFootage,
          bedrooms: value.bedrooms,
          bathrooms: value.bathrooms
        })
        );
      });
    } else {
      unitGroupsArray.push(this.buildUnitGroups());
    }
    return unitGroupsArray;
  }


  onRemoveFieldClick(formArray: FormArray, index: number, $event: Event): void {
    $event.preventDefault();
    if (formArray.length > 1) {
      formArray.removeAt(index);
    }
  }

  onAddArrayGroup(formArray: FormArray, e: Event): void {
    e.preventDefault();
    switch (formArray) {
      case this.closingCosts:
        this.closingCosts.push(this.buildClosingCosts());
        break;
      case this.generalRepairExpenses:
        this.generalRepairExpenses.push(this.buildGeneralExpense());
        break;
      case this.interiorRepairExpenses:
        this.interiorRepairExpenses.push(this.buildInteriorExpense());
        break;
      case this.exteriorRepairExpenses:
        this.exteriorRepairExpenses.push(this.buildExteriorExpense());
        break;
      case this.unitGroups:
        this.unitGroups.push(this.buildUnitGroups());
        break;
      case this.annualOperatingExpenses:
        this.annualOperatingExpenses.push(this.buildAnnualOperatingExpense());
        break;


      default:
    }
  }


  get thumbnailImage(): FormControl {
    return this.addEditPropertyForm.get('thumbnailImage') as FormControl;
  }

  get address(): FormControl {
    return this.addEditPropertyForm.get('address') as FormControl;
  }

  get city(): FormControl {
    return this.addEditPropertyForm.get('city') as FormControl;
  }

  get stateId(): FormControl {
    return this.addEditPropertyForm.get('stateId') as FormControl;
  }

  get zipCode(): FormControl {
    return this.addEditPropertyForm.get('zipCode') as FormControl;
  }

  get propertyTypeId(): FormControl {
    return this.addEditPropertyForm.get('propertyTypeId') as FormControl;
  }

  get propertyStatusId(): FormControl {
    return this.addEditPropertyForm.get('propertyStatusId') as FormControl;
  }

  get lotSquareFootage(): FormControl {
    return this.addEditPropertyForm.get('lotSquareFootage') as FormControl;
  }

  get buildingSquareFootage(): FormControl {
    return this.addEditPropertyForm.get('buildingSquareFootage') as FormControl;
  }

  get yearBuiltIn(): FormControl {
    return this.addEditPropertyForm.get('yearBuiltIn') as FormControl;
  }

  get unitGroups(): FormArray {
    return this.addEditPropertyForm.get('unitGroups') as FormArray;
  }

  get financialSummaryGroup(): FormGroup {
    return this.addEditPropertyForm.get('financialSummaryGroup') as FormGroup;
  }

  get purchasePrice(): FormControl {
    return this.financialSummaryGroup.get('purchasePrice') as FormControl;
  }

  get downPaymentPercent(): FormControl {
    return this.financialSummaryGroup.get('downPaymentPercent') as FormControl;
  }

  get downPayment(): FormControl {
    return this.financialSummaryGroup.get('downPayment') as FormControl;
  }

  get loanApr(): FormControl {
    return this.financialSummaryGroup.get('loanApr') as FormControl;
  }

  get loanYears(): FormControl {
    return this.financialSummaryGroup.get('loanYears') as FormControl;
  }

  get annualGrossScheduledRentalIncome(): FormControl {
    return this.financialSummaryGroup.get('annualGrossScheduledRentalIncome') as FormControl;
  }

  get otherAnnualIncome(): FormControl {
    return this.financialSummaryGroup.get('otherAnnualIncome') as FormControl;
  }

  get annualVacancyRate(): FormControl {
    return this.financialSummaryGroup.get('annualVacancyRate') as FormControl;
  }

  get annualPropertyManagementFeeRate(): FormControl {
    return this.financialSummaryGroup.get('annualPropertyManagementFeeRate') as FormControl;
  }

  get annualGrossScheduledRentalIncomeGrowthRate(): FormControl {
    return this.financialSummaryGroup.get('annualGrossScheduledRentalIncomeGrowthRate') as FormControl;
  }

  get annualOperatingExpensesGrowthRate(): FormControl {
    return this.financialSummaryGroup.get('annualOperatingExpensesGrowthRate') as FormControl;
  }

  get marketCapitalizationRate(): FormControl {
    return this.financialSummaryGroup.get('marketCapitalizationRate') as FormControl;
  }

  get annualOperatingExpenses(): FormArray {
    return this.financialSummaryGroup.get('annualOperatingExpenses') as FormArray;
  }

  get closingCosts(): FormArray {
    return this.financialSummaryGroup.get('closingCosts') as FormArray;
  }

  get exteriorRepairExpenses(): FormArray {
    return this.financialSummaryGroup.get('exteriorRepairExpenses') as FormArray;
  }

  get generalRepairExpenses(): FormArray {
    return this.financialSummaryGroup.get('generalRepairExpenses') as FormArray;
  }

  get interiorRepairExpenses(): FormArray {
    return this.financialSummaryGroup.get('interiorRepairExpenses') as FormArray;
  }

  get notes(): FormControl {
    return this.addEditPropertyForm.get('notes') as FormControl;
  }

  get groupName(): FormControl {
    return this.addEditPropertyForm.get('groupName') as FormControl;
  }

}
