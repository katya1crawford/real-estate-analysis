export class JwtTokenPayloadDto {
    constructor(public userId: string,
        public roles: Array<string>,
        public email: string,
        public firstName: string,
        public lastName: string,
        public exp: number) { }
}
