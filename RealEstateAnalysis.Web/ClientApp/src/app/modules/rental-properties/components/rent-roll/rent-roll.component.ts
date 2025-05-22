import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { takeWhile } from 'rxjs/operators';
import { appConfig } from 'src/app/app.config';
import { ReadValidationResult } from 'src/app/shared/dtos/reads/readValidationResult';
import { MessageType } from 'src/app/shared/enums/messageType';
import { MessagingService } from 'src/app/shared/services/messaging.service';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { ReadRentRollSummary } from '../../dtos/reads/readRentRollSummary';
import { ImportRentRollComponent } from '../../modals/import-rent-roll/import-rent-roll.component';
import { RentRollService } from '../../services/rent-roll.service';

@Component({
    templateUrl: './rent-roll.component.html',
    styleUrls: ['./rent-roll.component.css']
})
export class RentRollComponent implements OnInit, OnDestroy {
    public pageLoading = true;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public rentRollSummary: ReadRentRollSummary | null = null;
    public propertyId: number;

    private alive = true;

    constructor(private rentRollService: RentRollService,
        titleService: Title,
        private route: ActivatedRoute,
        private messagingService: MessagingService,
        private modalService: ModalService
    ) {
        titleService.setTitle(`${appConfig.businessName}: Rent Roll`);
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.route.paramMap
            .pipe(takeWhile(() => this.alive))
            .subscribe((params: ParamMap) => {
                this.propertyId = +params.get('id');
                this.setRentRoll(this.propertyId);
            });
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onImportRentRollCsvClick() {
        const model = this.modalService.show(ImportRentRollComponent);
        model.propertyId = this.propertyId;
    }

    private setRentRoll(propertyId: number) {
        this.rentRollService.getSummary(propertyId)
            .pipe(takeWhile(() => this.alive))
            .subscribe(rentRollSummary => {
                this.pageLoading = false;
                this.rentRollSummary = rentRollSummary;
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

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.importedRentRollCsv) {
                    this.rentRollSummary = message.content as ReadRentRollSummary;
                }
            });
    }
}
