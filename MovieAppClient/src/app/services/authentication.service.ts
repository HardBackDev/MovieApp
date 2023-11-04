import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserRegister } from '../models/user_models/user-register';
import { UserForLogin } from '../models/user_models/user-for-login';
import { JwtToken } from '../models/jwt-token';
import { JwtHelperService } from '@auth0/angular-jwt';
import { tokenGetter } from '../app.module';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  authUrl: string = `${environment.apiUrl}/auth`
  constructor( private httpContext: HttpClient, private jwtHelper: JwtHelperService) { }
  
  public registerUser(user: UserRegister) {
    return this.httpContext.post<UserRegister>(this.authUrl, user, this.generateHeaders());
  }

  public loginUser(user: UserForLogin) : Observable<JwtToken> {
    return this.httpContext.post<JwtToken>(`${this.authUrl}/login`, user, this.generateHeaders());
  }

  public getRole = () => {
    if(!this.isUserAuthenticated())
      return null
    const decodedToken = this.jwtHelper.decodeToken(tokenGetter());
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role
  }

  public isUserAdmin() : boolean{
    if(!this.isUserAuthenticated())
      return false
      const decodedToken = this.jwtHelper.decodeToken(tokenGetter());
      const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      return role == 'Admin'
  }

  public getUserName = () => {
    if(!this.isUserAuthenticated())
      return null
      const decodedToken = this.jwtHelper.decodeToken(tokenGetter());
      const userName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
      return userName
      
  }

  public isUserAuthenticated = (): boolean => {
    const token = tokenGetter();
    
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    return false;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json' 
      })
    }
  }
}
