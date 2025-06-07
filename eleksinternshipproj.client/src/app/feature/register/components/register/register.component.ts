import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private readonly authService: AuthService, private readonly snackBar: MatSnackBar, private readonly router: Router) {}

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  registerPayload = {
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: ''
  };

  async onRegister() {
    const isLogged = await this.authService.register(this.registerPayload);
    if (isLogged) {
      this.router.navigate(['/home']);
    }
    else {
      this.snackBar.open('Акаунт з такою електронною поштою вже існує', 'Закрити', {
        duration: 5000,
        panelClass: ['snackbar-error']
      });
    }
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
