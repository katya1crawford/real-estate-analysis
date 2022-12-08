import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { ReadFile } from 'src/app/modules/rental-properties/dtos/reads/readFile';
import { GalleryService } from 'src/app/modules/rental-properties/services/gallery.service';

@Component({
  selector: 'app-zoom-gallery-image',
  templateUrl: './zoom-gallery-image.component.html',
  styleUrls: ['./zoom-gallery-image.component.css']
})
export class ZoomGalleryImageComponent implements OnInit, OnDestroy {


  constructor(@Inject(MAT_DIALOG_DATA) data: any,
    private galleryService: GalleryService) { this.galleryImageId = data.imageId, this.propertyId = data.propertyId }

  private galleryImageId: number;
  private propertyId: number;
  subscription: Subscription;
  public activeImages: ReadFile[];

  ngOnInit(): void {
    this.subscription = this.galleryService.getAllLarge(this.propertyId)
      .pipe(map(images => images.filter(x => x.id === this.galleryImageId)))
      .subscribe((image) => this.activeImages = image);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
