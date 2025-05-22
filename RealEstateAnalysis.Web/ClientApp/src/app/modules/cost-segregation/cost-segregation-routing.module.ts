import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { CostSegregationComponent } from './components/cost-segregation/cost-segregation.component';

const routes: Routes = [
    { path: 'cost-segregation', component: CostSegregationComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CostSegregationRoutingModule { }
