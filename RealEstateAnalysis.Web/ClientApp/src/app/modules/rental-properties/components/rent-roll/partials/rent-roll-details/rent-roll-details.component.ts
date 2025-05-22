import { formatCurrency } from '@angular/common';
import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subject } from 'rxjs';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';

@Component({
    selector: 'app-rent-roll-details',
    templateUrl: './rent-roll-details.component.html',
    styleUrls: ['./rent-roll-details.component.css']
})
export class RentRollDetailsComponent implements OnDestroy, OnChanges, OnInit {
    @Input() public rentRollSummary: ReadRentRollSummary;

    public dtTrigger: Subject<any> = new Subject<any>();
    public dtTitle = 'Rent Roll';
    public dtMessageTop = '';

    ngOnChanges(changes: SimpleChanges) {
        if (changes['rentRollSummary'] !== undefined) {
            this.dtTrigger.next();
        }
    }

    ngOnInit() {
        this.dtMessageTop = `${this.rentRollSummary.address.address}, ${this.rentRollSummary.address.city}, ${this.rentRollSummary.address.state.abbreviation}, ${this.rentRollSummary.address.zipCode}
            \r\nTotal Units: ${this.rentRollSummary.totalNumberOfUnits}
            Total Actual Income: ${formatCurrency(this.rentRollSummary.totalActualMonthlyIncome, 'en-US', '$')}
            Average Contract Rent: ${formatCurrency(+this.rentRollSummary.averageContractRent.toFixed(0), 'en-US', '$')}
            Average Market Rent: ${formatCurrency(+this.rentRollSummary.averageMarketRent.toFixed(0), 'en-US', '$')}
            Average % of Market Rent ${this.rentRollSummary.averagePercentOfMarketRent}%
            Vacancy Rate: ${this.rentRollSummary.vacancyRate}%`;
    }

    ngOnDestroy() {
        this.dtTrigger.unsubscribe();
    }
}
