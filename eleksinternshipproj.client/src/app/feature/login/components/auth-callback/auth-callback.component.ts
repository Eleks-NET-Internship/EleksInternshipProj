import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { NotificationsSignalrService } from '../../../../core/services/notifications/notifications-signalr.service';
import { TokenActionsService } from '../../../../core/services/tokens/token-actions.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrl: './auth-callback.component.css'
})
export class AuthCallbackComponent {
  constructor(
    private route: ActivatedRoute,
    private tokenActionsService: TokenActionsService,
    private router: Router,
    private readonly notifSignalRService: NotificationsSignalrService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: (params) => {
        const token = params['accessToken'];
        const returnUrl = params['returnUrl'] || '/spaces';

        if (token) {
          this.tokenActionsService.setToken(token);
          this.notifSignalRService.startConnection();
          this.router.navigateByUrl(returnUrl);
        } else {
          this.router.navigate(['/login']);
        }
      }
    })
  }
}
