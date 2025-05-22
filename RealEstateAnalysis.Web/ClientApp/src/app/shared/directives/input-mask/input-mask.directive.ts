import { Directive, ElementRef, Input, AfterViewInit, NgZone } from '@angular/core';
import Inputmask from 'inputmask';

@Directive({
    selector: '[appInputMask]'
})
export class InputMaskDirective implements AfterViewInit {
    @Input('appInputMask') mask: string;

    constructor(private element: ElementRef, private zone: NgZone) { }

    ngAfterViewInit() {
        this.zone.runOutsideAngular(() => {
            if (this.mask === 'money') {
                Inputmask('currency', {
                    allowMinus: false,
                    prefix: '',
                    placeholder: '0',
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false,
                    enforceDigitsOnBlur: true
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'integer') {
                Inputmask('integer', {
                    groupSeparator: ',',
                    autoGroup: true,
                    allowMinus: true,
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'year') {
                Inputmask('integer', {
                    groupSeparator: '',
                    autoGroup: true,
                    allowMinus: true,
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'percentage') {
                Inputmask('percentage', {
                    digits: 2,
                    suffix: '',
                    allowMinus: false,
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false,
                    placeholder: ''
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'percentage-allow-minus') {
                Inputmask('percentage', {
                    digits: 2,
                    suffix: '',
                    allowMinus: true,
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false,
                    min: -100,
                    placeholder: ''
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'decimal-1') {
                Inputmask('decimal', {
                    digits: 1,
                    allowMinus: false,
                    autoUnmask: true,
                    removeMaskOnSubmit: true,
                    rightAlign: false
                }).mask(this.element.nativeElement);
            } else if (this.mask === 'zipCode') {
                Inputmask({
                    mask: '99999[-9999]',
                    clearIncomplete: true,
                    placeholder: '',
                    autoUnmask: false,
                    removeMaskOnSubmit: false,
                    greedy: false
                }).mask(this.element.nativeElement);
            }
        });
    }
}
