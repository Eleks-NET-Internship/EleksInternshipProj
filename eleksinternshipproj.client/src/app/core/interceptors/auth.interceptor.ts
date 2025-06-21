import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { TokenActionsService } from '../services/tokens/token-actions.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const tokenActionsService = inject(TokenActionsService);
  const token = tokenActionsService.getToken();

  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError(err => {
      if (err.status === 401) {
        authService.logOut();
      }
      return throwError(() => err);
    })
  );
};
