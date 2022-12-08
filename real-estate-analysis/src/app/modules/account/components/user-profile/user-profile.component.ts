import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { takeWhile } from 'rxjs/operators';
import { JwtTokenPayloadDto } from 'src/app/shared/dtos/jwtTokenPayloadDto';
import { WriteUpdateUser } from 'src/app/shared/dtos/writes/writeUpdateUser';
import { AccountService } from 'src/app/shared/services/account.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { DeleteUserComponent } from '../../dialogs/delete-user/delete-user.component';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {

  constructor(private fb: FormBuilder, private accountService: AccountService,
    private dialog: MatDialog,
    private route: Router, private authService: AuthService) { }

  public alive = true;
  public profileForm: FormGroup;
  public isLoading = false;
  public isSuccess = false;
  token: null | JwtTokenPayloadDto;

  ngOnInit(): void {
    this.token = this.authService.getJwtTokenPayload();
    this.createForm();
  }


  onUpdate(): void {
    this.isLoading = true;

    const apiModel = new WriteUpdateUser(this.firstName.value, this.lastName.value, this.password.value);
    this.accountService.updateUser(apiModel)
      .pipe(takeWhile(() => this.alive))
      .subscribe(() => {
        this.profileForm.reset();
        this.isSuccess = true;
      }, (error: HttpErrorResponse) => {
        this.isLoading = false;
        console.log(error);
      }
      );
  }

  onDelete(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.height = '250px';
    dialogConfig.width = '500px';

    this.dialog.open(DeleteUserComponent, dialogConfig);
  }


  createForm(): void {
    this.profileForm = this.fb.group({
      firstName: [this.token?.firstName, [Validators.required, Validators.minLength(2)]],
      lastName: [this.token?.lastName, [Validators.required, Validators.minLength(2)]],
      email: [this.token?.email, [Validators.required, Validators.email]],
      password: ['', Validators.required],

    });
  }


  ngOnDestroy(): void {
    this.alive = false;
  }

  get firstName(): FormControl {
    return this.profileForm.get('firstName') as FormControl;
  }

  get lastName(): FormControl {
    return this.profileForm.get('lastName') as FormControl;
  }

  get email(): FormControl {
    return this.profileForm.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.profileForm.get('password') as FormControl;
  }

}
