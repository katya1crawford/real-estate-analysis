import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuardService } from '../../shared/services/auth-guard.service';
import { PreOfferComponent } from './components/pre-offer/pre-offer.component';
import { PostAcceptanceComponent } from './components/post-acceptance/post-acceptance.component';
import { PurchaseAndSaleAgreementComponent } from './components/purchase-and-sale-agreement/purchase-and-sale-agreement.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'due-diligence/pre-offer', component: PreOfferComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
        { path: 'due-diligence/post-acceptance', component: PostAcceptanceComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
        { path: 'due-diligence/purchase-and-sale-agreement', component: PurchaseAndSaleAgreementComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
    ])],
    exports: [RouterModule]
})
export class DueDiligenceRoutingModule { }
