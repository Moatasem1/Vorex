import {
  HttpErrorResponse,
  HttpEvent,
  HttpRequest,
  HttpInterceptorFn,
  HttpHeaders,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import {
  BehaviorSubject,
  catchError,
  filter,
  Observable,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import { RefreshTokenUseCase } from '../../application/auth/usecases/refresh-token-usecase';
import {
  IRefreshTokenInput,
  IRefreshTokenResponse,
} from '../../application/auth/models/auth.model';
import { Router } from '@angular/router';
import { IError } from '../../shared/types/shared.types';
import { ErrorType } from '../../shared/constants/shared.constants';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const refreshTokenUseCase = inject(RefreshTokenUseCase);
  const router = inject(Router);

  const loginResponse = authService.getLoginResponse();
  const token = loginResponse?.token;

  if (token) {
    req = req.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
      }),
    });
  }

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      const error: IError[] = err.error?.responseData?.errors;

      if (error?.[0]?.errorType === ErrorType.Unauthorized) {
        if (!authService.isAuthenticated()) return next(req);
        return handleUnauthorizedError(
          req,
          next,
          authService,
          refreshTokenUseCase,
          router
        );
      }

      return throwError(() => err);
    })
  );
};

const handleUnauthorizedError = (
  req: HttpRequest<any>,
  next: (req: HttpRequest<any>) => Observable<HttpEvent<any>>,
  authService: AuthService,
  refreshTokenUseCase: RefreshTokenUseCase,
  router: Router
): Observable<HttpEvent<any>> => {
  if (!isRefreshing) {
    isRefreshing = true;
    refreshTokenSubject.next(null);

    const input: IRefreshTokenInput = {
      refreshToken: authService.getLoginResponse()?.refreshToken!,
    };

    return refreshTokenUseCase.execute(input).pipe(
      switchMap((tokenResponse: IRefreshTokenResponse) => {
        isRefreshing = false;
        authService.setLoginResponse({
          ...authService.getLoginResponse()!,
          token: tokenResponse.token,
          refreshToken: tokenResponse.refreshToken,
          expiration: tokenResponse.expiration,
        });
        refreshTokenSubject.next(tokenResponse.token);

        // Retry original request with new token
        return next(
          req.clone({
            setHeaders: {
              Authorization: `Bearer ${tokenResponse.token}`,
            },
          })
        );
      }),
      catchError((refreshErr) => {
        isRefreshing = false;
        authService.clearLoginResponse();
        router.navigate(['auth', 'login']);
        return throwError(() => refreshErr);
      })
    );
  } else {
    // Queue pending requests until refresh is done
    return refreshTokenSubject.pipe(
      filter((token) => token !== null),
      take(1),
      switchMap((token) =>
        next(
          req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`,
            },
          })
        )
      )
    );
  }
};
