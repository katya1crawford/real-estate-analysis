import { Component } from '@angular/core';
import { LeasesComponent } from '../../modals/pre-offer/leases/leases.component';
import { RentRollComponent } from '../../modals/pre-offer/rent-roll/rent-roll.component';
import { ServiceContractsComponent } from '../../modals/pre-offer/service-contracts/service-contracts.component';
import { InventoriesComponent } from '../../modals/pre-offer/inventories/inventories.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { OperatingStatementsComponent } from '../../modals/pre-offer/operating-statements/operating-statements.component';
import { OtherReportsComponent } from '../../modals/pre-offer/other-reports/other-reports.component';
import { SellerDisclosureStatementComponent } from '../../modals/pre-offer/seller-disclosure-statement/seller-disclosure-statement.component';
import { WalkThroughComponent } from '../../modals/post-acceptance/walk-through/walk-through.component';
import { UtilitiesStatementsComponent } from '../../modals/pre-offer/utilities-statements/utilities-statements.component';
import { TenantInterviewsComponent } from '../../modals/post-acceptance/tenant-interviews/tenant-interviews.component';
import { SellerTaxReturnComponent } from '../../modals/pre-offer/seller-tax-return/seller-tax-return.component';
import { ModalOptions } from 'src/app/shared/dtos/modalOptions';
import { HoaDocumentsComponent } from '../../modals/pre-offer/hoa-documents/hoa-documents.component';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';
import { AsBuiltDrawingsComponent } from '../../modals/pre-offer/as-built-drawings/as-built-drawings.component';
import { WarrantiesComponent } from '../../modals/pre-offer/warranties/warranties.component';
import { OperatingBudgetComponent } from '../../modals/pre-offer/operating-budget/operating-budget.component';
import { CertificateOfOccupancyComponent } from '../../modals/pre-offer/certificate-of-occupancy/certificate-of-occupancy.component';
import { TenantBackgroundCheckReportsComponent } from '../../modals/pre-offer/tenant-background-check-reports/tenant-background-check-reports.component';
import { WhyIsOwnerSellingComponent } from '../../modals/pre-offer/why-is-owner-selling/why-is-owner-selling.component';
import { BuildingAndFireCodeViolationsComponent } from '../../modals/pre-offer/building-and-fire-code-violations/building-and-fire-code-violations.component';
import { RecordsOfAllMaintenanceComponent } from '../../modals/pre-offer/records-of-all-maintenance/records-of-all-maintenance.component';
import { NonconformingUseComponent } from '../../modals/pre-offer/nonconforming-use/nonconforming-use.component';
import { RightOfFirstRefusalOrOptionComponent } from '../../modals/pre-offer/right-of-first-refusal-or-option/right-of-first-refusal-or-option.component';
import { TenantEstoppelCertificatesComponent } from '../../modals/pre-offer/tenant-estoppel-certificates/tenant-estoppel-certificates.component';

@Component({
    templateUrl: './pre-offer.component.html',
    styleUrls: ['./pre-offer.component.css']
})
export class PreOfferComponent {
    constructor(titleService: Title, private modalService: ModalService) {
        titleService.setTitle(`${appConfig.businessName}: Pre-Offer: During Negotiations of PSA`);
    }

    private modalOptions = { sizeClass: 'modal-xl' } as ModalOptions;

    onLeasesClick() {
        this.modalService.show(LeasesComponent, this.modalOptions);
    }

    onRentRollClick() {
        this.modalService.show(RentRollComponent, this.modalOptions);
    }

    onServiceContractsClick() {
        this.modalService.show(ServiceContractsComponent, this.modalOptions);
    }

    onInventoriesClick() {
        this.modalService.show(InventoriesComponent, this.modalOptions);
    }

    onOperatingStatementsClick() {
        this.modalService.show(OperatingStatementsComponent, this.modalOptions);
    }

    onOtherReportsClick() {
        this.modalService.show(OtherReportsComponent, this.modalOptions);
    }

    onSellerDisclosureStatementClick() {
        this.modalService.show(SellerDisclosureStatementComponent, this.modalOptions);
    }

    onUtilitiesStatementsClick() {
        this.modalService.show(UtilitiesStatementsComponent, this.modalOptions);
    }

    onSellerTaxReturnsClick() {
        this.modalService.show(SellerTaxReturnComponent, this.modalOptions);
    }

    onHoaDocumentsClick() {
        this.modalService.show(HoaDocumentsComponent, this.modalOptions);
    }

    onAsBuiltDrawingsClick() {
        this.modalService.show(AsBuiltDrawingsComponent, this.modalOptions);
    }

    onWarrantiesClick() {
        this.modalService.show(WarrantiesComponent, this.modalOptions);
    }

    onOperatingBudgetClick() {
        this.modalService.show(OperatingBudgetComponent, this.modalOptions);
    }

    onCertificateOfOccupancyClick() {
        this.modalService.show(CertificateOfOccupancyComponent, this.modalOptions);
    }

    onTenantBackgroundCheckReportsClick() {
        this.modalService.show(TenantBackgroundCheckReportsComponent, this.modalOptions);
    }

    onWhyIsOwnerSellingClick() {
        this.modalService.show(WhyIsOwnerSellingComponent, this.modalOptions);
    }

    onBuildingAndFireCodeViolationsClick() {
        this.modalService.show(BuildingAndFireCodeViolationsComponent, this.modalOptions);
    }

    onRecordsOfAllMaintenanceClick() {
        this.modalService.show(RecordsOfAllMaintenanceComponent, this.modalOptions);
    }

    onIsNonconformingUseClick() {
        this.modalService.show(NonconformingUseComponent, this.modalOptions);
    }

    onRightOfFirstRefusalOrOptionClick() {
        this.modalService.show(RightOfFirstRefusalOrOptionComponent, this.modalOptions);
    }

    onTenantEstoppelCertificatesClick() {
        this.modalService.show(TenantEstoppelCertificatesComponent, this.modalOptions);
    }
}
