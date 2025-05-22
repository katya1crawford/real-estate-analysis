import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FindingGoodDealRoutingModule } from './finding-good-deal-routing.module';
import { FindingGoodDealComponent } from './components/finding-good-deal/finding-good-deal.component';


@NgModule({
  declarations: [
    FindingGoodDealComponent
  ],
  imports: [
    CommonModule,
    FindingGoodDealRoutingModule
  ]
})
export class FindingGoodDealModule { }
