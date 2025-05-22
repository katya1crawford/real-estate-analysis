import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';
import { ReadAnnualOperatingExpense } from '../../../../dtos/reads/readAnnualOperatingExpense';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-income-statement',
    styleUrls: ['./income-statement.component.css'],
    templateUrl: './income-statement.component.html'
})
export class IncomeStatementComponent implements OnChanges {
    @Input() public property: ReadProperty;
    @Input() public pageLoading: boolean;

    public combinedAnnualOperatingExpenses: { name, amount }[] = [];

    public chartOptions = {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: 'Operating Expenses'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        credits: {
            enabled: false
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: 'black'
                    }
                }
            }
        },
        series: [{
            name: 'Share',
            colorByPoint: true,
            data: [] as any[]
        }]
    };

    public chart: Highcharts.Chart;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['property'] !== undefined) {
            const property = changes['property'].currentValue as ReadProperty;

            if (property) {
                this.setCombinedAnnualOperatingExpenses(property);
                this.setChartSeriesData(property.annualOperatingExpenses, property.financialSummary.annualPropertyManagementFee);
            }
        }
    }

    public getPercentOfGoi(amount: number): number {
        const percentOfGoi = (amount / this.property.financialSummary.annualEffectiveGrossIncome) * 100;
        return +percentOfGoi.toFixed(2);
    }

    public saveChartInstance(chartInstance: Highcharts.Chart): void {
        this.chart = chartInstance;
    }

    private setCombinedAnnualOperatingExpenses(property: ReadProperty): void {
        this.combinedAnnualOperatingExpenses = [];

        if (property.financialSummary.annualPropertyManagementFee > 0) {
            this.combinedAnnualOperatingExpenses.push({
                amount: property.financialSummary.annualPropertyManagementFee,
                name: 'Property Management Fee'
            });
        }

        if (property.annualOperatingExpenses.length > 0) {
            for (let i = 0; i < property.annualOperatingExpenses.length; i++) {
                this.combinedAnnualOperatingExpenses.push({
                    amount: property.annualOperatingExpenses[i].amount,
                    name: property.annualOperatingExpenses[i].operatingExpenseTypeName
                });
            }
        }

        this.combinedAnnualOperatingExpenses = this.combinedAnnualOperatingExpenses.sort(this.orderCombinedOperatingExpenseByAmountDesc);
    }

    private setChartSeriesData(operatingExpenses: ReadAnnualOperatingExpense[], annualPropertyManagementFee: number): void {
        const chartData: Highcharts.PointOptionsObject[] = [];

        for (let i = 0; i < this.combinedAnnualOperatingExpenses.length; i++) {
            const chartPoint: Highcharts.PointOptionsObject = {
                name: this.combinedAnnualOperatingExpenses[i].name,
                y: this.combinedAnnualOperatingExpenses[i].amount,
                sliced: false,
                selected: false
            };

            if (i === 0) {
                chartPoint.sliced = true;
                chartPoint.selected = true;
            }

            chartData.push(chartPoint);
        }

        if (this.chart) {
            this.chart.series[0].setData(chartData);
        } else {
            this.chartOptions.series[0].data.push(...chartData);
        }
    }

    private orderCombinedOperatingExpenseByAmountDesc(a: any, b: any): number {
        if (a.amount > b.amount) {
            return -1;
        }

        if (a.amount < b.amount) {
            return 1;
        }

        return 0;
    }
}
