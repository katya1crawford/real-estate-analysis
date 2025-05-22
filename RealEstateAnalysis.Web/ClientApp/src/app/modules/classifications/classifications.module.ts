import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClassificationsRoutingModule } from './classifications-routing.module';
import { ClassificationsComponent } from './components/classifications/classifications.component';
import { MultifamilyProsConsComponent } from './modals/multifamily-pros-cons/multifamily-pros-cons.component';


@NgModule({
    declarations: [
        ClassificationsComponent,
        MultifamilyProsConsComponent],
    imports: [
        CommonModule,
        ClassificationsRoutingModule
    ]
})
export class ClassificationsModule { }
