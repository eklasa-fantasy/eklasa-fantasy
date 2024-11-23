import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  isLoggedIn = false;  
  isLoginScreen = false;  

  constructor(private router: Router) {}

  toggleAccount() {
    if (this.isLoggedIn) {
      this.isLoggedIn = false;
      this.isLoginScreen = false;
    } else {
      this.isLoginScreen = !this.isLoginScreen;
    }
  }

  loginSuccess() {
    this.isLoggedIn = true;
    this.isLoginScreen = false;
  }
}
