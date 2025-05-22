import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import 'rxjs/add/operator/do';
import { AuthorizationService } from '../shared/services/authorization.service';
import { AccountService } from '../shared/services/account.service';
import { WriteRefreshToken } from '../shared/dtos/writes/writeRefreshToken';
import { switchMap, catchError, finalize, filter, take } from 'rxjs/operators';
import { ReadAuthResponse } from '../shared/dtos/reads/readAuthResponse';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthorizationService,
        private accountService: AccountService,
        private router: Router) { }

    private isTokenRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authService.getJwtToken()) {
            request = this.attachTokenToRequest(request);
        }

        return next.handle(request).pipe(catchError(error => {
            if (error instanceof HttpErrorResponse) {
                if (error.status === 401 && error.headers.has('Token-Expired')) {
                    const refreshToken = this.authService.getRefreshToken();

                    if (refreshToken == null) {
                        this.authService.signOut(this.router.url);
                    }

                    return this.refreshToken(request, next);
                } else if (error.status === 401) {
                    this.authService.signOut(this.router.url);
                }

                return throwError(error);
            }
        }));
    }

    private refreshToken(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isTokenRefreshing) {
            this.isTokenRefreshing = true;
            this.refreshTokenSubject.next(null);

            const apiModel = new WriteRefreshToken(this.authService.getJwtToken(), this.authService.getRefreshToken());

            return this.accountService.refreshToken(apiModel).pipe(
                switchMap((authResponse: ReadAuthResponse) => {
                    this.authService.storeOAuthPayloadInSession(authResponse);
                    this.refreshTokenSubject.next(authResponse.token);
                    return next.handle(this.attachTokenToRequest(request));
                }),
                catchError((err: HttpErrorResponse) => {
                    this.authService.signOut(this.router.url);
                    return throwError(err);
                }),
                finalize(() => {
                    this.isTokenRefreshing = false;
                }));
        } else {
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(token => {
                    return next.handle(this.attachTokenToRequest(request));
                })
            );
        }
    }

    private attachTokenToRequest(request: HttpRequest<any>) {
        const token = this.authService.getJwtToken();
        return request.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
    }
}
