import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { tokenGetter } from '../app.module';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      const decodedToken = this.jwtHelper.decodeToken(tokenGetter());
      const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      if(role != "Admin"){
        this.router.navigate(['auth'])
        return false 
      }else{
        return true
      }
    }

    this.router.navigate(['auth'])
    return false
  }
}