import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';
import * as Highcharts from 'highcharts';
import { XAxisOptions } from 'highcharts';

@Component({
    selector: 'app-contract-rent-by-month',
    templateUrl: './contract-rent-by-month.component.html',
    styleUrls: ['./contract-rent-by-month.component.css']
})
export class ContractRentByMonthComponent implements OnChanges {
    @Input() public rentRollSummary: ReadRentRollSummary;

    public allRentChartOptions: Highcharts.Options = {
        title: {
            text: 'Contract Rent Over Time'
        },
        subtitle: {
            text: 'How does contract rent change over time?'
        },
        yAxis: {
            title: {
                text: 'Amount ($)'
            }
        },
        xAxis: {
            categories: []
        },
        legend: {
            enabled: false
        },
        credits: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: '<span style="color: {point.color};">\u25CF</span> Contract Rent: <b>${point.y:,.2f}</b>'
        },
        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                }
            }
        },
        series: []
    };

    public rentByFloorPlanChartOptions: Highcharts.Options = {
        title: {
            text: 'Contract Rent By Floor PLan'
        },
        subtitle: {
            text: 'How does contract rent by floor plan change over time?'
        },
        yAxis: {
            title: {
                text: 'Amount ($)'
            }
        },
        xAxis: {
            categories: []
        },
        credits: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: '<span style="color: {point.color}">\u25CF</span> Floor Plan: <b>{series.name}</b><br /> <span style="color: {point.color}">\u25CF</span> Contract Rent: <b>${point.y:,.2f}</b>'
        },
        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                connectNulls: true
            }
        },
        series: []
    };

    public allRentChart: Highcharts.Chart;
    public rentByFloorPlanChart: Highcharts.Chart;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['rentRollSummary'] !== undefined) {
            const rentRollSummary = changes['rentRollSummary'].currentValue as ReadRentRollSummary;
            this.setAllRentChartSeriesData(rentRollSummary);
            this.setRentByFloorPlanChartSeriesData(rentRollSummary);
        }
    }

    public saveAllRentChartInstance(chartInstance: Highcharts.Chart): void {
        this.allRentChart = chartInstance;
    }

    public saveRentByFloorPlanChartInstance(chartInstance: Highcharts.Chart): void {
        this.rentByFloorPlanChart = chartInstance;
    }

    private setAllRentChartSeriesData(rentRollSummary: ReadRentRollSummary): void {
        const chartData: any = [];
        const averageRentByMonth = rentRollSummary.averageContractRentByMonth;

        if (averageRentByMonth) {
            const chartPoint = {
                data: averageRentByMonth.map(x => x.averageContractRent)
            };

            (this.allRentChartOptions.xAxis as XAxisOptions).categories = averageRentByMonth.map(x => x.category);

            chartData.push(chartPoint);
        }

        if (this.allRentChart) {
            for (let i = 0; i < chartData.length; i++) {
                this.allRentChart.series[i].update(chartData[i]);
            }
        } else {
            this.allRentChartOptions.series.push(...chartData);
        }
    }

    private setRentByFloorPlanChartSeriesData(rentRollSummary: ReadRentRollSummary): void {
        const chartData: any = [];
        const averageRentByFloorPlanByMonthGroups = rentRollSummary.floorPlanAverageContractRentByMonthSummary.floorPlanAverageContractRentByMonthGroups;

        if (averageRentByFloorPlanByMonthGroups) {
            const categories = rentRollSummary.floorPlanAverageContractRentByMonthSummary.categories;
            (this.rentByFloorPlanChartOptions.xAxis as XAxisOptions).categories = rentRollSummary.floorPlanAverageContractRentByMonthSummary.categories;

            for (let i = 0; i < averageRentByFloorPlanByMonthGroups.length; i++) {
                const data = [];

                categories.forEach(category => {
                    const item = averageRentByFloorPlanByMonthGroups[i].items.find(x => x.category === category);

                    if (item !== undefined) {
                        data.push(item.averageContractRent);
                    } else {
                        data.push(null);
                    }
                });

                const chartPoint = {
                    name: averageRentByFloorPlanByMonthGroups[i].floorPlan,
                    data: data
                };

                chartData.push(chartPoint);
            }
        }

        if (this.rentByFloorPlanChart) {
            for (let i = 0; i < chartData.length; i++) {
                this.rentByFloorPlanChart.series[i].update(chartData[i]);
            }
        } else {
            this.rentByFloorPlanChartOptions.series.push(...chartData);
        }
    }
}
