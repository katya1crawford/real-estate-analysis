import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { finalize, takeWhile } from 'rxjs/operators';
import { ReadFile } from 'src/app/modules/rental-properties/dtos/reads/readFile';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';
import { FileService } from 'src/app/modules/rental-properties/services/file.service';
import { GalleryService } from 'src/app/modules/rental-properties/services/gallery.service';
import { DeleteGalleryImageComponent } from '../../dialogs/delete-gallery-image/delete-gallery-image.component';
import { ZoomGalleryImageComponent } from '../../dialogs/zoom-gallery-image/zoom-gallery-image.component';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit, OnDestroy {

  @Input() public galleryImages: ReadFile[];
  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;

  public addImagesForm: FormGroup;
  public isAddingNewImages = false;
  public selectedImage: any;
  public arrayOfImages: any[];
  public alive = true;
  public serverError = false;
  public errorMessage: string | any;


  constructor(private fb: FormBuilder,
    private dialog: MatDialog,
    private galleryService: GalleryService,
  ) { }


  ngOnDestroy(): void {
    this.alive = false;
  }

  ngOnInit(): void {
    this.addImagesForm = this.fb.group({
      files: [null, [Validators.required]]
    });
  }

  onDelete(image: ReadFile): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = 'auto';
    dialogConfig.width = '400px';

    dialogConfig.data = {
      imageId: image.id,
      propertyId: this.property.id
    };

    const dialogRef = this.dialog.open(DeleteGalleryImageComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((response) => {
      if (response === true) {
        this.galleryImages = this.galleryImages.filter(x => x.id !== image.id);
      }
    });
  }

  onFilesChange(event: any): void {
    if (event.target.files.length > 0) {
      const files: File[] | null = event.target.files as File[];
      const fileNames = [];

      for (let i = 0; i < files.length; i++) {
        fileNames.push(files[i].name);
      }

      this.selectedImage = fileNames.join(', ');
      this.filesCtrl.setValue(files);
    } else {
      this.selectedImage = null;
      this.filesCtrl.setValue(null);
    }
  }


  onAddImagesFormSubmit(): void {
    this.isAddingNewImages = true;

    const files = this.filesCtrl.value as FileList;
    if (files !== null && files.length > 0) {
      this.galleryService.saveNew(this.property.id, files)
        .pipe(takeWhile(() => this.alive),
        )
        .subscribe((images) => {
          this.isAddingNewImages = false
          this.filesCtrl.setValue(null);
          this.addImagesForm.reset();
          this.galleryImages.push(...images);
        },
          (error: HttpErrorResponse) => {
            this.isAddingNewImages = false;
            this.serverError = true;
            if (error.status === 400) {
              console.log(error.error.errors);
              // this.errorMessage = error.error.errors;
              this.errorMessage = 'something went wrong';

            }
            else {
              this.errorMessage = 'something went wrong';
            }

          }
        );
    }



  }


  onZoom(image: ReadFile): void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '600px';
    dialogConfig.width = '685px';

    dialogConfig.data = {
      imageId: image.id,
      propertyId: this.property.id
    };

    const dialogRef = this.dialog.open(ZoomGalleryImageComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(() => console.log("WORKS"));

  }


  get filesCtrl(): FormControl {
    return this.addImagesForm.get('files') as FormControl;
  }

}
