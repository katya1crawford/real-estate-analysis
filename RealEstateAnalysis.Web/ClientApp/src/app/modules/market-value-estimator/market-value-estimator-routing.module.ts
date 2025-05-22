import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { MarketValueEstimatorComponent } from './components/market-value-estimator.component';


const routes: Routes = [
    { path: 'market/market-value-estimator', component: MarketValueEstimatorComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MarketValueEstimatorRoutingModule { }
