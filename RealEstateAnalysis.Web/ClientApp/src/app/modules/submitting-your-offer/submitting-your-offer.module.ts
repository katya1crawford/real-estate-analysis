import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SubmittingYourOfferRoutingModule } from './submitting-your-offer-routing.module';
import { SubmittingYourOfferComponent } from './components/submitting-your-offer/submitting-your-offer.component';


@NgModule({
  declarations: [
    SubmittingYourOfferComponent
  ],
  imports: [
    CommonModule,
    SubmittingYourOfferRoutingModule
  ]
})
export class SubmittingYourOfferModule { }
