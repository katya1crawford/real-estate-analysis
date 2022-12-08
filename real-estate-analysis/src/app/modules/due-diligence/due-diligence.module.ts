import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { DueDiligenceRoutingModule } from './due-diligence-routing.module';
import { PostAcceptanceComponent } from './post-acceptance/post-acceptance.component';
import { PreOfferComponent } from './pre-offer/pre-offer.component';
import { PurchaseAndSaleAgreementComponent } from './purchase-and-sale-agreement/purchase-and-sale-agreement.component';
import { OperatingStatementModalComponent } from './pre-offer/operating-statement-modal/operating-statement-modal.component';


@NgModule({
  declarations: [
    PostAcceptanceComponent,
    PreOfferComponent,
    PurchaseAndSaleAgreementComponent,
    OperatingStatementModalComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    DueDiligenceRoutingModule
  ]
})
export class DueDiligenceModule { }
