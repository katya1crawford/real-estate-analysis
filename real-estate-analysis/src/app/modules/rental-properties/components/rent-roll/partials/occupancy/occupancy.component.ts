import { Component, Input, OnInit } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';

@Component({
  selector: 'app-occupancy',
  templateUrl: './occupancy.component.html',
  styleUrls: ['./occupancy.component.css']
})
export class OccupancyComponent implements OnInit {


  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;
  constructor() { }

  ngOnInit(): void {
  }

}
