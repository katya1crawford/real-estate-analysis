import { Component, Inject, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { PropertyService } from '../../services/property.service';
import { TotalCashNeededDetailsComponent } from './total-cash-needed-details/total-cash-needed-details.component';

@Component({
  selector: 'app-annual-income-statement',
  templateUrl: './annual-income-statement.component.html',
  styleUrls: ['./annual-income-statement.component.css']
})
export class AnnualIncomeStatementComponent implements OnInit {

  public description = 'Annual Income Statement (Year 1)';
  public property: ReadProperty;
  public displayedColumnsOne: string[] = ['cap-rate', 'cash-on-cash rate', 'purchase price', 'total cash needed'];
  public displayedColumnsTwo: string[] = ['income', 'numbers', 'egi'];
  public displayedColumnsThree: string[] = ['operating-expenses', 'type-amount', 'percent'];
  public mainDataSource: any[];
  public incomeDataSource: any[];
  public operatingExpenseDataSource: any[];
  public otherExpensesDataSource: any[];
  public operatingExpenses: { name: string, amount: number }[] = [];
  clickedRows = new Set<any>();

  constructor(private _bottomSheet: MatBottomSheet,
    private dialogRef: MatDialogRef<AnnualIncomeStatementComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) { this.property = data.property; }

  ngOnInit(): void {
    this.mainDataSource = [this.property];
    this.incomeDataSource = [this.property];
    this.operatingExpenseDataSource = [this.property];
    this.otherExpensesDataSource = [this.property];
    this.setOperatingExpenses();
  }

  public setOperatingExpenses() {
    if (this.property.annualOperatingExpenses.length > 0) {
      for (let i = 0; i < this.property.annualOperatingExpenses.length; i++) {
        let expense = { name: this.property.annualOperatingExpenses[i].operatingExpenseTypeName, amount: this.property.annualOperatingExpenses[i].amount }
        this.operatingExpenses.push(expense);
      }
    }
    return this.operatingExpenses;
  }

  public getPercentOfGoi(amount: number): number {
    const percentOfGoi = (amount / this.property.financialSummary.annualEffectiveGrossIncome) * 100;
    return +percentOfGoi.toFixed(2);
  }

  public openTotalCashDetails(): void {
    this._bottomSheet.open(TotalCashNeededDetailsComponent, { data: this.property });
  }

  close(): void {
    this.dialogRef.close();
  }

}
