import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  @Output() switchToRegister = new EventEmitter<void>();
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router // Import routera do przekierowania
  ) {
    this.loginForm = this.fb.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const loginData = this.loginForm.value;

      this.http.post('https://localhost:7249/api/account/login', loginData)
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
  onSwitchToRegister() {
    this.switchToRegister.emit();
  }
}
