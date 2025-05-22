import { Component, OnDestroy } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { takeWhile } from 'rxjs/operators';
import { WriteSignIn } from '../../../../shared/dtos/writes/writeSignIn';
import { AccountService } from '../../../../shared/services/account.service';
import { AuthorizationService } from '../../../../shared/services/authorization.service';
import { appConfig } from '../../../../app.config';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { RequestPasswordResetModal } from '../../modals/request-password-reset/request-password-reset.modal';
import { RegistrationComponent } from '../../modals/registration/registration.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './sign-in.component.html'
})
export class SignInComponent implements OnDestroy {
    public isSigningIn = false;
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public loginForm: UntypedFormGroup;

    private alive = true;

    constructor(private router: Router,
        private titleService: Title,
        private accountService: AccountService,
        private authorizationService: AuthorizationService,
        private formBuilder: UntypedFormBuilder,
        private modalService: ModalService) {
        titleService.setTitle(`${appConfig.businessName}: Sign in`);
        authorizationService.signOut(authorizationService.redirectUrl);
        this.createLoginForm();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onLoginFormSubmit(): void {
        this.isSigningIn = true;
        this.validationErrorResult = null;
        this.serverError = false;

        const apiModel = new WriteSignIn(this.email.value, this.password.value);

        this.accountService.signIn(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(response => {
                this.authorizationService.storeOAuthPayloadInSession(response);
                const redirect = this.authorizationService.redirectUrl ? this.authorizationService.redirectUrl : '/rentals/listing';
                this.router.navigate([redirect]);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.loginForm.reset();
                    this.isSigningIn = false;
                });
    }

    public onForgotPasswordClick(): void {
        this.modalService.show(RequestPasswordResetModal);
    }

    public onRegisterClick(): void {
        this.modalService.show(RegistrationComponent);
    }

    private createLoginForm(): void {
        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.pattern(/^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/)]],
            password: ['', Validators.required]
        });
    }

    get email(): UntypedFormControl {
        return this.loginForm.get('email') as UntypedFormControl;
    }

    get password(): UntypedFormControl {
        return this.loginForm.get('password') as UntypedFormControl;
    }
}
