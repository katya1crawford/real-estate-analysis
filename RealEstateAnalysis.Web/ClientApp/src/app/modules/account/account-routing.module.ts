import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuardService } from '../../shared/services/auth-guard.service';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'sign-in', component: SignInComponent },
        { path: 'reset-password', component: ResetPasswordComponent },
        { path: 'confirm-email', component: ConfirmEmailComponent },
        { path: 'user-profile', component: UserProfileComponent, canActivate: [AuthGuardService], data: { roles: ['Member'] } }
    ])],
    exports: [RouterModule]
})
export class AccountRoutingModule { }
