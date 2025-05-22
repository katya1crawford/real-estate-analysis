import { Component, Input } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';

@Component({
    selector: 'app-floor-plans',
    templateUrl: './floor-plans.component.html',
    styleUrls: ['./floor-plans.component.css']
})
export class FloorPlansComponent {
    @Input() public rentRollSummary: ReadRentRollSummary;
}
