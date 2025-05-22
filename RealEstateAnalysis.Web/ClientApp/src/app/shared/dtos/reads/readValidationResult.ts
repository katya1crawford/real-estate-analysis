import { ReadValidationError } from './readValidationError';

export class ReadValidationResult {
    constructor(public errors: ReadValidationError[],
        public isValid: boolean) { }
}
