import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OperatingStatementModalComponent } from './operating-statement-modal/operating-statement-modal.component';

@Component({
  selector: 'app-pre-offer',
  templateUrl: './pre-offer.component.html',
  styleUrls: ['./pre-offer.component.css']
})
export class PreOfferComponent implements OnInit {

  public panelOpenState = false;
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(OperatingStatementModalComponent, {
      width: '70%',
      autoFocus: false,
      maxHeight: '90vh',

    });


  }
}
