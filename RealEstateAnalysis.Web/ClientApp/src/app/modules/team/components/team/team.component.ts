import { Component } from '@angular/core';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { InsuranceBrokerComponent } from '../../modals/insurance-broker/insurance-broker.component';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';
import { PropertyManagementCompanyComponent } from '../../modals/property-management-company/property-management-company.component';
import { LenderComponent } from '../../modals/lender/lender.component';
import { CommercialRealEstateBrokerComponent } from '../../modals/commercial-real-estate-broker/commercial-real-estate-broker.component';
import { RealEstateAttorneyComponent } from '../../modals/real-estate-attorney/real-estate-attorney.component';
import { RealEstateCpaComponent } from '../../modals/real-estate-cpa/real-estate-cpa.component';
import { RealEstateInteriorDesignerComponent } from '../../modals/real-estate-interior-designer/real-estate-interior-designer.component';

@Component({
    templateUrl: './team.component.html',
    styleUrls: ['./team.component.css']
})
export class TeamComponent {
    constructor(titleService: Title,
        private modalService: ModalService
    ) {
        titleService.setTitle(`${appConfig.businessName}: Team`);
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onInsuranceBrokerClick() {
        this.modalService.show(InsuranceBrokerComponent, this.modalOptions);
    }

    onPropertyManagementCompanyClick() {
        this.modalService.show(PropertyManagementCompanyComponent, this.modalOptions);
    }

    onLenderClick() {
        this.modalService.show(LenderComponent, this.modalOptions);
    }

    onCommercialRealEstateBrokerClick() {
        this.modalService.show(CommercialRealEstateBrokerComponent, this.modalOptions);
    }

    onRealEstateAttorneyClick() {
        this.modalService.show(RealEstateAttorneyComponent, this.modalOptions);
    }

    onRealEstateCpaClick() {
        this.modalService.show(RealEstateCpaComponent, this.modalOptions);
    }

    onRealEstateInteriorDesignerClick() {
        this.modalService.show(RealEstateInteriorDesignerComponent, this.modalOptions);
    }
}
