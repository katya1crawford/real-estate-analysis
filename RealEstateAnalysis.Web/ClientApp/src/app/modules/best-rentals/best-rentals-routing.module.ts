import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { BestRentalsComponent } from './components/best-rentals/best-rentals.component';

const routes: Routes = [
    { path: 'best-rentals', component: BestRentalsComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BestRentalsRoutingModule { }
