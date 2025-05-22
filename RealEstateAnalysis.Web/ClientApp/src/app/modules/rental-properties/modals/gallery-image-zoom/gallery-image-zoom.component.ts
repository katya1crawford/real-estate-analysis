import { Component, OnDestroy } from '@angular/core';
import { takeWhile } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { ReadFile } from '../../../../shared/dtos/reads/readFile';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { GalleryImageService } from '../../services/gallery-image.service';


@Component({
    templateUrl: './gallery-image-zoom.component.html'
})
export class GalleryImageZoomComponent implements OnDestroy {
    public property: ReadProperty;
    public activeImage: ReadFile;
    public largeImages: ReadFile[];
    public pageLoading = true;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;

    private alive = true;

    constructor(private galleryImageService: GalleryImageService) { }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public initialize(property: ReadProperty, activeImage: ReadFile): void {
        this.property = property;
        this.activeImage = activeImage;

        this.galleryImageService.getAllLarge(this.property.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(largeImages => {
                this.largeImages = largeImages;
                this.pageLoading = false;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.pageLoading = false;
                });
    }
}
