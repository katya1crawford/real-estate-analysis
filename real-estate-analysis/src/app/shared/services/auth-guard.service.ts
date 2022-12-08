import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { throwError } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }
  public redirectUrl = '';



  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isSigned = this.authService.isSignedIn();

    if (!isSigned) {

      throwError('Not valid token');
      this.handleUnauthorized(state);
      return false;
    }

    return true;

  }


  public handleUnauthorized(state: RouterStateSnapshot, redirectUrl = state.url): void {
    localStorage.removeItem('jwtToken');
    this.redirectUrl = redirectUrl;
    this.router.navigate(['account/login']);
  }

}
