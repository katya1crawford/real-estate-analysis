import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { JwtTokenPayloadDto } from '../dtos/jwtTokenPayloadDto';
import { ReadAuthResponse } from '../dtos/reads/readAuthResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router: Router) { }

  isLoggedIn = false;
  public authStatusListener = new Subject<boolean>();

  public getAuthStatusListener(): Observable<any> {
    return this.authStatusListener.asObservable();
  }

  public getRefreshToken(): any {
    return localStorage.getItem('refreshToken');

  }

  public getJwtToken(): any {
    return localStorage.getItem('jwtToken');
  }


  public getisAuth(): boolean {
    return this.isLoggedIn;
  }

  public getJwtTokenPayload(): JwtTokenPayloadDto | null {
    const encryptedJwtToken: string | null = localStorage.getItem('jwtToken');

    if (encryptedJwtToken === null) {
      return null;
    }

    const parts: Array<string> = encryptedJwtToken.split('.');

    if (parts.length !== 3) {
      throw new Error('JWT must have 3 parts');
    }

    const decodedPayload = this.urlBase64Decode(parts[1]);

    if (!decodedPayload) {
      throw new Error('Cannot decode the token');
    }

    const jsonDecoded = JSON.parse(decodedPayload);

    const jwtTokenPayload: JwtTokenPayloadDto = new JwtTokenPayloadDto(
      jsonDecoded.nameid,
      jsonDecoded.email,
      jsonDecoded.given_name,
      jsonDecoded.family_name,
      jsonDecoded.exp);

    return jwtTokenPayload;
  }


  private urlBase64Decode(str: string): string {
    let output = str.replace(/-/g, '+').replace(/_/g, '/');

    switch (output.length % 4) {
      case 0: { break; }
      case 2: { output += '=='; break; }
      case 3: { output += '='; break; }
      default: {
        throw new Error('Illegal base64url string!');
      }
    }

    return decodeURIComponent(escape(typeof window === 'undefined' ? atob(output) : window.atob(output)));
  }


  public storeOAuthPayloadInSession(payload: ReadAuthResponse): void {
    localStorage.setItem('jwtToken', payload.token);
    localStorage.setItem('refreshToken', payload.refreshToken);

    const jwtTokenPayload: JwtTokenPayloadDto | null = this.getJwtTokenPayload();
    if (jwtTokenPayload) {
      this.authStatusListener.next(true);
    }
  }


  public isSignedIn(): boolean {
    const token: JwtTokenPayloadDto = this.getJwtToken();

    if (!token || this.isTokenExpired()) {
      localStorage.removeItem('jwtToken');
      localStorage.removeItem('refreshToken');
      return false;
    }
    return true;
  }


  public getOAuthAuthorizationHeader(): string {
    const tokenType = 'bearer';
    const accessToken: string | null = localStorage.getItem('jwtToken');

    if (accessToken === null) {
      return '';
    } else {
      return `${tokenType} ${accessToken}`;
    }
  }



  private getTokenExpirationDate(): Date | null {
    const tokenPayload: JwtTokenPayloadDto | null = this.getJwtTokenPayload();

    if (tokenPayload === null || typeof tokenPayload.exp === 'undefined') {
      return null;
    }

    const date = new Date(0);
    date.setUTCSeconds(tokenPayload.exp);

    return date;
  }

  private isTokenExpired(): boolean {
    const tokenExpirationDate: Date | null = this.getTokenExpirationDate();
    const offsetSeconds = 7200; // 2 hours

    if (tokenExpirationDate === null) {
      return false;
    }

    if ((tokenExpirationDate.valueOf() > (new Date().valueOf() - (offsetSeconds * 1000)))) {
      return false;
    } else {
      return true;
    }
  }


  public signOut(): any {
    this.authStatusListener.next(false);
    this.router.navigate(['/home']);
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('refreshToken');
  }
}
