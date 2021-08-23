import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { filter, map, switchMap } from 'rxjs/operators';
import { Generated, GenerateToken, UpdateMaxBid, User } from 'src/generated/services';
import { AuthUser } from '../core/models/auth-user';
import * as jwtDecode from 'jwt-decode';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  token: string | undefined;
  isAuthenticated$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  authUser: AuthUser | undefined;
  constructor(private generatedApi: Generated,private router:Router) {
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
  getMyDetails():Observable<User>{
    return this.isAuthenticated$.pipe(
      filter(x=>x),
      switchMap(X=> this.generatedApi.me()))
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
    this.getMyDetails();
  }
  signOut(){
    console.log('sig')
    this.delToken();
    this.router.navigate(['login']);
  }
  delToken(){
    this.token = undefined;
    localStorage.removeItem('token');
    this.isAuthenticated$.next(false);
  }
  updateMaxBid(amount:number):Observable<void>{
    return this.generatedApi.maxBid(<UpdateMaxBid>{
      amount
    });
  }
}


