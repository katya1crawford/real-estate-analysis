import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppPopoverDirective } from './app-popover.directive';



@NgModule({
  declarations: [
    AppPopoverDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [AppPopoverDirective]
})
export class PopoverModule { }
