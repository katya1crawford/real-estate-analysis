import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public isAuthorized: boolean;
  public subscription: Subscription;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isAuthorized = this.authService.isSignedIn();
    this.subscription = this.authService.getAuthStatusListener().subscribe(status => {
      status ? this.isAuthorized = true : this.isAuthorized = false;
    });
  }

}
