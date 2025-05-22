import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EntityStructureRoutingModule } from './entity-structure-routing.module';
import { SeriesLlcComponent } from './components/series-llc/series-llc.component';
import { TaxationComponent } from './components/taxation/taxation.component';
import { WyomingEntityComponent } from './components/wyoming-entity/wyoming-entity.component';
import { LlcComponent } from './components/llc/llc.component';
import { EstatePlanningComponent } from './components/estate-planning/estate-planning.component';
import { LimitedPartnershipComponent } from './components/limited-partnership/limited-partnership.component';


@NgModule({
  declarations: [
    SeriesLlcComponent,
    TaxationComponent,
    WyomingEntityComponent,
    LlcComponent,
    EstatePlanningComponent,
    LimitedPartnershipComponent
  ],
  imports: [
    CommonModule,
    EntityStructureRoutingModule
  ]
})
export class EntityStructureModule { }
