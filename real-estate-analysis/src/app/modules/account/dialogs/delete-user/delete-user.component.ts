import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AccountService } from 'src/app/shared/services/account.service';
import { LoginComponent } from '../login/login.component';
import { AuthService } from 'src/app/shared/services/auth.service';
import { takeWhile } from 'rxjs/operators';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';



@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnDestroy {

  constructor(public dialogRef: MatDialogRef<LoginComponent>, private route: Router,
    private accountService: AccountService, private authService: AuthService) { }

  public isLoading = false;
  public alive = true;
  public errorMessage = '';


  onDeleteAccount(): void {
    this.isLoading = true;

    this.accountService.deleteAccount()
      .pipe(takeWhile(() => this.alive))
      .subscribe(() => {
        this.isLoading = false;
        this.authService.signOut();
      },
        (error: HttpErrorResponse) => {
          this.errorMessage = "Something went wrong, try later.";
          console.log(error);
        })
  }


  ngOnDestroy(): void {
    this.alive = false;
  }

}
