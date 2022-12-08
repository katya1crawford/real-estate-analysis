import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SummaryComponent } from './summary/summary.component';
import { SharedModule } from 'src/app/modules/shared/shared.module';



@NgModule({
  declarations: [
    SummaryComponent
  ],
  exports: [
    SummaryComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ErrorSummaryModule { }
