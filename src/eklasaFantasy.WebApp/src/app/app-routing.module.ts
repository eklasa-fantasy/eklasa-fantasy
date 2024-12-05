import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { LoggedInComponent } from './logged-in/logged-in.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { MatchListComponent } from './match-list/match-list.component';
import { NotLoggedInComponent } from './not-logged-in/not-logged-in.component';
import { ResultsComponent } from './results/results.component';

const routes: Routes = [
  { path: '', component: MainComponent }, 
 // { path: '**', redirectTo: '' },
  { path: 'user', component: LoggedInComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent},
  { path: 'match-list', component: MatchListComponent},
  { path: 'not-logged-in', component: NotLoggedInComponent},
  { path: 'results', component: ResultsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
