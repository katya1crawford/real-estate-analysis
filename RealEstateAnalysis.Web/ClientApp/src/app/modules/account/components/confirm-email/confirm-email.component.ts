import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AccountService } from '../../../../shared/services/account.service';
import { appConfig } from '../../../../app.config';
import { takeWhile } from 'rxjs/operators';
import { WriteConfirmEmail } from '../../../../shared/dtos/writes/writeConfirmEmail';

@Component({
    templateUrl: './confirm-email.component.html'
})
export class ConfirmEmailComponent implements OnInit, OnDestroy {
    public isConfirming = true;
    public emailConfirmError = false;

    private alive = true;
    private userId: string | null;
    private token: string | null;

    constructor(private titleService: Title,
        private accountService: AccountService,
        private route: ActivatedRoute,
        private router: Router) {
        titleService.setTitle(`${appConfig.businessName}: Confirm Email`);
    }

    ngOnInit() {
        this.route.queryParamMap
            .pipe(takeWhile(() => this.alive))
            .subscribe((params: ParamMap) => {
                this.userId = params.get('userId');
                this.token = params.get('token');

                if (!this.userId || !this.token) {
                    this.router.navigate(['/sign-in']);
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
