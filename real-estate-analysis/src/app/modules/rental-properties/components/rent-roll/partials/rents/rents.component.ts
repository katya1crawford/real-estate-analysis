import { Component, Input, OnInit } from '@angular/core';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';

@Component({
  selector: 'app-rents',
  templateUrl: './rents.component.html',
  styleUrls: ['./rents.component.css']
})
export class RentsComponent implements OnInit {


  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;
  constructor() { }

  ngOnInit(): void {
  }

}
