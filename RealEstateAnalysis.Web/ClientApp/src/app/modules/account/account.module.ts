import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountRoutingModule } from './account-routing.module';
import { ErrorSummaryModule } from '../../shared/directives/error-summary/error-summary.module';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { AccountService } from '../../shared/services/account.service';
import { RegistrationComponent } from './modals/registration/registration.component';
import { RequestPasswordResetModal } from './modals/request-password-reset/request-password-reset.modal';
import { ConfirmModule } from '../../shared/modals/confirm/confirm.module';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AccountRoutingModule,
        ErrorSummaryModule,
        ConfirmModule
    ],
    declarations: [
        SignInComponent,
        RegistrationComponent,
        RequestPasswordResetModal,
        UserProfileComponent,
        ResetPasswordComponent,
        ConfirmEmailComponent
    ],
    providers: [
        AccountService
    ]
})
export class AccountModule { }
