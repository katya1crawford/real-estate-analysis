import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BestRentalsRoutingModule } from './best-rentals-routing.module';
import { BestRentalsComponent } from './components/best-rentals/best-rentals.component';


@NgModule({
  declarations: [
    BestRentalsComponent
  ],
  imports: [
    CommonModule,
    BestRentalsRoutingModule
  ]
})
export class BestRentalsModule { }
