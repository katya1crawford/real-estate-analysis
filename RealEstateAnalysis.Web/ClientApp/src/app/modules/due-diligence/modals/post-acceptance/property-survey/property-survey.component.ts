import { Component } from '@angular/core';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { EncroachmentComponent } from '../encroachment/encroachment.component';

@Component({
    templateUrl: './property-survey.component.html',
})
export class PropertySurveyComponent {
    constructor(private modalService: ModalService) {
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onEncroachmentClick() {
        this.modalService.show(EncroachmentComponent, this.modalOptions);
    }
}
