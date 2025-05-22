import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorSummaryModule } from '../../directives/error-summary/error-summary.module';
import { ContactUsComponent } from './contact-us.component';
import { ContactUsService } from '../../services/contact-us.service';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ErrorSummaryModule
    ],
    declarations: [
        ContactUsComponent
    ],
    providers: [
        ContactUsService
    ]
})
export class ContactUsModule { }
