import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { LoggedInComponent } from './logged-in/logged-in.component';

const routes: Routes = [
  { path: '', component: MainComponent }, 
 // { path: '**', redirectTo: '' },
  { path: 'login', component: LoginComponent },
  { path: 'user', component: LoggedInComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
