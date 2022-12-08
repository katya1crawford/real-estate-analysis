import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, ObservableInput, throwError } from 'rxjs';
import { catchError, filter, finalize, switchMap, tap, take } from 'rxjs/operators';

import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ReadAuthResponse } from '../dtos/reads/readAuthResponse';
import { AccountService } from '../services/account.service';
import { WriteRefreshToken } from 'src/app/modules/rental-properties/dtos/writes/writeRefreshToken';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  private isTokenRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(private authService: AuthService, private router: Router, private accountService: AccountService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.getJwtToken()) {
      request = this.attachTokenToRequest(request);
    }

    return next.handle(request)
      .pipe(catchError(err => {
        if (err instanceof HttpErrorResponse && err.headers.has('Token-Expired')) {
          const refreshToken = this.authService.getRefreshToken();

          if (refreshToken == null) {
            this.authService.signOut();
          }

          return this.refreshToken(request, next);

        } else if (err.status === 401) {
          console.log('UNAUTHORIZE', err.error);
          this.authService.signOut();
        }

        return new Observable<HttpEvent<any>>();
      }));



  }

  private refreshToken(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isTokenRefreshing) {
      this.isTokenRefreshing = true;
      this.refreshTokenSubject.next('');

      const apiModel = new WriteRefreshToken(this.authService.getJwtToken(), this.authService.getRefreshToken());

      return this.accountService.refreshToken(apiModel).pipe(
        switchMap((authResponse: ReadAuthResponse) => {
          this.authService.storeOAuthPayloadInSession(authResponse);
          this.refreshTokenSubject.next(authResponse.token);
          return next.handle(this.attachTokenToRequest(request));
        }),
        catchError((err: HttpErrorResponse) => {
          this.authService.signOut();
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

  private attachTokenToRequest(request: HttpRequest<any>): any {
    const token = this.authService.getJwtToken();
    return request.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
  }

}
