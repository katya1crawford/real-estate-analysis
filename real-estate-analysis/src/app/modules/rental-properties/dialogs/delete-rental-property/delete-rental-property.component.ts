import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-delete-rental-property',
  templateUrl: './delete-rental-property.component.html',
  styleUrls: ['./delete-rental-property.component.css']
})
export class DeleteRentalPropertyComponent implements OnInit, OnDestroy {

  public property: ReadProperty;
  description = 'Delete Property';
  private subscription: Subscription
  constructor(private dialogRef: MatDialogRef<DeleteRentalPropertyComponent>,
    @Inject(MAT_DIALOG_DATA) data: any, private propertyService: PropertyService) { this.property = data.property; }

  ngOnInit(): void {
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.subscription = this.propertyService.deleteProperty(this.property.id).subscribe();
    this.dialogRef.close(true);
  }

  ngOnDestroy(): void {
    if (this.subscription !== undefined) {
      this.subscription.unsubscribe();

    }
  }

}
