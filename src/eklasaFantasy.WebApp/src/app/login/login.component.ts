import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router // Import routera do przekierowania
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const loginData = this.loginForm.value;

      this.http.post('https://localhost:7249/api/account/register', loginData)
        .subscribe(
          response => {
            console.log('Sukces logowania:', response);
            this.router.navigate(['/user']);
          },
          error => {
            console.error('Błąd logowania:', error);
          }
        );
    }
  }
}
