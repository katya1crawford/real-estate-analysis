import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { MessageDto } from 'src/app/shared/dtos/messageDto';
import { ReadValidationResult } from 'src/app/shared/dtos/reads/readValidationResult';
import { MessageType } from 'src/app/shared/enums/messageType';
import { MessagingService } from 'src/app/shared/services/messaging.service';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { csvFileValidator } from 'src/app/shared/validators/shared.validators';
import { RentRollService } from '../../services/rent-roll.service';

@Component({
    templateUrl: './import-rent-roll.component.html',
    styleUrls: ['./import-rent-roll.component.css']
})
export class ImportRentRollComponent implements OnDestroy {
    public propertyId: number;

    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public importRentRollForm: UntypedFormGroup;
    public isImportingRentRollCsv = false;
    public selectedFileName: string | null;

    private alive = true;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder,
        private rentRollService: RentRollService
    ) {
        this.createImportRentRollForm();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    private createImportRentRollForm(): void {
        this.importRentRollForm = this.formBuilder.group({
            csvFile: [null, [Validators.required, csvFileValidator]]
        });
    }

    public onImportRentRollFormSubmit() {
        this.isImportingRentRollCsv = true;
        this.serverError = false;
        this.validationErrorResult = null;

        this.rentRollService.importRentRollCsv(this.propertyId, this.csvFile.value)
            .pipe(takeWhile(() => this.alive))
            .subscribe(rentRollSummary => {
                this.isImportingRentRollCsv = false;
                this.selectedFileName = null;
                this.csvFile.setValue(null);
                this.importRentRollForm.reset();
                const message = new MessageDto(MessageType.importedRentRollCsv, rentRollSummary);
                this.messagingService.sendMessage(message);
                this.modalService.hide();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.isImportingRentRollCsv = false;
                });
    }

    public onFileChange(event: any) {
        if (event.target.files.length > 0) {
            const file: File | null = (event.target.files as File[])[0];
            const fileName = file.name;

            this.selectedFileName = fileName;
            this.csvFile.setValue(file);
        } else {
            this.selectedFileName = null;
            this.csvFile.setValue(null);
        }

        this.importRentRollForm.markAsDirty();
    }

    get csvFile(): UntypedFormControl {
        return this.importRentRollForm.get('csvFile') as UntypedFormControl;
    }
}
