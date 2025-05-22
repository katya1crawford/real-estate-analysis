import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TermsRoutingModule } from './terms-routing.module';
import { TermsComponent } from './components/terms/terms.component';

@NgModule({
    imports: [
        CommonModule,
        TermsRoutingModule
    ],
    declarations: [
        TermsComponent
    ]
})
export class TermsModule { }
