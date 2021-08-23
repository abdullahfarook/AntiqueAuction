import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, switchMap } from 'rxjs/operators';
import { User } from 'src/generated/services';
import { ParseUtil } from '../core/utils';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user$!: Observable<User>;
  maxBidMsg:string|undefined;
  toggleSidebar = false;
  constructor(public authService:AuthService) {
   this.getUserDetails();
  }

  ngOnInit(): void {
  }
  getUserDetails(){
    this.user$ = this.authService.isAuthenticated$.pipe(
      filter(x=>x),
      switchMap(x=> this.authService.getMyDetails()));
  }
  async updateMaxBid(amount:number){
    try {
      await this.authService.updateMaxBid(amount).toPromise();
      this.getUserDetails();
    } catch (error) {
      this.maxBidMsg = ParseUtil.error(error)?.description

    }
  }
}
