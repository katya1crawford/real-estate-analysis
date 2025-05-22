import { NgModule } from '@angular/core';
import { ModalComponent } from './components/modal.component';
import { ModalService } from './modal.service';


@NgModule({
    declarations: [ModalComponent],
    providers: [ModalService]
})
export class ModalModule { }
