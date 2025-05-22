import { Component, Input } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';

@Component({
    selector: 'app-estimated-resale-value',
    templateUrl: './estimated-resale-value.component.html'
})
export class EstimatedResaleValueComponent {
    @Input() public property: ReadProperty;
    @Input() public pageLoading: boolean;
}
