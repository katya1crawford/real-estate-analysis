import { Component, Input } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/readRentRollSummary';

@Component({
    selector: 'app-rents',
    templateUrl: './rents.component.html',
    styleUrls: ['./rents.component.css']
})
export class RentsComponent {
    @Input() public rentRollSummary: ReadRentRollSummary;
}
