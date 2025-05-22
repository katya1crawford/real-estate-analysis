import { MessageType } from '../enums/messageType';

export class MessageDto {
    constructor(public messageType: MessageType,
        public content: any) { }
}
