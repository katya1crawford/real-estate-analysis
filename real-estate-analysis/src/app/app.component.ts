import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Observable, Subject, Subscription } from 'rxjs';
import { LoginComponent } from './modules/account/dialogs/login/login.component';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public isAuthorized: boolean;
  constructor(private dialog: MatDialog, private authService: AuthService) { }
  public subscription: Subscription;

  ngOnInit(): void {
    this.isAuthorized = this.authService.isSignedIn();
    this.subscription = this.authService.getAuthStatusListener().subscribe(status => {
      status ? this.isAuthorized = true : this.isAuthorized = false;
    });
  }

  onLogin(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    this.dialog.open(LoginComponent, dialogConfig);
  }

  onLogOut(): void {
    this.authService.signOut();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
