import { ReadState } from 'src/app/shared/dtos/reads/readState';

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
