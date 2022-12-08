import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WriteSignIn } from 'src/app/shared/dtos/writes/writeSignIn';
import { AccountService } from 'src/app/shared/services/account.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { MatDialogRef } from '@angular/material/dialog';
import { takeWhile } from 'rxjs/operators';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {

  constructor(
    public dialogRef: MatDialogRef<LoginComponent>,
    private fb: UntypedFormBuilder, private accountService: AccountService,
    private authService: AuthService, private nav: Router) { this.createLoginForm() }

  public loginForm!: FormGroup | any;
  public hide = true;
  public alive = true;
  public isLoading = false;
  public serverError = false;
  public errorMessage: string;



  createLoginForm(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],

    });
  }


  onLoginFormSubmit(): void {
    this.isLoading = true;

    const loginApiModel = new WriteSignIn(this.email.value, this.password.value);

    this.accountService.login(loginApiModel)
      .pipe(takeWhile(() => this.alive))
      .subscribe((response: any) => {
        this.authService.storeOAuthPayloadInSession(response);
        if (response) {
          this.loginForm.reset();
          this.isLoading = false;
          this.dialogRef.close();
        }
      },
        (error: HttpErrorResponse) => {
          this.serverError = true;
          if (error.status === 400) {
            this.errorMessage = "Invalid email or password";

          } else {
            this.errorMessage = 'Sorry, something went wrong';
          }

          this.loginForm.reset();
          this.isLoading = false;

        });

  }

  ngOnDestroy(): void {
    this.alive = false;
  }

  get email(): FormControl {
    return this.loginForm.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.loginForm.get('password') as FormControl;
  }
}
