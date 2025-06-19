import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
import { inject } from '@angular/core';
import { NotificationsSignalrService } from '../services/notifications/notifications-signalr.service';
import { TokenActionsService } from '../services/tokens/token-actions.service';

export const authGuard: CanActivateFn = (route, state) => {
  const tokenActionsService = inject(TokenActionsService);
  const router = inject(Router);
  const notificationSignalRService = inject(NotificationsSignalrService);

  const token = tokenActionsService.getToken();

  if (token) {
    notificationSignalRService.startConnection();
    return true;
  }

  return router.createUrlTree(['/login']);
};
