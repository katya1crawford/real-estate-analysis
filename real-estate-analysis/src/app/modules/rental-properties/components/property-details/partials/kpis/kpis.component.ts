import { Component, Input, OnInit } from '@angular/core';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-kpis',
  templateUrl: './kpis.component.html',
  styleUrls: ['./kpis.component.css']
})
export class KpisComponent implements OnInit {

  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;
  constructor() { }

  ngOnInit(): void {
  }

}
