import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { MarketValueEstimatorComponent } from './market-value-estimator/market-value-estimator.component';

const routes: Routes = [
  { path: 'market-value-estimator', component: MarketValueEstimatorComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MarketValueEstimatorRoutingModule { }
