import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuardService } from '../../shared/services/auth-guard.service';
import { RentRollComponent } from './components/rent-roll/rent-roll.component';
import { RentalPropertiesComponent } from './components/rental-properties/rental-properties.component';
import { RentalPropertyDetailsComponent } from './components/rental-property-details/rental-property-details.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'rentals/:propertyStatus', component: RentalPropertiesComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
        { path: 'rental-property-details/:id', component: RentalPropertyDetailsComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
        { path: 'rental-property-details/:id/rent-roll', component: RentRollComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
    ])],
    exports: [RouterModule]
})
export class RentalPropertiesRoutingModule { }
