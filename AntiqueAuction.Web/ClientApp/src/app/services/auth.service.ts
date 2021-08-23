import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiGenerated, GenerateToken, User } from 'src/generated/services';
import { AuthUser } from '../core/models/auth-user';
import * as jwtDecode from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  token: string | undefined;
  isAuthenticated$: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);
  authUser: AuthUser | undefined;
  constructor(private generatedApi: ApiGenerated) {
    const token = localStorage.getItem('token');
    if (token){
     this.setToken(token);
    }
  }
  authenticate(username: string, password: string): Promise<void> {
    return this.generatedApi
      .auth(<GenerateToken>{
        username,
        password,
      })
      .pipe(
        map((x) => {
          const token = x.token as string;
          localStorage.setItem('token', token);
          this.setToken(token);
        })
      )
      .toPromise();
  }
  setToken(token: string) {
    this.token = token;
    this.isAuthenticated$.next(true);
    this.setAuthUser(token);
  }
  setAuthUser(token: string) {
    const decode = jwtDecode.default(token) as any;
    this.authUser = <AuthUser>{
      id: decode.sid,
      name: decode.given_name,
      username: decode.sub,
    };
  }
   
}


