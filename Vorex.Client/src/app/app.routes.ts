import { Routes } from '@angular/router';
import { ProductListComponent } from './presentation/product-list/product-list.component';
import { CryptoAnlysisComponent } from './presentation/crypto-anlysis/crypto-anlysis.component';

export const routes: Routes = [
  {
    path: 'products',
    component: ProductListComponent,
  },
  {
    path: 'crypto-anlysis',
    component: CryptoAnlysisComponent,
  },
  {
    path: '',
    redirectTo: '/crypto-anlysis',
    pathMatch: 'full',
  },
];
