import { Component, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { AccountService } from '../../../../shared/services/account.service';
import { WriteRequestPasswordReset } from '../../../../shared/dtos/writes/writeRequestPasswordReset';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './request-password-reset.modal.html'
})
// tslint:disable-next-line: component-class-suffix
export class RequestPasswordResetModal implements OnDestroy {
    public requestPasswordResetForm: UntypedFormGroup;
    public isSendingPasswordResetEmail = false;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public success = false;

    private alive = true;

    constructor(private modalService: ModalService,
        private formBuilder: UntypedFormBuilder,
        private accountService: AccountService) {
        this.createRequestPasswordResetForm();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onRequestPasswordResetFormSubmit(): void {
        this.validationErrorResult = null;
        this.serverError = false;
        this.isSendingPasswordResetEmail = true;

        const apiModel = new WriteRequestPasswordReset(this.email.value);

        this.accountService.requestPasswordReset(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.success = true;
                this.requestPasswordResetForm.reset();
                this.isSendingPasswordResetEmail = false;

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

                    this.requestPasswordResetForm.reset();
                    this.isSendingPasswordResetEmail = false;
                });
    }

    private createRequestPasswordResetForm(): void {
        this.requestPasswordResetForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.pattern(/^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/), Validators.maxLength(100)]]
        });
    }

    get email(): UntypedFormControl {
        return this.requestPasswordResetForm.get('email') as UntypedFormControl;
    }
}
