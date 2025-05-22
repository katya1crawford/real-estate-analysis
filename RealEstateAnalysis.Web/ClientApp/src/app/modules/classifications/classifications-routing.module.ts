import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClassificationsComponent } from './components/classifications/classifications.component';
import { AuthGuardService } from '../../shared/services/auth-guard.service';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'classifications', component: ClassificationsComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
    ])],
    exports: [RouterModule]
})
export class ClassificationsRoutingModule { }
