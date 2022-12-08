import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';
import { Chart, ChartConfiguration, ChartType } from 'chart.js';


@Component({
  selector: 'app-lease-count-by-month',
  templateUrl: './lease-count-by-month.component.html',
  styleUrls: ['./lease-count-by-month.component.css']
})
export class LeaseCountByMonthComponent implements OnChanges {

  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;
  public configLeaseByMonth: any;
  public configLeaseExpSchedule: any;
  public barChartType: ChartType = 'bar';
  public idLeaseCount = 1;
  public idLeaseExp = 3;
  public leaseByMonthChart: any;
  public leaseExpChart: any;



  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['rentRollSummary'] !== undefined) {
      const rentRollSummary = changes['rentRollSummary'].currentValue as ReadRentRollSummary;
      this.setLeaseCountByMonthChart(rentRollSummary);
      this.saveLeaseExpChart(rentRollSummary);
    }
  }


  saveLeaseCountByMonthInstance(chartInstance: Chart): void {
    this.leaseByMonthChart = chartInstance;
  }
  saveLeaseExpInstance(newChartInst: Chart): void {
    this.leaseExpChart = newChartInst;
  }

  setLeaseCountByMonthChart(rentRollSummary: ReadRentRollSummary): void {
    const chartDataY: any = [];
    const charDataX: any = [];
    const newLeasesCount = rentRollSummary.newLeasesCountByMonth;

    if (newLeasesCount) {
      const chartPoint = {
        data: newLeasesCount.map(x => x.count)
      };

      const chartDates = {
        dates: newLeasesCount.map(x => x.category)
      };

      chartDataY.push(chartPoint);
      charDataX.push(chartDates);

      const chartData = {
        labels: charDataX[0].dates,
        datasets: [{

          label: 'Lease Count',
          data: chartDataY[0].data,
          barPercentage: 1,
          barThickness: 16,
          maxBarThickness: 18,
          minBarLength: 10,
          backgroundColor: [
            'rgba(15, 168, 169, 0.8)',

          ],
          borderColor: [
            'rgba(10, 123, 123, 0.8)',
          ],
          borderWidth: 1

        }]
      };

      const max = +Math.round(Math.max(...chartDataY[0].data)) + 2;

      const config = {
        type: this.barChartType,
        data: chartData,
        options: {
          scales: {
            y: { // defining min and max so hiding the dataset does not change scale range
              min: 0,
              max: max
            }
          },
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: 'New Leases By Month',
              padding: {
                top: 20,
                bottom: 10
              },
              font: {
                size: 24
              }
            },
            subtitle: {
              display: true,
              text: 'When were new leases signed?',
              padding: {
                top: 5,
                bottom: 20
              },
              font: {
                size: 16
              }
            }
          }
        },
        // plugins: any[]
      };

      this.configLeaseByMonth = config;

    }

  }

  saveLeaseExpChart(rentRollSummary: ReadRentRollSummary): void {

    const chartDataY: any = [];
    const charDataX: any = [];
    const newLeasesCount = rentRollSummary.leasesExpireCountByMonth;

    if (newLeasesCount) {
      const chartPoint = {
        data: newLeasesCount.map(x => x.count)
      };

      const chartDates = {
        dates: newLeasesCount.map(x => x.category)
      };

      chartDataY.push(chartPoint);
      charDataX.push(chartDates);

      const chartData = {
        labels: charDataX[0].dates,
        datasets: [{

          label: 'Lease Count',
          data: chartDataY[0].data,
          barPercentage: 1,
          barThickness: 16,
          maxBarThickness: 18,
          minBarLength: 10,
          backgroundColor: [
            'rgba(15, 168, 169, 0.8)',

          ],
          borderColor: [
            'rgba(10, 123, 123, 0.8)',
          ],
          borderWidth: 1

        }]
      };

      const max = +Math.round(Math.max(...chartDataY[0].data)) + 2;

      const config = {
        type: this.barChartType,
        data: chartData,
        options: {
          scales: {
            y: { // defining min and max so hiding the dataset does not change scale range
              min: 0,
              max: max
            }
          },
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: 'Lease Expiration Schedule',
              padding: {
                top: 20,
                bottom: 10
              },
              font: {
                size: 24
              }
            },
            subtitle: {
              display: true,
              text: 'When do active leases expire?',
              padding: {
                top: 5,
                bottom: 20
              },
              font: {
                size: 16
              }
            }
          }
        },
        // plugins: []
      };

      this.configLeaseExpSchedule = config;

    }

  }


}
