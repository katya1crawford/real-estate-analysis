import { Component, Input, ElementRef, NgZone, AfterViewInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { BubbleDataPoint, Chart, ChartConfiguration, ChartTypeRegistry, registerables, ScatterDataPoint } from 'chart.js';
import * as $ from "jquery"

Chart.register(...registerables);

// declare var $: any;

@Component({
  selector: 'app-charts',
  template: `<ng-template > </ng-template>`

})
export class ChartsComponent implements AfterViewInit, OnDestroy {
  @Input() public options: ChartConfiguration<keyof ChartTypeRegistry, (number | ScatterDataPoint | BubbleDataPoint | null)[], unknown>;
  @Output() load = new EventEmitter<any>();
  @Input() public width: string;
  @Input() public height: string;


  constructor(private element: ElementRef, private zone: NgZone) { }

  public chart: Chart;


  ngAfterViewInit(): void {
    this.zone.runOutsideAngular(() => {
      let canvas: any = document.createElement("canvas");
      canvas.style.width = this.width;
      canvas.style.height = this.height;
      let element = this.element.nativeElement as HTMLElement;
      element.appendChild(canvas);
      this.chart = new Chart(canvas, this.options);
      this.load.emit(this.chart);
    });
  }

  ngOnDestroy(): void {
    this.chart.destroy();
  }
}
