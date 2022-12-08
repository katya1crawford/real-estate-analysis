import { Component, Input, OnChanges, ChangeDetectionStrategy, OnInit, SimpleChanges, ChangeDetectorRef } from '@angular/core';
import { Chart, ChartType } from 'chart.js';
import { ReadAnnualOperatingExpense } from 'src/app/modules/rental-properties/dtos/reads/readAnnualOperatingExpense';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-income-statement',
  templateUrl: './income-statement.component.html',
  styleUrls: ['./income-statement.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class IncomeStatementComponent implements OnChanges, OnInit {

  constructor(private cd: ChangeDetectorRef) { }

  @Input() property: ReadProperty;
  @Input() pageLoading: boolean;

  chartOptions: any;
  public barChartType: ChartType = 'pie';
  chart: Chart;
  public combinedAnnualOperatingExpenses: any[];

  ngOnInit(): void {
    if (!this.pageLoading) {
      this.setCombinedOperatingExpenses();
    }

  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['property'] !== undefined) {
      const property = changes['property'].currentValue as ReadProperty;
      if (property && !this.pageLoading) {
        this.setOperatingExpensesChart(property);
      }
    }
  }

  setOperatingExpensesChart(property: ReadProperty): void {

    if (!this.pageLoading && property.annualOperatingExpenses) {
      const operatingExpenses = property.annualOperatingExpenses;
      const labels = operatingExpenses.map(x => x.operatingExpenseTypeName);
      const data = operatingExpenses.map(x => x.amount);

      const chartData = {
        labels: labels,
        datasets: [{

          label: 'Operating Expenses',
          data: data,

          backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)',
            'rgb(106, 90, 205)',
            'rgb(0, 117, 186)',
            'rgb(119, 239, 170)',
            'rgb(222, 88, 31)',
            'rgb(255, 0, 0)'
          ],

          hoverOffset: 4

        }]

      };

      const config = {
        type: this.barChartType,
        data: chartData,
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: 'top',
            },
            title: {
              display: true,
              text: 'Operating Expenses'
            }
          },
          layout: {
            padding: 10
          },
        }
      };

      this.chartOptions = config;
    }
  }

  setCombinedOperatingExpenses(): void {
    this.combinedAnnualOperatingExpenses = [];

    if (this.property.financialSummary.annualPropertyManagementFee > 0) {
      this.combinedAnnualOperatingExpenses.push({
        amount: this.property.financialSummary.annualPropertyManagementFee,
        name: 'Property Management Fee'
      });
    }

    if (this.property.annualOperatingExpenses.length > 0) {
      for (let i = 0; i < this.property.annualOperatingExpenses.length; i++) {
        this.combinedAnnualOperatingExpenses.push({
          amount: this.property.annualOperatingExpenses[i].amount,
          name: this.property.annualOperatingExpenses[i].operatingExpenseTypeName
        });
      }
    }

  }

  getPercentOfGoi(amount: number): number {
    const percentOfGoi = (amount / this.property.financialSummary.annualEffectiveGrossIncome) * 100;
    return +percentOfGoi.toFixed(2);

  }
}
