import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { JwtTokenPayloadDto } from '../dtos/jwtTokenPayloadDto';
import { ReadAuthResponse } from '../dtos/reads/readAuthResponse';
import { Router } from '@angular/router';

@Injectable()
export class AuthorizationService {
    public redirectUrl = '';
    private authStatusSource = new Subject<JwtTokenPayloadDto | null>();
    public authStatus = this.authStatusSource.asObservable();

    constructor(private router: Router) { }

    public storeOAuthPayloadInSession(payload: ReadAuthResponse): void {
        localStorage.setItem('jwtToken', payload.token);
        localStorage.setItem('refreshToken', payload.refreshToken);
        const jwtTokenPayload: JwtTokenPayloadDto | null = this.getJwtTokenPayload();
        this.authStatusSource.next(jwtTokenPayload);
    }

    public signOut(redirectUrl = ''): void {
        this.removeTokenFromLocalStorage();
        this.authStatusSource.next(null);
        this.redirectUrl = redirectUrl;
        this.router.navigate(['/sign-in']);
    }

    public getRefreshToken(): string | null {
        return localStorage.getItem('refreshToken');
    }

    public getJwtToken(): string | null {
        return localStorage.getItem('jwtToken');
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
        const roles: Array<string> = this.mapUserRoles(jsonDecoded.role);

        const jwtTokenPayload: JwtTokenPayloadDto = new JwtTokenPayloadDto(jsonDecoded.nameid,
            roles,
            jsonDecoded.email,
            jsonDecoded.given_name,
            jsonDecoded.family_name,
            jsonDecoded.exp);

        return jwtTokenPayload;
    }

    public isSignedIn(): boolean {
        const jwtTokenPayload: JwtTokenPayloadDto | null = this.getJwtTokenPayload();

        if (jwtTokenPayload === null || this.isTokenExpired()) {
            this.removeTokenFromLocalStorage();
            this.authStatusSource.next(null);
            return false;
        }

        this.authStatusSource.next(jwtTokenPayload);
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

    private mapUserRoles(role: string | string[]): Array<string> {
        if (Array.isArray(role)) {
            return role;
        } else {
            return [role];
        }
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

    private getTokenExpirationDate(): Date | null {
        const tokenPayload: JwtTokenPayloadDto | null = this.getJwtTokenPayload();

        if (tokenPayload === null || typeof tokenPayload.exp === 'undefined') {
            return null;
        }

        const date = new Date(0);
        date.setUTCSeconds(tokenPayload.exp);

        return date;
    }

    private removeTokenFromLocalStorage() {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('refreshToken');
    }
}
