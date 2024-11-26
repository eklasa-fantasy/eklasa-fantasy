import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  isLoggedIn = false;  
  isLoginScreen = false;  
  showLogin: boolean = true;
  currentView: string = 'login'; // Domyślnie widok logowania

  constructor(private router: Router) {}
    
  switchView(view: string): void {
     this.currentView = view;
  }

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

  goToLogin(){
    this.switchView('login');
  }
  goToRegister(){
    this.switchView('register');
  }
  
}
