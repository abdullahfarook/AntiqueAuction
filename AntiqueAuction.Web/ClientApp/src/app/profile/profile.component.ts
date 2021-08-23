import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  isAuthenticated = false
  constructor(public authService:AuthService) { 
    authService.isAuthenticated$.subscribe(x=> this.isAuthenticated = x);
  }

  ngOnInit(): void {
  }

}
