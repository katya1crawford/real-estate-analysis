import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { JwtTokenPayloadDto } from '../dtos/jwtTokenPayloadDto';
import { AuthorizationService } from './authorization.service';

@Injectable()
export class AuthGuardService  {
    constructor(private authorizationService: AuthorizationService, private router: Router) { }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authorizationService.isSignedIn()) {
            const routeRoles: string[] | null = <string[]>route.data.roles || null;
            const jwtTokenPayload: JwtTokenPayloadDto | null = this.authorizationService.getJwtTokenPayload();

            if (!jwtTokenPayload) {
                this.handleUnauthorized(state);
                return false;
            }

            if (!routeRoles || routeRoles.length === 0) {
                return true;
            }

            if (jwtTokenPayload.roles.length === 0) {
                this.handleUnauthorized(state);
                return false;
            }

            for (let i = 0; i < routeRoles.length; i++) {
                for (let n = 0; n < jwtTokenPayload.roles.length; n++) {
                    if (routeRoles[i].toUpperCase() === jwtTokenPayload.roles[n].toUpperCase()) {
                        return true;
                    }
                }
            }

            this.handleUnauthorized(state);
            return false;
        } else {
            this.handleUnauthorized(state);
            return false;
        }
    }

    private handleUnauthorized(state: RouterStateSnapshot): void {
        this.authorizationService.signOut(state.url);
    }
}
