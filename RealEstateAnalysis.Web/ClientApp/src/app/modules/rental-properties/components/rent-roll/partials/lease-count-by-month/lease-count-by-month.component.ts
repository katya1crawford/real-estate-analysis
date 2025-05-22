import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { XAxisOptions } from 'highcharts';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';

@Component({
    selector: 'app-lease-count-by-month',
    templateUrl: './lease-count-by-month.component.html',
    styleUrls: ['./lease-count-by-month.component.css']
})
export class LeaseCountByMonthComponent implements OnChanges {
    @Input() public rentRollSummary: ReadRentRollSummary;

    public newLeasesOptions: Highcharts.Options = {
        chart: {
            type: 'column'
        },
        title: {
            text: 'New Leases By Month'
        },
        subtitle: {
            text: 'When were new leases signed?'
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            title: {
                text: 'Lease Count'
            }
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: '<span style="color: {point.color};">\u25CF</span> Lease Count: <b>{point.y}</b>'
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false
        },
        series: []
    };

    public leasesExpireOptions: Highcharts.Options = {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Lease Expiration Schedule'
        },
        subtitle: {
            text: 'When do active leases expire?'
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            title: {
                text: 'Lease Count'
            }
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: '<span style="color: {point.color};">\u25CF</span> Lease Count: <b>{point.y}</b>'
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false
        },
        series: []
    };

    public newLeasesChart: Highcharts.Chart;
    public leasesExpireChart: Highcharts.Chart;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['rentRollSummary'] !== undefined) {
            const rentRollSummary = changes['rentRollSummary'].currentValue as ReadRentRollSummary;
            this.setNewLeasesChartSeriesData(rentRollSummary);
            this.setLeasesExpireChartSeriesData(rentRollSummary);
        }
    }

    public saveNewLeasesChartInstance(chartInstance: Highcharts.Chart): void {
        this.newLeasesChart = chartInstance;
    }

    public saveLeasesExpireChartInstance(chartInstance: Highcharts.Chart): void {
        this.leasesExpireChart = chartInstance;
    }

    private setNewLeasesChartSeriesData(rentRollSummary: ReadRentRollSummary) {
        const chartData: any = [];
        const newLeasesByMonth = rentRollSummary.newLeasesCountByMonth;

        if (newLeasesByMonth) {
            const chartPoint = {
                data: newLeasesByMonth.map(x => x.count)
            };

            (this.newLeasesOptions.xAxis as XAxisOptions).categories = newLeasesByMonth.map(x => x.category);

            chartData.push(chartPoint);
        }

        if (this.newLeasesChart) {
            for (let i = 0; i < chartData.length; i++) {
                this.newLeasesChart.series[i].update(chartData[i]);
            }
        } else {
            this.newLeasesOptions.series.push(...chartData);
        }
    }

    private setLeasesExpireChartSeriesData(rentRollSummary: ReadRentRollSummary) {
        const chartData: any = [];
        const leasesExpireByMonth = rentRollSummary.leasesExpireCountByMonth;

        if (leasesExpireByMonth) {
            const chartPoint = {
                data: leasesExpireByMonth.map(x => x.count)
            };

            (this.leasesExpireOptions.xAxis as XAxisOptions).categories = leasesExpireByMonth.map(x => x.category);

            chartData.push(chartPoint);
        }

        if (this.leasesExpireChart) {
            for (let i = 0; i < chartData.length; i++) {
                this.leasesExpireChart.series[i].update(chartData[i]);
            }
        } else {
            this.leasesExpireOptions.series.push(...chartData);
        }
    }
}
