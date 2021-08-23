import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ParseUtil } from 'src/app/core/utils';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  errorMsg:any;
  form!: FormGroup;
  loading = false;
  constructor(private authService:AuthService,
    private router: Router,private formBuilder: FormBuilder,) { }

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(x=> this.router.navigate(['']));
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['abc123', Validators.required]
  });
  }
  async onSubmit() {
    this.errorMsg = null;
    // stop here if form is invalid
    if (this.form.invalid) {
      return;
  }
    this.loading = true;
    try {
      await this.authService.authenticate(this.form.value.username, this.form.value.password);
      this.router.navigate(['']);
    } catch (error) {
      this.loading = false
      this.errorMsg = ParseUtil.error(error)?.description;
    }
}

}
