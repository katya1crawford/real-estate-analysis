import { Component, Input, ElementRef, NgZone, AfterViewInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-highcharts',
    template: '<ng-content></ng-content>'
})
export class HighchartsComponent implements AfterViewInit, OnDestroy {
    @Input() public options;
    @Output() load = new EventEmitter<Highcharts.Chart>();

    private chart: Highcharts.Chart;

    constructor(private element: ElementRef, private zone: NgZone) { }

    ngAfterViewInit() {
        this.zone.runOutsideAngular(() => {
            this.chart = Highcharts.chart(this.element.nativeElement, this.options);
            this.load.emit(this.chart);
        });
    }

    ngOnDestroy() {
        this.chart.destroy();
    }
}
