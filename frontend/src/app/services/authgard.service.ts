import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthentificationService } from './authentification.service';

@Injectable({
  providedIn: 'root'
})
export class AuthgardService implements CanActivate {

  constructor(private authSrv: AuthentificationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authSrv.isAuthenticated()) {
      return true;
    } else {
        localStorage.setItem('path-redirect', state.url);
        this.authSrv.startAuthentication();
        return false;
    }
  }
}
