import { Routes } from '@angular/router';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

export const authRoutes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'email-confirmation',
    component: EmailConfirmationComponent,
  },
];
