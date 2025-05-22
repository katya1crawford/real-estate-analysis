import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { MultifamilyProsConsComponent } from '../../modals/multifamily-pros-cons/multifamily-pros-cons.component';

@Component({
    templateUrl: './classifications.component.html',
})
export class ClassificationsComponent {
    constructor(private modalService: ModalService, private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Classifications`);
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onMultifamilyProsAndConsClick() {
        this.modalService.show(MultifamilyProsConsComponent, this.modalOptions);
    }
}
