import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PopoverDirective } from './popover.directive';

@NgModule({
    declarations: [
        PopoverDirective
    ],
    imports: [
        CommonModule
    ],
    exports: [
        PopoverDirective
    ]
})
export class PopoverModule { }
