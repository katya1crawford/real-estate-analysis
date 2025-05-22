import { Component } from '@angular/core';
import { ModalOptions } from '../../../dtos/modalOptions';

@Component({
    templateUrl: './modal.component.html'
})
export class ModalComponent {
    public sizeClass: string;

    setModelOptions(options?: ModalOptions) {
        if (options) {
            this.sizeClass = options.sizeClass;
        }
    }
}
