import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { takeWhile } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { ReadFinancialForecast } from '../../dtos/reads/readFinancialForecast';
import { ReadFile } from '../../../../shared/dtos/reads/readFile';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { CalculatorService } from '../../services/calculator.service';
import { GalleryImageService } from '../../services/gallery-image.service';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { RentalPropertyService } from '../../services/rental-property.service';
import { PdfService } from '../../services/pdf.service';
import { appConfig } from '../../../../app.config';
import { IncomeStatementComponent } from '../../modals/income-statement/income-statement.component';
import { AddEditPropertyComponent } from '../../modals/add-edit-property/add-edit-property.component';
import { AttachmentsComponent } from '../../modals/attachments/attachments.component';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { MessageType } from '../../../../shared/enums/messageType';
import { ReadAnnualOperatingExpense } from '../../dtos/reads/readAnnualOperatingExpense';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './rental-property-details.component.html'
})
export class RentalPropertyDetailsComponent implements OnInit, OnDestroy {
    public property: ReadProperty;
    public financialForecasts: ReadFinancialForecast[];
    public galleryImages: ReadFile[];
    public propertyToDelete: ReadProperty;
    public pageLoading = true;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public downloadingPdf = false;

    private alive = true;

    constructor(titleService: Title,
        private route: ActivatedRoute,
        private router: Router,
        private calculatorService: CalculatorService,
        private galleryImageService: GalleryImageService,
        private messagingService: MessagingService,
        private propertyService: RentalPropertyService,
        private pdfService: PdfService,
        private modalService: ModalService) {
        titleService.setTitle(`${appConfig.businessName}: Property Details`);
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.route.paramMap
            .pipe(takeWhile(() => this.alive))
            .subscribe((params: ParamMap) => {
                const propertyId = +params.get('id');
                this.setProperty(propertyId);
            });
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onRentRollClick(property: ReadProperty) {
        this.router.navigate([`/rental-property-details/${property.id}/rent-roll`]);
    }

    public onDownloadPropertySummaryPdfClick(property: ReadProperty): void {
        if (this.downloadingPdf) {
            return;
        }

        this.downloadingPdf = true;

        this.pdfService.getGetPropertySummaryPdf(property.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe((file) => {
                const binary = atob(file.contentBase64.replace(/\s/g, ''));
                const buffer = new ArrayBuffer(binary.length);
                const blobParts = new Uint8Array(buffer);

                for (let i = 0; i < binary.length; i++) {
                    blobParts[i] = binary.charCodeAt(i);
                }

                const blob: Blob = new Blob([blobParts], { type: file.mimeType });
                const linkElement = document.createElement('a');
                linkElement.href = URL.createObjectURL(blob);
                linkElement.download = file.name;
                linkElement.click();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                },
                () => this.downloadingPdf = false);
    }

    public onIncomeStatementClick(property: ReadProperty): void {
        const incomeStatementModal = this.modalService.show(IncomeStatementComponent, { sizeClass: 'modal-lg' });
        incomeStatementModal.initialize(property);
    }

    public onEditClick(property: ReadProperty): void {
        const editModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        editModal.setEditPropertyFormValues(property);
        editModal.messageType = MessageType.updatedRentalProperty;
    }

    public onAttachmentsClick(): void {
        const propertyAttachmentsModal = this.modalService.show(AttachmentsComponent, { sizeClass: 'modal-lg' });
        propertyAttachmentsModal.property = this.property;
    }

    public onDeleteClick(property: ReadProperty): void {
        this.propertyToDelete = property;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${property.address.address} property?`, MessageType.deleteRentalProperty);
    }

    private deleteProperty() {
        this.propertyService.deleteProperty(this.propertyToDelete.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => this.router.navigate(['/rentals']),
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setProperty(propertyId: number): void {
        this.propertyService.getProperty(propertyId, true)
            .pipe(takeWhile(() => this.alive))
            .subscribe(property => {
                this.property = property;

                if (property.annualOperatingExpenses !== null) {
                    this.property.annualOperatingExpenses = property.annualOperatingExpenses.sort(this.orderAnnualOperatingExpensesByAmountDesc);
                }

                forkJoin([this.calculatorService.getRentalPropertyLongTermFinancialForecasts(propertyId), this.galleryImageService.getAllSmall(propertyId)])
                    .pipe(takeWhile(() => this.alive))
                    .subscribe(([financialForecasts, galleryImages]) => {
                        this.financialForecasts = financialForecasts.sort(this.orderFinancialForecastByIdAsc);
                        this.galleryImages = galleryImages;
                        this.pageLoading = false;
                    },
                        () => {
                            this.serverError = true;
                            this.pageLoading = false;
                        });
            },
                () => {
                    this.serverError = true;
                    this.pageLoading = false;
                });
    }

    private setFinancialForecasts(propertyId: number) {
        return this.calculatorService.getRentalPropertyLongTermFinancialForecasts(propertyId)
            .pipe(takeWhile(() => this.alive))
            .subscribe((financialForecasts) => {
                this.financialForecasts = financialForecasts.sort(this.orderFinancialForecastByIdAsc);
            },
                () => {
                    this.serverError = true;
                });
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.deleteRentalProperty) {
                    this.deleteProperty();
                }

                if (message.messageType === MessageType.updatedRentalProperty) {
                    this.property = message.content as ReadProperty;
                    this.setFinancialForecasts(this.property.id);
                }
            });
    }

    private orderAnnualOperatingExpensesByAmountDesc(a: ReadAnnualOperatingExpense, b: ReadAnnualOperatingExpense): number {
        if (a.amount > b.amount) {
            return -1;
        }

        if (a.amount < b.amount) {
            return 1;
        }

        return 0;
    }

    private orderFinancialForecastByIdAsc(a: ReadFinancialForecast, b: ReadFinancialForecast): number {
        if (a.id > b.id) {
            return 1;
        }

        if (a.id < b.id) {
            return -1;
        }

        return 0;
    }
}
