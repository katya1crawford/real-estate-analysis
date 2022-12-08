import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { takeWhile } from 'rxjs/operators';
import { GalleryService } from 'src/app/modules/rental-properties/services/gallery.service';

@Component({
  selector: 'app-delete-gallery-image',
  templateUrl: './delete-gallery-image.component.html',
  styleUrls: ['./delete-gallery-image.component.css']
})
export class DeleteGalleryImageComponent {

  imageId: number;
  propertyId: number;
  alive = true;
  isDeleting = false;

  constructor(
    private router: Router,
    private dialogRef: MatDialogRef<DeleteGalleryImageComponent>,
    @Inject(MAT_DIALOG_DATA) data: any,
    private galleryService: GalleryService) { this.imageId = data.imageId, this.propertyId = data.propertyId; }


  onSave(): void {
    this.isDeleting = true;

    this.galleryService.delete(this.propertyId, this.imageId)
      .pipe(takeWhile(() => this.alive))
      .subscribe(() => {
        this.isDeleting = false;
        this.router.navigate(['rentals/property-details', this.propertyId]);
      });
  }

}
