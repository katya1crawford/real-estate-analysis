import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { MessageDto } from 'src/app/shared/dtos/messageDto';
import { ReadValidationResult } from 'src/app/shared/dtos/reads/readValidationResult';
import { MessageType } from 'src/app/shared/enums/messageType';
import { MessagingService } from 'src/app/shared/services/messaging.service';
import { ModalService } from 'src/app/shared/services/modal/modal.service';
import { CityService } from '../../services/city.service';

@Component({
    templateUrl: './harvest-city-data.component.html',
})
export class HarvestCityDataComponent implements OnInit, OnDestroy {
    public messageType: MessageType;
    public modalTitle = 'Harvest City Data';
    public harvestCityDataForm: UntypedFormGroup;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public isHarvestingCityData = false;

    private alive = true;

    constructor(private modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder,
        private cityService: CityService) {
        this.createHarvestCityDataForm();
    }

    ngOnInit(): void {
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onHarvestCityDataFormSubmit(harvestCityDataNgForm: FormGroupDirective): void {
        if (harvestCityDataNgForm.invalid) {
            return;
        }

        this.isHarvestingCityData = true;
        this.validationErrorResult = null;
        this.serverError = false;

        this.cityService.harvestCityData(+this.minimumPopulationCount.value)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.isHarvestingCityData = false;
                const message: MessageDto = new MessageDto(this.messageType, null);
                this.messagingService.sendMessage(message);
                this.modalService.hide();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.isHarvestingCityData = false;
                });
    }

    private createHarvestCityDataForm(): void {
        this.harvestCityDataForm = this.formBuilder.group({
            minimumPopulationCount: ['', [Validators.required, Validators.min(0)]]
        });
    }

    get minimumPopulationCount(): UntypedFormControl {
        return this.harvestCityDataForm.get('minimumPopulationCount') as UntypedFormControl;
    }
}
