import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { LoginComponent } from './dialogs/login/login.component';


const routes: Routes = [
  { path: 'account/registration', component: RegistrationComponent },
  { path: 'account/user-profile', component: UserProfileComponent, canActivate: [AuthGuardService] },
  { path: 'account/reset-password', component: ResetPasswordComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes), SharedModule],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
