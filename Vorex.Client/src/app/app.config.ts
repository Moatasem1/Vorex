import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ProductApiRepository } from './infrastructure/repositories/product-api.repository';
import { ProductRepository } from './application/cryptos/repositories/product.respository';
import { CryptoRepository } from './application/cryptos/repositories/crypto-repository';
import { CryptoApiRepository } from './infrastructure/repositories/crypto-api.repository';
import { UserRepository } from './application/users/repositories/user-repository';
import { UserApiRepository } from './infrastructure/repositories/user-api.respository';
import { provideAnimations } from '@angular/platform-browser/animations';
import { AuthRepository } from './application/auth/repositories/auth.repository';
import { AuthApiRepository } from './infrastructure/repositories/auth-api.repository';
import { httpErrorInterceptor } from './core/interceptors/http-error.interceptor';
import { authInterceptor } from './core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([authInterceptor, httpErrorInterceptor])
    ),
    provideAnimations(),
    { provide: ProductRepository, useClass: ProductApiRepository },
    { provide: CryptoRepository, useClass: CryptoApiRepository },
    { provide: UserRepository, useClass: UserApiRepository },
    { provide: AuthRepository, useClass: AuthApiRepository },
  ],
};
