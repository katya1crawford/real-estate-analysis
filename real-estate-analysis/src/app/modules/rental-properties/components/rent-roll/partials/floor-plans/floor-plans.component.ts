import { Component, Input, OnInit } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';

@Component({
  selector: 'app-floor-plans',
  templateUrl: './floor-plans.component.html',
  styleUrls: ['./floor-plans.component.css']
})
export class FloorPlansComponent implements OnInit {


  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;

  constructor() { }

  ngOnInit(): void {
  }

}
