import { ReadState } from './readState';

export class ReadAddress {
    constructor(public address: string,
        public city: string,
        public state: ReadState,
        public zipCode: string,
        public latitude: number,
        public longitude: number,
        public neighborhood: string,
        public county: string) { }
}
