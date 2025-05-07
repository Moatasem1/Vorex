import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ProductApiRepository } from './infrastructure/repositories/product-api.repository';
import { ProductRepository } from './application/cryptos/repositories/product.respository';
import { CryptoRepository } from './application/cryptos/repositories/crypto-repository';
import { CryptoApiRepository } from './infrastructure/repositories/crypto-api.repository';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    { provide: ProductRepository, useClass: ProductApiRepository },
    { provide: CryptoRepository, useClass: CryptoApiRepository },
  ],
};
