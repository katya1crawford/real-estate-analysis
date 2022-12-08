import { Component, Inject, OnInit } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { ReadProperty } from '../../../dtos/reads/readProperty';
import { PropertyService } from '../../../services/property.service';

@Component({
  selector: 'app-total-cash-needed-details',
  templateUrl: './total-cash-needed-details.component.html',
  styleUrls: ['./total-cash-needed-details.component.css']
})
export class TotalCashNeededDetailsComponent {

  public property: ReadProperty;
  constructor(private _bottomSheetRef: MatBottomSheetRef<TotalCashNeededDetailsComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any) {
    this.property = data
  }



  openLink(event: MouseEvent): void {
    this._bottomSheetRef.dismiss();
    event.preventDefault();
  }
}
