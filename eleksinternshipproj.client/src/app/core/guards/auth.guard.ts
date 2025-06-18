import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
import { inject } from '@angular/core';
import { NotificationsSignalrService } from '../services/notifications/notifications-signalr.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const notificationSignalRService = inject(NotificationsSignalrService);

  const token = authService.getToken();

  if (token) {
    notificationSignalRService.startConnection();
    return true;
  }

  return router.createUrlTree(['/login']);
};
