import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ReadCity } from '../../dtos/reads/readCity';
import { CityService } from '../../services/cityService/city.service';

@Component({
  selector: 'app-remove-from-favorites',
  templateUrl: './remove-from-favorites.component.html',
  styleUrls: ['./remove-from-favorites.component.css']
})
export class RemoveFromFavoritesComponent implements OnDestroy {

  public description = 'Remove from favorites';
  public city: ReadCity;
  public cityName: string;
  private subscription: Subscription;

  constructor(
    private dialogRef: MatDialogRef<RemoveFromFavoritesComponent>,
    private cityService: CityService,
    @Inject(MAT_DIALOG_DATA) data: any) {
    this.city = data.city;
  }

  ngOnDestroy(): void {
    if (this.subscription !== undefined) {
      this.subscription.unsubscribe();
    }
  }


  save(): void {
    this.subscription = this.cityService.toggleIsFavorite(this.city.id).subscribe();
    this.dialogRef.close(true);
  }

  close(): void {
    this.dialogRef.close();
  }

}
