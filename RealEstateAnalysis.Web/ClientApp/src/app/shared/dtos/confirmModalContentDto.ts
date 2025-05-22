import { MessageType } from '../enums/messageType';

export class ConfirmModalContentDto {
    constructor(public title: string,
        public message: string,
        public messageType: MessageType) { }
}
