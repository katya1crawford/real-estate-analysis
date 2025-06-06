﻿import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TermsComponent } from './components/terms/terms.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'terms', component: TermsComponent }
    ])],
    exports: [RouterModule]
})
export class TermsRoutingModule { }
