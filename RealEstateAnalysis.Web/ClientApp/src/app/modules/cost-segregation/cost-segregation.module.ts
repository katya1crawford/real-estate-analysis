import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CostSegregationRoutingModule } from './cost-segregation-routing.module';
import { CostSegregationComponent } from './components/cost-segregation/cost-segregation.component';


@NgModule({
    declarations: [
        CostSegregationComponent
    ],
    imports: [
        CommonModule,
        CostSegregationRoutingModule
    ]
})
export class CostSegregationModule { }
