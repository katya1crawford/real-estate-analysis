import { Directive, ElementRef, AfterViewInit, NgZone, OnDestroy, Input } from '@angular/core';

declare var $: any;

@Directive({
    selector: '[appTooltip]'
})
export class TooltipDirective implements AfterViewInit, OnDestroy {
    constructor(private elementRef: ElementRef, private zone: NgZone) { }

    ngAfterViewInit() {
        this.zone.runOutsideAngular(() => {
            $(this.elementRef.nativeElement).tooltip();
        });
    }

    ngOnDestroy() {
        $(this.elementRef.nativeElement).tooltip('dispose');
    }
}
