import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-operating-statement-modal',
  templateUrl: './operating-statement-modal.component.html',
  styleUrls: ['./operating-statement-modal.component.css']
})
export class OperatingStatementModalComponent {

  panelOpenState = false;
  constructor(
    public dialogRef: MatDialogRef<OperatingStatementModalComponent>

  ) { }



  onClose(): void {
    this.dialogRef.close();

  }

}
