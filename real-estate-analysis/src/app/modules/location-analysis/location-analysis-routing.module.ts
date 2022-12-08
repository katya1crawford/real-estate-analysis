import { Host, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { HomeComponent } from '../home/components/home/home.component';
import { CitiesComponent } from './cities/cities.component';
import { NeighborhoodsComponent } from './neighborhoods/neighborhoods.component';


const routes: Routes = [
  { path: 'location-analysis/cities', component: CitiesComponent, canActivate: [AuthGuardService] },
  { path: 'location-analysis/neighborhoods', component: NeighborhoodsComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocationAnalysisRoutingModule { }
