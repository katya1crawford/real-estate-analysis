import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { takeWhile } from 'rxjs/operators';
import { RentRollService } from 'src/app/modules/rental-properties/services/rent-roll.service';
import { requiredFileType } from 'src/app/shared/validators/validators';

@Component({
  selector: 'app-rent-roll-upload',
  templateUrl: './rent-roll-upload.component.html',
  styleUrls: ['./rent-roll-upload.component.css']
})
export class RentRollUploadComponent implements OnDestroy {

  public errorMessage: string;
  public serverError = false;
  private alive = true;
  public isUploading = false;
  public uploadForm: FormGroup;
  constructor(private bottomSheetRef: MatBottomSheetRef<RentRollUploadComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: { propertyId: number },
    private rentRollService: RentRollService, private fb: FormBuilder) { this.createUploadForm(); }


  openLink(event: MouseEvent): void {
    this.bottomSheetRef.dismiss();
    event.preventDefault();
  }

  createUploadForm(): void {
    this.uploadForm = this.fb.group({
      file: [null, [Validators.required, requiredFileType('csv')]]
    })

  }

  onUploadRentRoll(): void {
    this.isUploading = true;

    this.rentRollService.importRentRollCsv(this.data.propertyId, this.file.value)
      .pipe(takeWhile(() => this.alive)
      )
      .subscribe(() => {

        this.bottomSheetRef.dismiss();
        this.isUploading = false;
      },
        (error: HttpErrorResponse | any) => {

          this.serverError = true;
          if (error.status == 400) {
            this.serverError = true;
            this.errorMessage = "Something went wrong";
            console.log(error.name, error.message, error.statusText);
            this.isUploading = false;
          } else {
            this.errorMessage = 'Please try again later.'
          }
        })

  }


  handleFileInputChange(event: any): void {
    if (event.target.files.length > 0) {
      const file: File | null = event.target.files[0] as File;
      this.file.setValue(file);
    } else {
      this.file.setValue(null);
    }

    this.uploadForm.markAsDirty();
  }



  ngOnDestroy(): void {
    this.alive = false;
  }


  get file(): FormControl {
    return this.uploadForm.get('file') as FormControl;
  }

}
