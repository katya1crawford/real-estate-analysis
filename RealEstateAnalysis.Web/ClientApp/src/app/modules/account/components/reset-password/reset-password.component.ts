import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { AccountService } from '../../../../shared/services/account.service';
import { appConfig } from '../../../../app.config';
import { WritePasswordReset } from '../../../../shared/dtos/writes/writePasswordReset';
import { passwordMatcher } from '../../../../shared/validators/shared.validators';

@Component({
    styleUrls: ['./reset-password.component.css'],
    templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit, OnDestroy {
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public passwordResetForm: UntypedFormGroup;
    public isReseting = false;

    private alive = true;
    private userId: string | null;
    private token: string | null;

    constructor(private titleService: Title,
        private accountService: AccountService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: UntypedFormBuilder) {
        titleService.setTitle(`${appConfig.businessName}: Reset Password`);
        this.createPasswordResetForm();
    }

    ngOnInit(): void {
        this.route.queryParamMap
            .pipe(takeWhile(() => this.alive))
            .subscribe((params: ParamMap) => {
                this.userId = params.get('userId');
                this.token = params.get('token');

                if (!this.userId || !this.token) {
                    this.router.navigate(['/sign-in']);
                }
            });
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onPasswordResetFormSubmit(): void {
        this.validationErrorResult = null;
        this.serverError = false;
        this.isReseting = true;
        const apiModel = new WritePasswordReset(this.userId, this.token, this.password.value);

        this.accountService.resetPassword(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => this.router.navigate(['/sign-in']),
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.passwordResetForm.reset();
                    this.isReseting = false;
                });
    }

    private createPasswordResetForm(): void {
        this.passwordResetForm = this.formBuilder.group({
            password: ['', [Validators.required, Validators.minLength(5)]],
            confirmPassword: ['', Validators.required]
        }, { validator: passwordMatcher('password', 'confirmPassword') });
    }

    get password(): UntypedFormControl {
        return this.passwordResetForm.get('password') as UntypedFormControl;
    }

    get confirmPassword(): UntypedFormControl {
        return this.passwordResetForm.get('confirmPassword') as UntypedFormControl;
    }
}
