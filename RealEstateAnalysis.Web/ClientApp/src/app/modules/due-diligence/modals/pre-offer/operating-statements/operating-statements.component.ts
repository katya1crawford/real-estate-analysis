import { Component } from '@angular/core';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { CapitalExpendituresComponent } from '../capital-expenditures/capital-expenditures.component';

@Component({
    templateUrl: './operating-statements.component.html',
    styleUrls: ['./operating-statements.component.css']
})
export class OperatingStatementsComponent {
    constructor(private modalService: ModalService) {
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onCapitalExpendituresClick() {
        this.modalService.show(CapitalExpendituresComponent, this.modalOptions);
    }
}
