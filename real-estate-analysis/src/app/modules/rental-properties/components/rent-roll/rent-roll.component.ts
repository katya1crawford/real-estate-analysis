import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { ActivatedRoute } from '@angular/router';
import { finalize, takeWhile } from 'rxjs/operators';
import { ReadRentRollSummary } from '../../dtos/reads/ReadRentRollSummary';
import { FileSaverService } from '../../services/file-saver.service';
import { RentRollService } from '../../services/rent-roll.service';
import { RentRollUploadComponent } from './bottom-sheet-upload/rent-roll-upload/rent-roll-upload.component';

@Component({
  selector: 'app-rent-roll',
  templateUrl: './rent-roll.component.html',
  styleUrls: ['./rent-roll.component.css']
})
export class RentRollComponent implements OnInit {

  public rentRollSummary: ReadRentRollSummary;
  public pageLoading = false;
  public errorMessages: any;
  private alive = true;
  public id: number;
  constructor(private activatedRoute: ActivatedRoute,
    private bottomSheet: MatBottomSheet,
    private rentRollService: RentRollService) { }

  ngOnInit(): void {
    this.pageLoading = true;
    this.id = Number(this.activatedRoute.snapshot.paramMap.get('id'));

    this.rentRollService.getSummary(this.id)
      .pipe(
        takeWhile(() => this.alive),
        finalize(() => this.pageLoading = false)
      )
      .subscribe(data => {
        this.rentRollSummary = data;
      },
        (error: HttpErrorResponse) => {
          if (error.status === 400) {
            this.errorMessages = error.error.errors;
          }
        });
  }

  onImportRentRollCsvClick(): void {
    this.bottomSheet.open(RentRollUploadComponent, { data: { propertyId: this.id } });
  }

}


