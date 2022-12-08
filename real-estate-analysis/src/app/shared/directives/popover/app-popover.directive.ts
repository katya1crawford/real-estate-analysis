import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { Directive, ElementRef, HostListener, Input, OnDestroy, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PopoverService } from './popover.service';

@Directive({
  selector: '[appPopover]'
})
export class AppPopoverDirective implements OnDestroy, OnInit {

  constructor(
    private elementRef: ElementRef,
    private overlay: Overlay,
    private vcr: ViewContainerRef,
    private popoverService: PopoverService
  ) { }


  @Input()
  appPopover: TemplateRef<object>;

  alive = true;
  private unsubscribe = new Subject();
  private overlayRef: OverlayRef;


  ngOnInit(): void {
    this.createOverlay();
    this.popoverService.getState().subscribe(resp => {
      if (resp) {
        this.detachOverlay();
      }
    });
  }

  @HostListener("click") click() {
    this.attachOverlay();
  }


  ngOnDestroy(): void {
    this.detachOverlay();
    this.unsubscribe.next(false);
    this.unsubscribe.complete();
  }

  private createOverlay(): void {
    const scrollStrategy = this.overlay.scrollStrategies.block();
    const positionStrategy = this.overlay.position()
      .flexibleConnectedTo(this.elementRef)
      .withPositions([{
        originX: "start",
        originY: "center",
        overlayX: "start",
        overlayY: "center"
      }, {

        originX: 'start',
        originY: 'center',
        overlayX: 'start',
        overlayY: 'center',
      }]);



    this.overlayRef = this.overlay.create({
      positionStrategy,
      scrollStrategy,
      hasBackdrop: true,
      backdropClass: ""
    });

    this.overlayRef
      .backdropClick()
      .pipe(takeUntil(this.unsubscribe))
      .subscribe(() => {
        this.detachOverlay();
      });
  }


  private attachOverlay(): void {
    if (!this.overlayRef.hasAttached()) {
      const periodSelectorPortal = new TemplatePortal(
        this.appPopover,
        this.vcr
      );

      this.overlayRef.attach(periodSelectorPortal);
    }
  }

  private detachOverlay(): void {
    if (this.overlayRef.hasAttached()) {
      this.overlayRef.detach();
    }
  }

}
