import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './dialogs/login/login.component';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import { RegistrationComponent } from './components/registration/registration.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { DeleteUserComponent } from './dialogs/delete-user/delete-user.component';




@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    ResetPasswordComponent,
    UserProfileComponent,
    ConfirmEmailComponent,
    DeleteUserComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ],
  exports: []
})
export class AccountModule { }
