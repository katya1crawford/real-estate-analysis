import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from '../../shared/services/auth-guard.service';
import { CitiesComponent } from './components/cities/cities.component';
import { NeighborhoodsComponent } from './components/neighborhoods/neighborhoods.component';


const routes: Routes = [
    { path: 'location-analysis/cities', component: CitiesComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
    { path: 'location-analysis/neighborhoods', component: NeighborhoodsComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LocationAnalysisRoutingModule { }
