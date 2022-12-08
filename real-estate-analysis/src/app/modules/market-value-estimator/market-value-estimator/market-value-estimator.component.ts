import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
// import { AddEditPropertyComponent } from '../../rental-properties/dialogs/add-edit-property/add-edit-property.component';
import { AnnualIncomeStatementComponent } from '../../rental-properties/dialogs/annual-income-statement/annual-income-statement.component';
import { DeleteRentalPropertyComponent } from '../../rental-properties/dialogs/delete-rental-property/delete-rental-property.component';
import { ReadProperty } from '../../rental-properties/dtos/reads/readProperty';
import { PropertyService } from '../../rental-properties/services/property.service';

@Component({
  selector: 'app-market-value-estimator',
  templateUrl: './market-value-estimator.component.html',
  styleUrls: ['./market-value-estimator.component.css']
})
export class MarketValueEstimatorComponent implements OnInit {

  public groupNames: any[];
  public subjectPropertiesLookups: any[];
  public isRunningReport = false;
  public comparableProperties: ReadProperty[];
  public serverError = false;
  public marketValueForm: FormGroup;
  public subjectProperty: ReadProperty | null;

  constructor(private fb: FormBuilder,
    private propertyService: PropertyService,
    private route: Router,
    private dialog: MatDialog,) { this.createMarketEstimatorForm(); }


  ngOnInit(): void {
    this.propertyService.getAllGroupNames().subscribe(data => this.groupNames = data,
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          console.log(error);
        } else {
          this.serverError = true;
        }
      });

    this.propertyService.getAllSubjectPropertyLookups().subscribe(data => this.subjectPropertiesLookups = data,
      (error: HttpErrorResponse) => {
        if (error.status === 400) {
          console.log(error);
        } else {
          this.serverError = true;
        }
      });
  }


  createMarketEstimatorForm(): void {

    this.marketValueForm = this.fb.group({
      groupName: ['', Validators.required],
      subjectPropertyId: ['', Validators.required]
    });

  }

  onMarketValueFormSubmit(): void {
    this.isRunningReport = true;

    // tslint:disable-next-line: max-line-length
    this.propertyService.getByGroupName(this.groupName.value)
      .subscribe(data => {
        this.comparableProperties = data,
          this.isRunningReport = false;
      },

        (error: HttpErrorResponse) => {
          if (error.status === 400) {
            console.log(error);
          } else {
            this.serverError = true;
            this.isRunningReport = false;
          }
        });

    const subjectPropertyId = +this.subjectPropertyId.value;

    if (subjectPropertyId !== 0) {
      this.propertyService.getProperty(subjectPropertyId)
        .subscribe(response => this.subjectProperty = response,
          (error: HttpErrorResponse) => {
            if (error.status === 400) {
              console.log(error);
            } else {
              this.serverError = true;
            }
          });
    } else {
      this.subjectProperty = null;
    }



  }

  onAddNewClick(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '85vh';
    dialogConfig.maxWidth = '90%';
    dialogConfig.scrollStrategy?.disable;
    // const dialogRef = this.dialog.open(AddEditPropertyComponent, dialogConfig);

  }

  onIncomeStatementClick(property: ReadProperty): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '800px';
    dialogConfig.width = '700px';

    dialogConfig.data = {
      property: property
    };

    this.dialog.open(AnnualIncomeStatementComponent, dialogConfig);
  }


  onEditSubjectPropertyClick(property: ReadProperty): void {
    this.route.navigate(['/add-edit-property', property.id])


  }


  onDeleteClick(property: ReadProperty): any {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '30%';
    dialogConfig.maxWidth = '50%';
    dialogConfig.data = {
      property: property
    };

    this.dialog.open(DeleteRentalPropertyComponent, dialogConfig);
  }

  getTotalActiveUnitsSqFts(property: ReadProperty): number {
    const vacancyLossSqFts = property.totalUnitsSquareFootage * (property.annualVacancyRate / 100);
    const totalActiveUnitsSqFts = property.totalUnitsSquareFootage - vacancyLossSqFts;
    return totalActiveUnitsSqFts;
  }


  getRatio(x: number, y: number): number {
    return +(x / y).toFixed(2);
  }

  getMinimumSalesPrice(): number {
    const salePrices = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
    return Math.min(...salePrices);

  }

  getMinimumNumberOfUnits(): number {
    const numberOfUnits = this.comparableProperties.map(x => x.totalUnits);
    return Math.min(...numberOfUnits);

  }

  getMinimumPricePerUnit(): number {
    const pricesPerUnit = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
    return Math.min(...pricesPerUnit);
  }

  getMinimumAverageUnitSize(): number {
    const minAverageUnitsSize = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
    return Math.min(...minAverageUnitsSize);
  }

  getMinimumRentalIncomePerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const minRentalIncomesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
    return Math.min(...minRentalIncomesPerUnitSqFt);

  }

  getMinimumOperatingExpensesPerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const minimumOperatingExpensesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, x.totalUnitsSquareFootage))
    return Math.min(...minimumOperatingExpensesPerUnitSqFt);
  }

  getMinimumCapRate(): number {
    const minCapRates = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
    return Math.min(...minCapRates);
  }

  getMinimumGRM(): number {
    const minGRMs = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
    return Math.min(...minGRMs);

  }

  getMinimumYearBuilt(): number {
    const values = this.comparableProperties.map(x => x.yearBuiltIn);
    return Math.min(...values);
  }

  getMinimumPricePerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const minPricesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
    return Math.min(...minPricesPerUnitSqFt);
  }

  getMaximumYearBuilt(): number {
    const values = this.comparableProperties.map(x => x.yearBuiltIn);
    return Math.max(...values);
  }

  getMaximumSalesPrice(): number {
    const salePrices = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
    return Math.max(...salePrices);
  }

  getMaximumNumberOfUnits(): number {
    const numberOfUnits = this.comparableProperties.map(x => x.totalUnits);
    return Math.max(...numberOfUnits);
  }

  getMaximumPricePerUnit(): number {
    const pricesPerUnit = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
    return Math.max(...pricesPerUnit);
  }

  getMaximumAverageUnitSize(): number {
    const maxAverageUnitsSize = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
    return Math.max(...maxAverageUnitsSize);
  }

  getMaximumPricePerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const maxPricesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
    return Math.max(...maxPricesPerUnitSqFt);
  }


  getMaximumRentalIncomePerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const maxRentalIncomesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
    return Math.max(...maxRentalIncomesPerUnitSqFt);
  }


  getMaximumOperatingExpensesPerUnitSqFt(): number {
    // tslint:disable-next-line: max-line-length
    const maximumOperatingExpensesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, x.totalUnitsSquareFootage));
    return Math.max(...maximumOperatingExpensesPerUnitSqFt);
  }

  getMaximumCapRate(): number {
    const maxCapRates = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
    return Math.min(...maxCapRates);
  }

  getMaximumGRM(): number {
    const maxGRMs = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
    return Math.min(...maxGRMs);
  }


  getYearBuiltAverage(): number {
    const values = this.comparableProperties.map(x => x.yearBuiltIn);
    return this.getAverageValue(values);
  }

  getSalesPriceAverage(): number {
    const salePricesAvrg = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
    return this.getAverageValue(salePricesAvrg);
  }

  getNumberOfUnitsAverage(): number {
    const numberOfUnitsAvrg = this.comparableProperties.map(x => x.totalUnits);
    return this.getAverageValue(numberOfUnitsAvrg);
  }

  getPricePerUnitAverage(): number {
    const pricesPerUnitAvrg = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
    return this.getAverageValue(pricesPerUnitAvrg);
  }

  getAverageUnitSizeAverage(): number {
    const unitSizes = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
    return this.getAverageValue(unitSizes);
  }

  getPricePerUnitSqFtAverage(): number {
    // tslint:disable-next-line: max-line-length
    const avrgPricesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
    return this.getAverageValue(avrgPricesPerUnitSqFt);
  }

  getOperatingExpensesPerUnitSqFtAverage(): number {
    // tslint:disable-next-line: max-line-length
    const avrgOperatingExpensesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, x.totalUnitsSquareFootage));
    return this.getAverageValue(avrgOperatingExpensesPerUnitSqFt);
  }

  getGRMAverage(): number {
    const avrgGRMs = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
    return this.getAverageValue(avrgGRMs);
  }

  getRentalIncomePerUnitSqFtAverage(): number {
    // tslint:disable-next-line: max-line-length
    const avrgRentalIncomesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
    return this.getAverageValue(avrgRentalIncomesPerUnitSqFt);
  }

  getCapRatesAverage(): number {
    const avrgCapRates = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
    return this.getAverageValue(avrgCapRates);
  }

  getSalesPriceMedian(): number {
    const salePricesMedian = this.comparableProperties.map(x => x.financialSummary.totalPurchasePrice);
    return this.getMedianValue(salePricesMedian);
  }

  getNumberOfUnitsMedian(): number {
    const medianNumberOfUnits = this.comparableProperties.map(x => x.totalUnits);
    return this.getMedianValue(medianNumberOfUnits);
  }

  getPricePerUnitMedian(): number {
    const medianPricesPerUnit = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnits));
    return this.getMedianValue(medianPricesPerUnit);
  }

  getAverageUnitSizeMedian(): number {
    const medianAverageUnitsSize = this.comparableProperties.map(x => this.getRatio(x.totalUnitsSquareFootage, x.totalUnits));
    return this.getMedianValue(medianAverageUnitsSize);
  }

  getPricePerUnitSqFtMedian(): number {
    // tslint:disable-next-line: max-line-length
    const medianPricesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalPurchasePrice, x.totalUnitsSquareFootage));
    return this.getMedianValue(medianPricesPerUnitSqFt);
  }

  getRentalIncomePerUnitSqFtMedian(): number {
    // tslint:disable-next-line: max-line-length
    const medianRentalIncomesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.annualGrossScheduledRentalIncome, x.totalUnitsSquareFootage));
    return this.getMedianValue(medianRentalIncomesPerUnitSqFt);
  }

  getOperatingExpensesPerUnitSqFtMedian(): number {
    // tslint:disable-next-line: max-line-length
    const medianOperatingExpensesPerUnitSqFt = this.comparableProperties.map(x => this.getRatio(x.financialSummary.totalAnnualOperatingExpenses, x.totalUnitsSquareFootage));
    return this.getMedianValue(medianOperatingExpensesPerUnitSqFt);
  }

  getGRMMedian(): number {
    const medianGRMs = this.comparableProperties.map(x => x.financialSummary.grossRentMultiplier);
    return this.getMedianValue(medianGRMs);
  }

  getCapRatesMedian(): number {
    const medianCapRates = this.comparableProperties.map(x => x.financialSummary.annualCapRate);
    return this.getMedianValue(medianCapRates);
  }

  getYearBuiltMedian(): number {
    const values = this.comparableProperties.map(x => x.yearBuiltIn);
    return this.getMedianValue(values);
  }

  getMedianValue(values: number[]): number {
    let middle = Math.floor(values.length / 2);
    values = [...values].sort((a, b) => a - b);
    return values.length % 2 !== 0 ? values[middle] : (values[middle - 1] + values[middle]) / 2;
  }

  getAverageValue(values: number[]): number {
    const sum = values.reduce((a, b) => a + b, 0);
    const average = sum / values.length;
    return +average.toFixed(2);
  }



  get groupName(): FormControl {
    return this.marketValueForm.get('groupName') as FormControl;
  }

  get subjectPropertyId(): FormControl {
    return this.marketValueForm.get('subjectPropertyId') as FormControl;
  }

}
