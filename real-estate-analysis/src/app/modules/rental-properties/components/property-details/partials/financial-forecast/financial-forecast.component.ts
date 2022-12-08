import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ChartType, Chart } from 'chart.js';
import { ReadFinancialForecast } from 'src/app/modules/rental-properties/dtos/reads/readFinancialForecast';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-financial-forecast',
  templateUrl: './financial-forecast.component.html',
  styleUrls: ['./financial-forecast.component.css']
})
export class FinancialForecastComponent implements OnChanges {

  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;
  @Input() public financialForecasts: ReadFinancialForecast[];

  public chart: Chart;
  public lineChartType: ChartType = 'line';
  public chartOptions: any;

  constructor() { }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['property'] !== undefined) {
      const property = changes['property'].currentValue as ReadProperty;
      if (property && this.financialForecasts && !this.pageLoading) {
        this.setAllRentChartSeriesData(this.financialForecasts);
      }
    }
  }

  public saveChartInstance(chartInstance: Chart): void {
    this.chart = chartInstance;
  }

  setAllRentChartSeriesData(financialForecasts: ReadFinancialForecast[]): void {

    const dataCashFlow = financialForecasts.find(x => x.name === 'Net Cash Flow');
    const valuesCashFlow = dataCashFlow?.values;

    const dataNetOperatingIncome = financialForecasts.find(x => x.name === 'Net Operating Income');
    const valuesOperatingIncome = dataNetOperatingIncome?.values;

    const dataEffectiveGrossIncome = financialForecasts.find(x => x.name === 'Effective Gross Income');
    const valuesEffectiveGrossIncome = dataEffectiveGrossIncome?.values;

    const holdingPeriod = financialForecasts[0].values.length;
    const years = [];
    for (let index = 0; index <= holdingPeriod; index++) {
      if (index % 2 === 0) {
        years.push(index);
      }
    }

    const chartData = {
      labels: years,
      datasets: [
        {
          label: 'Net Cash Flow',
          data: valuesCashFlow,
          borderColor: 'rgb(0, 0, 255)',
          backgroundColor: 'rgb(105, 84, 255)',

        },
        {
          label: 'Net Operating Income',
          data: valuesOperatingIncome,
          borderColor: 'rgb(32, 239, 181)',
          backgroundColor: 'rgb(0, 239, 161)'

        },
        {
          label: 'Effective Gross Income',
          data: valuesEffectiveGrossIncome,
          borderColor: 'rgb(255, 48, 177)',
          backgroundColor: 'rgb(240, 93, 181)'
        },

      ]
    };



    const config = {
      type: this.lineChartType,
      data: chartData,
      options: {
        scales: {
          y: {
            display: true,
            title: {
              display: true,
              text: 'Amount($)'
            }
          },
        },
        layout: {
          padding: 20
        },
        responsive: true,
        plugins: {
          plugins: {
            legend: {
              position: 'bottom',
            },
            title: {
              position: 'top',
              display: true,
              text: 'EGI, NOI and NCF Over the Holding Period'
            }
          }
        }
      },
      interaction: {
        mode: 'index',
        intersect: false
      },

      // plugins: [],


    };

    this.chartOptions = config;

  }
}
