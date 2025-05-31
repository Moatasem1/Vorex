import { Routes } from '@angular/router';
import { CryptoAnlysisComponent } from './presentation/crypto-anlysis/crypto-anlysis.component';
import { CryptoAnlysisHistoryComponent } from './presentation/crypto-anlysis-history/crypto-anlysis-history.component';
import { CryptoComparisonComponent } from './presentation/crypto-comparison/crypto-comparison.component';
import { CryptoFavouriteComponent } from './presentation/crypto-favourite/crypto-favourite.component';
import { AuthComponent } from './presentation/auth/auth.component';
import { authRoutes } from './presentation/auth/auth.routes';
import { LoginComponent } from './presentation/auth/login/login.component';
import { authGuard } from './core/guards/auth.guard';
import { HomeComponent } from './presentation/home/home.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'auth',
    component: AuthComponent,
    data: { hideNavbar: true },
    children: authRoutes,
  },
  {
    path: 'compare',
    component: CryptoComparisonComponent,
    canActivate: [authGuard],
  },
  {
    path: 'analyze',
    component: CryptoAnlysisComponent,
  },
  {
    path: 'history',
    component: CryptoAnlysisHistoryComponent,
    canActivate: [authGuard],
  },
  {
    path: 'favorites',
    component: CryptoFavouriteComponent,
    canActivate: [authGuard],
  },
  {
    path: '**',
    component: HomeComponent,
  },
];
