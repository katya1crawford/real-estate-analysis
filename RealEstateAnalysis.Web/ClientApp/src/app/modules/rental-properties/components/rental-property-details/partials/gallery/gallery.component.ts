import { Component, Input, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, FormArray, UntypedFormGroup, Validators, AbstractControl } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { ReadProperty } from '../../../../dtos/reads/readProperty';
import { ReadFile } from '../../../../../../shared/dtos/reads/readFile';
import { ReadValidationResult } from '../../../../../../shared/dtos/reads/readValidationResult';
import { GalleryImageService } from '../../../../services/gallery-image.service';
import { MessagingService } from '../../../../../../shared/services/messaging.service';
import { ConfirmModalContentDto } from '../../../../../../shared/dtos/confirmModalContentDto';
import { MessageType } from '../../../../../../shared/enums/messageType';
import { imageFileValidator } from '../../../../../../shared/validators/shared.validators';
import { GalleryImageZoomComponent } from '../../../../modals/gallery-image-zoom/gallery-image-zoom.component';
import { ConfirmComponent } from '../../../../../../shared/modals/confirm/confirm.component';
import { ModalService } from '../../../../../../shared/services/modal/modal.service';

@Component({
    selector: 'app-gallery',
    templateUrl: './gallery.component.html'
})
export class GalleryComponent implements OnDestroy {
    @Input() public property: ReadProperty;
    @Input() public galleryImages: ReadFile[];
    @Input() public pageLoading: boolean;

    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public isAddingNewImages = false;
    public addImagesForm: UntypedFormGroup;
    public selectedFiles: string | null;

    private alive = true;
    private galleryImageToDelete: ReadFile;

    constructor(private galleryImageService: GalleryImageService,
        private formBuilder: UntypedFormBuilder,
        private modalService: ModalService,
        private messagingService: MessagingService) {
        this.createAddImagesForm();
        this.subscribeToMessagingService();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onDelete(galleryImage: ReadFile): void {
        this.galleryImageToDelete = galleryImage;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete this image?`, MessageType.deleteGalleryImage);
    }

    public onZoom(galleryImage: ReadFile): void {
        const galleryImageZoomModal = this.modalService.show(GalleryImageZoomComponent, { sizeClass: 'modal-lg' });
        galleryImageZoomModal.initialize(this.property, galleryImage);
    }

    public onAddImagesFormSubmit(): void {
        this.isAddingNewImages = true;
        this.serverError = false;
        this.validationErrorResult = null;
        const files = this.filesCtrl.value as FileList;

        this.galleryImageService.saveNew(this.property.id, files)
            .pipe(takeWhile(() => this.alive))
            .subscribe((galleryImages) => {
                this.galleryImages.push(...galleryImages);
                this.isAddingNewImages = false;

                this.selectedFiles = null;
                this.filesCtrl.setValue(null);
                this.addImagesForm.reset();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    }
                    // tslint:disable-next-line: one-line
                    else {
                        this.serverError = true;
                    }

                    this.isAddingNewImages = false;
                });
    }

    public onFilesChange(event): void {
        if (event.target.files.length > 0) {
            const files: File[] | null = event.target.files as File[];
            const fileNames = [];

            for (let i = 0; i < files.length; i++) {
                fileNames.push(files[i].name);
            }

            this.selectedFiles = fileNames.join(', ');
            this.filesCtrl.setValue(files);
        } else {
            this.selectedFiles = null;
            this.filesCtrl.setValue(null);
        }

        this.addImagesForm.markAsDirty();
    }

    private createAddImagesForm(): void {
        this.addImagesForm = this.formBuilder.group({
            files: [null, [Validators.required, imageFileValidator]]
        });
    }

    private deleteGalleryImage(): void {
        this.galleryImageService.delete(this.property.id, this.galleryImageToDelete.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.galleryImages.findIndex(x => x.id === this.galleryImageToDelete.id);
                this.galleryImages.splice(itemIndex, 1);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.deleteGalleryImage) {
                    this.deleteGalleryImage();
                }
            });
    }

    get filesCtrl(): UntypedFormControl {
        return this.addImagesForm.get('files') as UntypedFormControl;
    }
}
