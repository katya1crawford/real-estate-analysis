import { Component, Input } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';

@Component({
    selector: 'app-kpis',
    templateUrl: './kpis.component.html'
})
export class KpisComponent {
    @Input() public property: ReadProperty;
    @Input() public pageLoading: boolean;

    public getMonthlyAmount(annualAmout: number): number {
        if (this.nullUndefinedOrZero(annualAmout)) {
            return 0;
        } else {
            return +(annualAmout / 12).toFixed(2);
        }
    }

    public getRatio(numerator: number, denominator: number): number {
        if (this.nullUndefinedOrZero(numerator) || this.nullUndefinedOrZero(denominator)) {
            return 0;
        } else {
            return +(numerator / denominator).toFixed(2);
        }
    }

    public getTotalActiveUnitsSqFts(): number {
        const vacancyLossSqFts = this.property.totalUnitsSquareFootage * (this.property.annualVacancyRate / 100);
        const totalActiveUnitsSqFts = this.property.totalUnitsSquareFootage - vacancyLossSqFts;
        return totalActiveUnitsSqFts;
    }

    private nullUndefinedOrZero(value: number | undefined | null): boolean {
        return value === undefined || value === null || value === 0;
    }
}
