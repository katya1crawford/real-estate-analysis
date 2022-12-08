import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MarketValueEstimatorRoutingModule } from './market-value-estimator-routing.module';
import { MarketValueEstimatorComponent } from './market-value-estimator/market-value-estimator.component';


@NgModule({
  declarations: [
    MarketValueEstimatorComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MarketValueEstimatorRoutingModule
  ]
})
export class MarketValueEstimatorModule { }
