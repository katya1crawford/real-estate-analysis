import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { EstatePlanningComponent } from './components/estate-planning/estate-planning.component';
import { LimitedPartnershipComponent } from './components/limited-partnership/limited-partnership.component';
import { LlcComponent } from './components/llc/llc.component';
import { SeriesLlcComponent } from './components/series-llc/series-llc.component';
import { TaxationComponent } from './components/taxation/taxation.component';
import { WyomingEntityComponent } from './components/wyoming-entity/wyoming-entity.component';

const routes: Routes = [
    { path: 'entity-structure/limited-partnership', component: LimitedPartnershipComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'entity-structure/series-llc', component: SeriesLlcComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'entity-structure/taxation', component: TaxationComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'entity-structure/wyoming-entity', component: WyomingEntityComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'entity-structure/llc', component: LlcComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'entity-structure/estate-planning', component: EstatePlanningComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EntityStructureRoutingModule { }
