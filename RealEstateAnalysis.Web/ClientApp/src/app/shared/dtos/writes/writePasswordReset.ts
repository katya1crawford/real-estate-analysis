export class WritePasswordReset {
    constructor(public userId: string,
        public token: string,
        public newPassword: string) { }
}
