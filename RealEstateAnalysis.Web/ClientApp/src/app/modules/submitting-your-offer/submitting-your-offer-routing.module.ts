import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { SubmittingYourOfferComponent } from './components/submitting-your-offer/submitting-your-offer.component';

const routes: Routes = [
    { path: 'submitting-your-offer', component: SubmittingYourOfferComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SubmittingYourOfferRoutingModule { }
