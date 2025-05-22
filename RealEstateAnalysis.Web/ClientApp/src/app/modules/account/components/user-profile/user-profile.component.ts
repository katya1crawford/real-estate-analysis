import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { takeWhile } from 'rxjs/operators';
import { AuthorizationService } from '../../../../shared/services/authorization.service';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { AccountService } from '../../../../shared/services/account.service';
import { appConfig } from '../../../../app.config';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { WriteUpdateUser } from '../../../../shared/dtos/writes/writeUpdateUser';
import { WriteRefreshToken } from '../../../../shared/dtos/writes/writeRefreshToken';
import { MessageType } from '../../../../shared/enums/messageType';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './user-profile.component.html'
})
export class UserProfileComponent implements OnInit, OnDestroy {
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public personalInfoForm: UntypedFormGroup;
    public isUpdatingProfile = false;
    public showProfileSuccessfullyUpdated = false;

    private alive = true;

    constructor(private titleService: Title,
        private authorizationService: AuthorizationService,
        private accountService: AccountService,
        private modalService: ModalService,
        private messagingService: MessagingService,
        private formBuilder: UntypedFormBuilder) {
        titleService.setTitle(`${appConfig.businessName}: User Profile`);
        this.createPersonalInfoForm();
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.setCreatePersonalInfoFormValues();
    }

    ngOnDestroy(): void {
        this.alive = false;
    }

    public onDeleteAccountClick(): void {
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `All data related to your account will be lost. Are you sure that you want to delete your account?`, MessageType.deleteUserAccount);
    }

    private deleteAccount(): void {
        this.accountService.deleteAccount()
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.authorizationService.signOut();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    public onPersonalInfoFormSubmit(): void {
        this.isUpdatingProfile = true;
        const writeUpdateUser = new WriteUpdateUser(this.firstName.value, this.lastName.value, this.password.value);

        this.accountService.updateUser(writeUpdateUser)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const writeRefreshToken = new WriteRefreshToken(this.authorizationService.getJwtToken(), this.authorizationService.getRefreshToken());
                this.accountService.refreshToken(writeRefreshToken)
                    .pipe(takeWhile(() => this.alive))
                    .subscribe((authResponse) => {
                        this.authorizationService.storeOAuthPayloadInSession(authResponse);
                        this.isUpdatingProfile = false;
                        this.showProfileSuccessfullyUpdated = true;
                        this.password.reset();
                        this.serverError = false;
                        this.validationErrorResult = null;

                        setTimeout(() => {
                            this.showProfileSuccessfullyUpdated = false;
                        }, 5000);
                    },
                        (error: HttpErrorResponse) => {
                            if (error.status === 400) {
                                this.validationErrorResult = error.error as ReadValidationResult;
                            } else {
                                this.serverError = true;
                            }

                            this.password.reset();
                            this.isUpdatingProfile = false;
                        });
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.password.reset();
                    this.isUpdatingProfile = false;
                });
    }

    private createPersonalInfoForm(): void {
        this.personalInfoForm = this.formBuilder.group({
            firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
            lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
            password: ['', [Validators.required]]
        });
    }

    private setCreatePersonalInfoFormValues(): void {
        this.accountService.getLoggedIntUser()
            .pipe(takeWhile(() => this.alive))
            .subscribe(user => {
                this.firstName.patchValue(user.firstName);
                this.lastName.patchValue(user.lastName);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.deleteUserAccount) {
                    this.deleteAccount();
                }
            });
    }

    public get firstName(): UntypedFormControl {
        return this.personalInfoForm.get('firstName') as UntypedFormControl;
    }

    public get lastName(): UntypedFormControl {
        return this.personalInfoForm.get('lastName') as UntypedFormControl;
    }

    public get password(): UntypedFormControl {
        return this.personalInfoForm.get('password') as UntypedFormControl;
    }
}
