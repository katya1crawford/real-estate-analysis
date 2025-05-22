export class ReadValidationError {
    constructor(public attemptedValue: string[],
        public errorMessage: string) { }
}
