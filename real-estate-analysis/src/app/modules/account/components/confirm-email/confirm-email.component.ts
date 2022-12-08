import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AccountService } from 'src/app/shared/services/account.service';
import { takeWhile } from 'rxjs/operators';
import { WriteConfirmEmail } from 'src/app/shared/dtos/writes/WriteConfirmEmail';


@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {

  public isConfirming = true;
  public emailConfirmError = false;

  private alive = true;
  private userId: string | null;
  private token: string | null;

  constructor(
    private accountService: AccountService,
    private route: ActivatedRoute,
    private router: Router) {

  }

  ngOnInit() {
    this.route.queryParamMap
      .pipe(takeWhile(() => this.alive))
      .subscribe((params: ParamMap) => {
        this.userId = params.get('userId');
        this.token = params.get('token');

        if (!this.userId || !this.token) {
          this.router.navigate(['/home']);
        } else {
          const apiModel = new WriteConfirmEmail(this.userId, this.token);
          this.accountService.confirmEmail(apiModel)
            .pipe(takeWhile(() => this.alive))
            .subscribe(
              () => {
                this.emailConfirmError = false;
                this.isConfirming = false;
              },
              () => {
                this.emailConfirmError = true;
                this.isConfirming = false;
              }
            );
        }
      });
  }

  ngOnDestroy() {
    this.alive = false;
  }
}
