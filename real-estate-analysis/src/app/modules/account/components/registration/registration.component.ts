import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { WriteRegistration } from 'src/app/shared/dtos/writes/writeRegistration';
import { AccountService } from 'src/app/shared/services/account.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountService: AccountService, breakpointObserver: BreakpointObserver) {
    breakpointObserver.observe([
      Breakpoints.XSmall,
      Breakpoints.Small
    ])
      .pipe(takeWhile(() => this.alive))
      .subscribe(result => {
        this.isSmall = result.matches;
      });
  }


  public credentials: FormGroup;
  public name: FormGroup;
  public isSmall: boolean;
  public isProcessing = false;
  public form: FormGroup;
  public isEditable = true;
  public isSuccess = false;
  private alive = true;


  ngOnInit(): void {

    this.name = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    }),

      this.credentials = this.fb.group({
        email: ['', Validators.required],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required]
      })

  }


  onRegistrationFormSubmit(): void {
    this.isProcessing = true;

    const registerApiModel = new WriteRegistration(this.firstName.value, this.lastName.value, this.email.value, this.password.value);

    this.accountService.register(registerApiModel)
      .subscribe(() => {
        this.form.reset();
        this.isSuccess = true;
        this.isProcessing = false;
      },
        (error: HttpErrorResponse) => {
          if (error.status === 400) {
            console.log(error);
          }

          this.form.reset();
          this.isProcessing = false;
          this.isSuccess = false;

        });
  }



  get email(): FormControl {
    return this.credentials.get('email') as FormControl;
  }

  get firstName(): FormControl {
    return this.name.get('firstName') as FormControl;
  }

  get lastName(): FormControl {
    return this.name.get('lastName') as FormControl;
  }

  get password(): FormControl {
    return this.credentials.get('password') as FormControl;
  }

  get confirmPassword(): FormControl {
    return this.credentials.get('confirmPassword') as FormControl;
  }


}
