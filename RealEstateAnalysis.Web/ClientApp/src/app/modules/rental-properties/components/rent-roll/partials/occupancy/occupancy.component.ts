import { Component, Input } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';

@Component({
    selector: 'app-occupancy',
    templateUrl: './occupancy.component.html',
    styleUrls: ['./occupancy.component.css']
})
export class OccupancyComponent {
    @Input() public rentRollSummary: ReadRentRollSummary;
}
