import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HighchartsComponent } from './highcharts.component';


@NgModule({
    declarations: [
        HighchartsComponent
    ],
    imports: [
        CommonModule
    ],
    exports: [
        HighchartsComponent
    ]
})
export class HighchartsModule { }
