import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { MessageDto } from '../dtos/messageDto';

@Injectable()
export class MessagingService {
    private messageStatusSource = new Subject<MessageDto>();
    public messageStatus = this.messageStatusSource.asObservable();

    public sendMessage(message: MessageDto): void {
        this.messageStatusSource.next(message);
    }
}
