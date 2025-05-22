import { Component, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { ReadFile } from '../../../../shared/dtos/reads/readFile';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { FileService } from '../../services/file.service';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { MessageType } from '../../../../shared/enums/messageType';
import { fileValidator } from '../../../../shared/validators/shared.validators';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './attachments.component.html'
})
export class AttachmentsComponent implements OnDestroy {
    public property: ReadProperty;

    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public isAddingNewFile = false;
    public addAttachmentsForm: UntypedFormGroup;
    public isAddingNewAttachments = false;
    public downloadingFileId: number | null;
    public selectedFiles: string | null;

    private alive = true;
    private fileToDelete: ReadFile;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService,
        private fileService: FileService,
        private formBuilder: UntypedFormBuilder) {
        this.createAddAttachmentsForm();
        this.subscribeToMessagingService();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onFilesChange(event: any): void {
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

        this.addAttachmentsForm.markAsDirty();
    }

    public onAddAttachmentsFormSubmit(): void {
        this.isAddingNewAttachments = true;
        const files = (this.addAttachmentsForm.get('files') as UntypedFormControl).value as FileList;
        this.serverError = false;
        this.validationErrorResult = null;

        this.fileService.uploadFiles(this.property.id, files)
            .pipe(takeWhile(() => this.alive))
            .subscribe(x => {
                this.property.files = x;
                this.isAddingNewAttachments = false;

                this.selectedFiles = null;
                this.filesCtrl.setValue(null);
                this.addAttachmentsForm.reset();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.isAddingNewAttachments = false;
                });
    }
    //test
    public onDelete(file: ReadFile): void {
        this.fileToDelete = file;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${file.name} file?`, MessageType.deleteRentalPropertyFile);
    }

    public onDownloadClick(file: ReadFile): void {
        this.downloadingFileId = file.id;

        this.fileService.getFile(this.property.id, file.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(x => {
                const binary = atob(x.contentBase64.replace(/\s/g, ''));
                const buffer = new ArrayBuffer(binary.length);
                const blobParts = new Uint8Array(buffer);

                for (let i = 0; i < binary.length; i++) {
                    blobParts[i] = binary.charCodeAt(i);
                }

                const blob: Blob = new Blob([blobParts], { type: x.mimeType });
                const linkElement = document.createElement('a');
                linkElement.href = URL.createObjectURL(blob);
                linkElement.download = x.name;
                linkElement.click();

                this.downloadingFileId = null;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.downloadingFileId = null;
                });
    }

    private deleteFile() {
        this.fileService.deleteFile(this.property.id, this.fileToDelete.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.property.files.findIndex(x => x.id === this.fileToDelete.id);
                this.property.files.splice(itemIndex, 1);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private createAddAttachmentsForm(): void {
        this.addAttachmentsForm = this.formBuilder.group({
            files: [null, [Validators.required, fileValidator]]
        });
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.deleteRentalPropertyFile) {
                    this.deleteFile();
                }
            });
    }

    get filesCtrl(): UntypedFormControl {
        return this.addAttachmentsForm.get('files') as UntypedFormControl;
    }
}
