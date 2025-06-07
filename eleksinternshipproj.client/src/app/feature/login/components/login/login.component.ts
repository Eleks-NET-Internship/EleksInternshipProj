import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private readonly authService: AuthService, private readonly snackBar: MatSnackBar, private readonly router: Router) {}

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  loginPayload = {
    email: '',
    password: ''
  };

  async onSignIn() {
    const login = await this.authService.login(this.loginPayload);
    if (login) {
      this.router.navigate(['/home']);
    }
    else {
      this.snackBar.open('Невірна електронна пошта або пароль', 'Закрити', {
        duration: 5000,
        panelClass: ['snackbar-error']
      });
    }
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
