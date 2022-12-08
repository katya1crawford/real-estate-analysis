import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ReadCity } from '../../dtos/reads/readCity';
import { CityService } from '../../services/cityService/city.service';

@Component({
  selector: 'app-delete-city',
  templateUrl: './delete-city.component.html',
  styleUrls: ['./delete-city.component.css']
})
export class DeleteCityComponent implements OnDestroy {

  description = 'Delete City';
  city: ReadCity;
  private subscription: Subscription;
  cityName: string;

  constructor(
    private dialogRef: MatDialogRef<DeleteCityComponent>,
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
    this.subscription = this.cityService.deleteCity(this.city.id).subscribe();
    this.dialogRef.close(true);
  }


  close() {
    this.dialogRef.close();
  }
}
