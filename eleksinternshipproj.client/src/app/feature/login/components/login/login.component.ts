import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { NotificationsSignalrService } from '../../../../core/services/notifications/notifications-signalr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router,
    private readonly notifSignalRService: NotificationsSignalrService) { }

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  loginPayload = {
    email: '',
    password: ''
  };

  onSignIn() {
    this.authService.login(this.loginPayload).subscribe({
      next: (response) => {
        this.authService.setToken(response.accessToken);
        this.notifSignalRService.startConnection();
        this.router.navigate(['/spaces']);
      },
      error: (error) => {
        console.log(error.error.message);
        this.snackBar.open('Неправильна електронна пошта або пароль', 'Закрити', {
          duration: 5000,
          panelClass: ['snackbar-error']
        });
      }
    });
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
