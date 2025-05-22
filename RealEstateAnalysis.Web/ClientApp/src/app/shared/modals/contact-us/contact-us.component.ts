import { Component, OnInit, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { JwtTokenPayloadDto } from '../../dtos/jwtTokenPayloadDto';
import { AuthorizationService } from '../../services/authorization.service';
import { ContactUsService } from '../../services/contact-us.service';
import { WriteContactUs } from '../../dtos/writes/writeContactUs';
import { ReadValidationResult } from '../../dtos/reads/readValidationResult';
import { ModalService } from '../../services/modal/modal.service';

@Component({
    templateUrl: './contact-us.component.html'
})
export class ContactUsComponent implements OnInit, OnDestroy {
    public contactUsForm: UntypedFormGroup;
    public isSendingEmail = false;
    public success = false;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;

    private alive = true;

    constructor(private modalService: ModalService,
        private formBuilder: UntypedFormBuilder,
        private authorizationService: AuthorizationService,
        private contactUsService: ContactUsService) {
        this.createContactUsForm();
    }

    ngOnInit(): void {
        if (this.authorizationService.isSignedIn()) {
            this.setFromEmail();
        }
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onContactUsFormSubmit(): void {
        this.validationErrorResult = null;
        this.serverError = false;
        this.isSendingEmail = true;

        const apiModel = new WriteContactUs(this.fromEmail.value, this.subject.value, this.message.value);

        this.contactUsService.sendEmail(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.success = true;
                this.contactUsForm.reset();
                this.isSendingEmail = false;

                setTimeout(() => {
                    this.modalService.hide();
                }, 5000);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.contactUsForm.reset();
                    this.isSendingEmail = false;
                });
    }

    private createContactUsForm(): void {
        this.contactUsForm = this.formBuilder.group({
            fromEmail: ['', [Validators.required, Validators.maxLength(100), Validators.pattern(/^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/)]],
            subject: ['', [Validators.required, Validators.maxLength(150)]],
            message: ['', [Validators.required, Validators.maxLength(1000)]]
        });
    }

    public get fromEmail(): UntypedFormControl {
        return this.contactUsForm.get('fromEmail') as UntypedFormControl;
    }

    public get subject(): UntypedFormControl {
        return this.contactUsForm.get('subject') as UntypedFormControl;
    }

    public get message(): UntypedFormControl {
        return this.contactUsForm.get('message') as UntypedFormControl;
    }

    private setFromEmail(): void {
        const jwtTokenPayload: JwtTokenPayloadDto | null = this.authorizationService.getJwtTokenPayload();

        if (jwtTokenPayload !== null) {
            this.fromEmail.patchValue(jwtTokenPayload.email);
        }
    }
}
