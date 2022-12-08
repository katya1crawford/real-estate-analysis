import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-error-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {

  constructor() { }

  @Input() serverError: boolean;
  @Input() errorMessage: string | string[];

  ngOnInit(): void {
  }

}
