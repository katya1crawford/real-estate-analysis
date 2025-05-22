import { Directive, AfterViewInit, OnDestroy, ElementRef, NgZone, Input, TemplateRef, ViewContainerRef, EmbeddedViewRef } from '@angular/core';

declare var $: any;

@Directive({
    selector: '[appPopover]'
})
export class PopoverDirective implements AfterViewInit, OnDestroy {
    constructor(private elementRef: ElementRef, private zone: NgZone, private viewContainerRef: ViewContainerRef) { }

    @Input('appPopover') popoverContent: string | TemplateRef<any>;

    private embeddedViewRef: EmbeddedViewRef<any>;

    ngAfterViewInit() {
        if (this.viewContainerRef && this.popoverContent) {
            let content;

            if (this.popoverContent instanceof TemplateRef) {
                this.embeddedViewRef = this.viewContainerRef.createEmbeddedView(this.popoverContent);
                this.embeddedViewRef.detectChanges();
                content = document.createElement('div');
                this.embeddedViewRef.rootNodes.forEach(x => content.appendChild(x));
            } else {
                content = this.popoverContent;
            }

            this.zone.runOutsideAngular(() => {
                $(this.elementRef.nativeElement).popover({
                    content: content,
                    html: true
                });
            });
        }
    }

    ngOnDestroy() {
        $(this.elementRef.nativeElement).popover('dispose');

        if (this.embeddedViewRef) {
            this.embeddedViewRef.destroy();
        }
    }
}
