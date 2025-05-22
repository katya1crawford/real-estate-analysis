import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { ContingenciesComponent } from '../../modals/purchase-and-sale-agreement/contingencies/contingencies.component';

@Component({
    templateUrl: './purchase-and-sale-agreement.component.html',
    styleUrls: ['./purchase-and-sale-agreement.component.css']
})
export class PurchaseAndSaleAgreementComponent {
    constructor(titleService: Title, private modalService: ModalService) {
        titleService.setTitle(`${appConfig.businessName}: Purchase &amp; Sale Agreement (PSA)`);
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onSeeRecommendedContingenciesListClick() {
        this.modalService.show(ContingenciesComponent, this.modalOptions);
    }
}
