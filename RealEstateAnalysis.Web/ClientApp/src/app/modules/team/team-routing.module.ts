import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeamComponent } from './components/team/team.component';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';


const routes: Routes = [
    { path: 'team', component: TeamComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TeamRoutingModule { }
