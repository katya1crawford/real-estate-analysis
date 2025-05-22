import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatatablesDirective } from './datatables.directive';

@NgModule({
    declarations: [
        DatatablesDirective
    ],
    imports: [
        CommonModule
    ],
    exports: [
        DatatablesDirective
    ]
})
export class DatatablesModule { }
