import { Component, OnDestroy, OnInit } from '@angular/core';
import { JwtTokenPayloadDto } from '../../shared/dtos/jwtTokenPayloadDto';
import { ContactUsComponent } from '../../shared/modals/contact-us/contact-us.component';
import { AuthorizationService } from '../../shared/services/authorization.service';
import { appConfig } from '../../app.config';
import { takeWhile } from 'rxjs/operators';
import { ModalService } from '../../shared/services/modal/modal.service';

@Component({
    selector: 'app-outlet',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {
    public isSignedIn = false;
    public userFullName = '';
    public currentYear: number;
    public appName: string;

    private alive = true;

    constructor(private authorizationService: AuthorizationService,
        private modalService: ModalService
    ) {
        this.subscribeToAuthStatus();
        this.currentYear = new Date().getFullYear();
        this.appName = appConfig.businessName;
    }

    ngOnInit() {
        this.authorizationService.isSignedIn();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onSignOut(): void {
        this.authorizationService.signOut();
    }

    public onContactUs(): void {
        this.modalService.show(ContactUsComponent);
    }

    private subscribeToAuthStatus() {
        this.authorizationService.authStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe(jwtTokenPayload => this.updateAuthStatus(jwtTokenPayload));
    }

    private updateAuthStatus(jwtTokenPayload: JwtTokenPayloadDto | null): void {
        if (jwtTokenPayload !== null) {
            this.isSignedIn = true;
            this.userFullName = `${jwtTokenPayload.firstName} ${jwtTokenPayload.lastName}`;
        } else {
            this.isSignedIn = false;
            this.userFullName = '';
        }
    }
}
