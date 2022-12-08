import { Component, Input, OnInit } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {


  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;

  constructor() { }

  ngOnInit(): void {
  }



}
