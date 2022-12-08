import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';


@Component({
  selector: 'app-property-summary',
  templateUrl: './property-summary.component.html',
  styleUrls: ['./property-summary.component.css']
})
export class PropertySummaryComponent {

  constructor() { }
  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;
  public noImage = './assets/noImage.jpg';




  public getRatio(numerator: number, denominator: number): number {
    if (this.nullUndefinedOrZero(numerator) || this.nullUndefinedOrZero(denominator)) {
      return 0;
    } else {
      return +(numerator / denominator).toFixed(2);
    }
  }

  private nullUndefinedOrZero(value: number | undefined | null): boolean {
    return value === undefined || value === null || value === 0;
  }



}

