import { Component, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators, AbstractControl } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { takeWhile } from 'rxjs/operators';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { passwordMatcher } from '../../../../shared/validators/shared.validators';
import { AccountService } from '../../../../shared/services/account.service';
import { WriteRegistration } from '../../../../shared/dtos/writes/writeRegistration';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './registration.component.html'
})
export class RegistrationComponent implements OnDestroy {
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public registrationForm: UntypedFormGroup;
    public success = false;
    public isProcessing = false;

    private alive = true;

    constructor(private modalService: ModalService,
        private formBuilder: UntypedFormBuilder,
        private accountService: AccountService) {
        this.createRegistrationForm();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onRegistrationFormSubmit(): void {
        this.validationErrorResult = null;
        this.serverError = false;
        this.isProcessing = true;

        const apiModel = new WriteRegistration(this.firstName.value,
            this.lastName.value,
            this.email.value,
            this.password.value);

        this.accountService.register(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.registrationForm.reset();
                this.success = true;
                this.isProcessing = false;

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

                    this.isProcessing = false;
                });
    }

    get firstName(): UntypedFormControl {
        return this.registrationForm.get('firstName') as UntypedFormControl;
    }

    get lastName(): UntypedFormControl {
        return this.registrationForm.get('lastName') as UntypedFormControl;
    }

    get email(): UntypedFormControl {
        return this.registrationForm.get('email') as UntypedFormControl;
    }

    get password(): UntypedFormControl {
        return this.registrationForm.get('password') as UntypedFormControl;
    }

    get confirmPassword(): UntypedFormControl {
        return this.registrationForm.get('confirmPassword') as UntypedFormControl;
    }

    private createRegistrationForm(): void {
        this.registrationForm = this.formBuilder.group({
            firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
            lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
            email: ['', [Validators.required, Validators.pattern(/^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/), Validators.maxLength(100)]],
            password: ['', [Validators.required, Validators.minLength(5)]],
            confirmPassword: ['', Validators.required]
        }, { validator: passwordMatcher('password', 'confirmPassword') });
    }
}
