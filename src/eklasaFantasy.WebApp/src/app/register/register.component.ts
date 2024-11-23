// register.component.ts
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  @Output() switchToLogin = new EventEmitter<void>();
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    // Inicjalizacja formularza rejestracji
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onRegister() {
    if (this.registerForm.valid) {
      const registerData = this.registerForm.value;

      this.http.post('https://localhost:7249/api/account/register', registerData).subscribe(
        response => {
          console.log('Registration successful', response);
        },
        error => {
          console.error('Registration failed', error);
        }
      );
    }
  }

  onSwitchToLogin() {
    this.switchToLogin.emit();
  }
}
