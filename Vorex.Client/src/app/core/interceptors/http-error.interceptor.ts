import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastrService } from '../../shared/services/toastr.service';
import { catchError, Observable, throwError } from 'rxjs';
import { ErrorType } from '../../shared/constants/shared.constants';
import { IError } from '../../shared/types/shared.types';
import { Router } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  let toastService = inject(ToastrService);
  let router = inject(Router);
  let authService = inject(AuthService);
  return next(req).pipe(
    catchError((err: HttpErrorResponse) =>
      hanedleError(err, toastService, router, authService)
    )
  );
};

function hanedleError(
  error: HttpErrorResponse,
  toastService: ToastrService,
  router: Router,
  authService: AuthService
): Observable<never> {
  const errors: IError[] = error.error?.responseData.errors;

  switch (errors[0].errorType) {
    case ErrorType.InternalError:
      toastService.error(
        'Something went wrong',
        'Something went wrong, please try again'
      );
      break;
    case ErrorType.Unauthorized:
      console.log('Unauthorized');
      // if (authService.isAuthenticated()) {
      //   toastService.error('Unauthorized', 'please login first');
      //   router.navigate(['auth', 'login']);
      // }
      // even i already redirect it in auth guard, i still need to redirect here, because maybe the token is expired
      break;
    case ErrorType.Forbidden:
      toastService.error('Forbidden', 'You do not have permission');
      break;
  }

  return throwError(() => error);
}
