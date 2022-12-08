import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ReadNeighborhood } from '../../dtos/reads/readNeighborhood';
import { NeighborhoodService } from '../../services/neighborhoodService/neighborhood.service';

@Component({
  selector: 'app-delete-neighborhood',
  templateUrl: './delete-neighborhood.component.html',
  styleUrls: ['./delete-neighborhood.component.css']
})
export class DeleteNeighborhoodComponent implements OnDestroy {

  description = "Delete Neighborhood";
  neighborhood: ReadNeighborhood;
  subscription: Subscription;


  constructor(
    private dialogRef: MatDialogRef<DeleteNeighborhoodComponent>,
    private neighborhoodService: NeighborhoodService,
    @Inject(MAT_DIALOG_DATA) data: any) {
    this.neighborhood = data.neighborhood;
  }
  ngOnDestroy(): void {
    if (this.subscription !== undefined) {
      this.subscription.unsubscribe();
    }
  }

  save(): void {
    this.subscription = this.neighborhoodService.deleteNeighborhood(this.neighborhood.id).subscribe();
    this.dialogRef.close(true);
  }

  close() {
    this.dialogRef.close();
  }

}
