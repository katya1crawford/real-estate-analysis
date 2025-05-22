import { Component, Input } from '@angular/core';
import { ReadValidationResult } from '../../dtos/reads/readValidationResult';

@Component({
    selector: 'app-error-summary',
    templateUrl: './error-summary.component.html'
})
export class ErrorSummaryComponent {
    @Input() public validationErrorResult: ReadValidationResult;
    @Input() public serverError: boolean;
}
