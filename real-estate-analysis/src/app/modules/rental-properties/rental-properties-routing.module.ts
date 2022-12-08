import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { AddEditNewPropertyComponent } from './components/add-edit-property/add-edit-property.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { RentRollComponent } from './components/rent-roll/rent-roll.component';
import { RentalsComponent } from './components/rentals/rentals.component';

const routes: Routes = [
  { path: 'rentals/:propertyStatus', component: RentalsComponent, canActivate: [AuthGuardService] },
  { path: 'rentals/property-details/:id', component: PropertyDetailsComponent, canActivate: [AuthGuardService] },
  { path: 'add-edit-property/:id', component: AddEditNewPropertyComponent, canActivate: [AuthGuardService] },
  { path: 'rentals/property-details/:id/rent-roll', component: RentRollComponent, canActivate: [AuthGuardService] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RentalPropertiesRoutingModule { }
