import { Component, Input, OnInit } from '@angular/core';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-estimated-resale-value',
  templateUrl: './estimated-resale-value.component.html',
  styleUrls: ['./estimated-resale-value.component.css']
})
export class EstimatedResaleValueComponent {

  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;

  constructor() { }



}
