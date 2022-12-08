import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';
import { Chart, ChartType } from 'chart.js';


@Component({
  selector: 'app-contract-rent-by-month',
  templateUrl: './contract-rent-by-month.component.html',
  styleUrls: ['./contract-rent-by-month.component.css']
})
export class ContractRentByMonthComponent implements OnChanges {

  constructor() { }

  @Input() rentRollSummary: ReadRentRollSummary;
  public allRentChart: Chart;
  public rentByFloorChart: Chart;
  public configAllRent: any;
  public configRentByFloor: any;
  public lineChartType: ChartType = 'line';
  idAllRent = 0;
  idRentByFloor = 2;



  ngOnChanges(changes: SimpleChanges): void {
    if (changes['rentRollSummary'] !== undefined) {
      const rentRollSummary = changes['rentRollSummary'].currentValue as ReadRentRollSummary;
      this.setAllRentChartSeriesData(rentRollSummary);
      this.setRentByFloorChart(rentRollSummary);
    }
  }

  setAllRentChartSeriesData(rentRollSummary: ReadRentRollSummary): void {
    const chartDataY: any = [];
    const charDataX: any = [];
    const averageRentByMonth = rentRollSummary.averageContractRentByMonth;

    if (averageRentByMonth) {
      const chartPoint = {
        data: averageRentByMonth.map(x => x.averageContractRent)
      };

      const chartDates = {
        dates: averageRentByMonth.map(x => x.category)
      };

      chartDataY.push(chartPoint);
      charDataX.push(chartDates);

      const chartData = {
        labels: charDataX[0].dates,
        datasets: [{
          label: 'Contract Rent',
          data: chartDataY[0].data,
          borderColor: 'rgb(75, 192, 192)',
          tension: 0.1,
          pointStyle: 'circle',
          pointBackgroundColor: 'rgb(75, 192, 192)'

        }]
      };



      const config = {
        type: this.lineChartType,
        data: chartData,
        options: {
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: 'Contract Rent Over Time'
            },
            subtitle: {
              display: true,
              text: 'How does contract rent change over time?',
              padding: {
                bottom: 10
              }
            }
          }
        },
        //plugins: []
      };

      this.configAllRent = config;

    }
  }

  setRentByFloorChart(rentRollSummary: ReadRentRollSummary): void {
    const chartDataY: any = [];
    const charDataX: any = [];
    const averageRentByFloorPlanByMonthGroups = rentRollSummary.floorPlanAverageContractRentByMonthSummary.floorPlanAverageContractRentByMonthGroups;
    const datesAxisX = rentRollSummary.floorPlanAverageContractRentByMonthSummary.categories;

    const chartDates = {
      dates: datesAxisX
    };

    console.log('Categories', datesAxisX);
    console.log('DATA', averageRentByFloorPlanByMonthGroups.map(x => x.items));


    charDataX.push(chartDates);

    const chartData = {
      labels: charDataX[0].dates,
      datasets: [] as Object[]
    };


    if (averageRentByFloorPlanByMonthGroups.length > 0) {
      for (let i = 0; i < averageRentByFloorPlanByMonthGroups.length; i++) {

        chartData.datasets.push(
          {
            label: averageRentByFloorPlanByMonthGroups[i].floorPlan,
            data: averageRentByFloorPlanByMonthGroups[i].items.map(x => x.averageContractRent),
            borderColor: "#" + Math.floor(Math.random() * 16777215).toString(16),
            fill: true,
            tension: 0.1,
            pointStyle: 'circle',
          }
        )

      }

    }



    const config = {
      type: this.lineChartType,
      data: chartData,
      options: {

        responsive: true,
        plugins: {
          title: {
            display: true,
            text: 'Contract Rent By FLoor Plan'
          },
          subtitle: {
            display: true,
            text: 'How does contract rent by floor plan change over time?',
            padding: {
              bottom: 10
            }
          }
        },
        //plugins: [],
      }
    };

    this.configRentByFloor = config;

    console.log("TEST", averageRentByFloorPlanByMonthGroups);
  }

}
