import { Component } from '@angular/core';
import { ConfirmModalContentDto } from '../../dtos/confirmModalContentDto';
import { MessageDto } from '../../dtos/messageDto';
import { MessagingService } from '../../services/messaging.service';
import { ModalService } from '../../services/modal/modal.service';

@Component({
    templateUrl: './confirm.component.html'
})
export class ConfirmComponent {
    public modalContent: ConfirmModalContentDto;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService) { }

    public onOk(): void {
        const message = new MessageDto(this.modalContent.messageType, null);
        this.messagingService.sendMessage(message);
        this.modalService.hide();
    }
}
