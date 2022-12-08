import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { PostAcceptanceComponent } from './post-acceptance/post-acceptance.component';
import { PreOfferComponent } from './pre-offer/pre-offer.component';
import { PurchaseAndSaleAgreementComponent } from './purchase-and-sale-agreement/purchase-and-sale-agreement.component';

const routes: Routes = [{ path: 'due-diligence/pre-offer', component: PreOfferComponent, canActivate: [AuthGuardService] },
{ path: 'due-diligence/post-acceptance', component: PostAcceptanceComponent, canActivate: [AuthGuardService] },
{ path: 'due-diligence/purchase-and-sale-agreement', component: PurchaseAndSaleAgreementComponent, canActivate: [AuthGuardService] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DueDiligenceRoutingModule { }
