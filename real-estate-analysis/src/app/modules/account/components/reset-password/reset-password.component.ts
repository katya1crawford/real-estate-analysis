import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { WriteRequestPasswordReset } from 'src/app/shared/dtos/writes/writeRequestPasswordReset';
import { AccountService } from 'src/app/shared/services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  constructor(private fb: FormBuilder, private accountService: AccountService, private route: Router, private location: Location) { }


  public passwordResetForm: FormGroup;
  public validationErrorResult: any[];
  public serverError = false;
  public isProcessing = false;
  public isSuccess = false;



  ngOnInit(): void {
    this.passwordResetForm = this.fb.group({
      email: ['', Validators.required]
    });
  }

  onGoBack(): void {
    this.location.back();
  }

  onRequestPasswordReset(): void {
    this.isProcessing = true;
    const apiModel = new WriteRequestPasswordReset(this.email.value);

    this.accountService.requestPasswordReset(apiModel).subscribe(() => {

      this.isSuccess = true;
      setTimeout(() => {
        this.route.navigate(['/home']);
        this.isProcessing = false;

      }, 3000);
    }, (error: HttpErrorResponse) => {
      this.isSuccess = false;
      // this.errorResponse = error.error[0];
      console.log(error);
      this.isProcessing = false;
    });
  }


  get email(): FormControl {
    return this.passwordResetForm.get('email') as FormControl;
  }

}
