import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  @Output() switchView= new EventEmitter<string>();
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*])')
      ]]
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

  goToLogin() {
    this.switchView.emit('register');
  }

  get passwordErrors() {
    const passwordControl = this.registerForm.get('password');
    if (passwordControl?.hasError('minlength')) {
      return 'Hasło musi mieć co najmniej 8 znaków';
    }
    if (passwordControl?.hasError('pattern')) {
      return 'Password must contain at least one uppercase letter, one lowercase letter, and one special character';
    }
    return '';
  }
}
