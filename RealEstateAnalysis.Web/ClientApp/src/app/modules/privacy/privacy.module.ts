import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PrivacyRoutingModule } from './privacy-routing.module';
import { PrivacyComponent } from './components/privacy/privacy.component';

@NgModule({
    imports: [
        CommonModule,
        PrivacyRoutingModule
    ],
    declarations: [
        PrivacyComponent
    ]
})
export class PrivacyModule { }
