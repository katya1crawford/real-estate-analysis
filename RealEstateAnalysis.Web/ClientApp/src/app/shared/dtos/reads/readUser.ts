export class ReadUser {
    constructor(public id: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public roles: string[]) { }
}
