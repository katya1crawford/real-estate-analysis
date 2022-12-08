import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-summary-map',
  templateUrl: './summary-map.component.html',
  styleUrls: ['./summary-map.component.css']
})
export class SummaryMapComponent {

  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;

  constructor() { }

  public noImage = '/assets/shared/noImage.jpg';
  public mapHeight = 1036;

  // @ViewChild('summaryWrapper', { static: false }) set content(element: ElementRef) {
  //   if (element) {
  //     setTimeout(() => this.mapHeight = (+element.nativeElement.offsetHeight - 1.79), 500);
  //   }
  // }

}
