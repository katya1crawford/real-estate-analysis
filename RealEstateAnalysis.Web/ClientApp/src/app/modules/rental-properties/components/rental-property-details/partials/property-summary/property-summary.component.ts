import { Component, Input, ViewChild, ElementRef } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';

@Component({
    selector: 'app-property-summary',
    styleUrls: ['./property-summary.component.css'],
    templateUrl: './property-summary.component.html'
})
export class PropertySummaryComponent {
    @Input() public property: ReadProperty;
    @Input() public pageLoading: boolean;

    public noImage = '/assets/shared/noImage.jpg';
    public mapHeight = 1036;

    @ViewChild('summaryWrapper', { static: false }) set content(element: ElementRef) {
        if (element) {
            setTimeout(() => this.mapHeight = (+element.nativeElement.offsetHeight - 1.79), 500);
        }
    }

    public getRatio(numerator: number, denominator: number): number {
        if (this.nullUndefinedOrZero(numerator) || this.nullUndefinedOrZero(denominator)) {
            return 0;
        } else {
            return +(numerator / denominator).toFixed(2);
        }
    }

    private nullUndefinedOrZero(value: number | undefined | null): boolean {
        return value === undefined || value === null || value === 0;
    }
}
