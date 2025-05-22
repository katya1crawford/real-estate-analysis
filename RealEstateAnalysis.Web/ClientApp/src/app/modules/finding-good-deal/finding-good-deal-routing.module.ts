import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { FindingGoodDealComponent } from './components/finding-good-deal/finding-good-deal.component';

const routes: Routes = [
    { path: 'finding-good-deal', component: FindingGoodDealComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FindingGoodDealRoutingModule { }
