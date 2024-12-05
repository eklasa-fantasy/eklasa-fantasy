import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {
  @Output() switchView = new EventEmitter<string>();
  forgotPasswordForm: FormGroup;
  isSubmitted: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  goToForgotPassword(): void {
    this.switchView.emit('forgotPassword');
  }

  onSubmit(): void {
    if (this.forgotPasswordForm.invalid) {
      this.errorMessage = 'Please enter a valid email address.';
      return;
    }

    const emailData = { email: this.forgotPasswordForm.value.email };

    this.http.post('https://localhost:7249/api/account/forgotPassword', emailData)
      .subscribe({
        next: (response: any) => {
          this.isSubmitted = true;
          this.successMessage = 'Password reset link sent to your email.';
          this.errorMessage = '';
        },
        error: (error) => {
          this.errorMessage = 'Failed to send password reset email. Please try again.';
          this.successMessage = '';
        }
      });
  }
}
