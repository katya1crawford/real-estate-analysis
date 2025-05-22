import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';
import { ReadFinancialForecast } from '../../../../dtos/reads/readFinancialForecast';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-financial-forecast',
    styleUrls: ['./financial-forecast.component.css'],
    templateUrl: './financial-forecast.component.html'
})
export class FinancialForecastComponent implements OnChanges {
    @Input() public property: ReadProperty;
    @Input() public financialForecasts: ReadFinancialForecast[];
    @Input() public pageLoading: boolean;

    public chartOptions: Highcharts.Options = {
        title: {
            text: 'EGI, NOI and NCF Over the Holding Period'
        },
        subtitle: {
            text: ''
        },
        yAxis: {
            title: {
                text: 'Amount ($)'
            }
        },
        legend: {
            layout: 'horizontal',
            align: 'center',
            verticalAlign: 'bottom'
        },
        credits: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<span style="color: {point.color}">\u25CF</span> Year: <b>{point.key}</b><br/>',
            pointFormat: '<span style="color: {point.color}">\u25CF</span> {series.name}: <b>${point.y:,.2f}</b>'
        },
        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                pointStart: 1
            }
        },
        series: []
    };

    public chart: Highcharts.Chart;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['property'] !== undefined) {
            const property = changes['property'].currentValue as ReadProperty;
            this.setChartSubtitleText(property);
        }

        if (changes['financialForecasts'] !== undefined) {
            const financialForecasts = changes['financialForecasts'].currentValue as ReadFinancialForecast[];
            this.setChartSeriesData(financialForecasts);
        }
    }

    public saveChartInstance(chartInstance: Highcharts.Chart): void {
        this.chart = chartInstance;
    }

    private setChartSeriesData(financialForecasts: ReadFinancialForecast[]): void {
        const chartData: any = [];

        if (financialForecasts) {
            for (let i = 0; i < financialForecasts.length; i++) {
                if (financialForecasts[i].id === 5 || financialForecasts[i].id === 7 || financialForecasts[i].id === 9) {
                    const chartPoint = {
                        name: financialForecasts[i].name,
                        data: financialForecasts[i].values
                    };

                    chartData.push(chartPoint);
                }
            }
        }

        if (this.chart) {
            for (let i = 0; i < chartData.length; i++) {
                this.chart.series[i].update(chartData[i]);
            }
        } else {
            this.chartOptions.series.push(...chartData);
        }
    }

    private setChartSubtitleText(property: ReadProperty): void {
        if (property) {
            this.chartOptions.subtitle.text = `Rental Income Growth Rate: ${property.annualGrossScheduledRentalIncomeGrowthRate}%
                & Expenses Growth Rate: ${property.annualOperatingExpensesGrowthRate}%`;

            if (this.chart) {
                this.chart.setTitle(null, { text: this.chartOptions.subtitle.text });
            }
        }
    }
}
