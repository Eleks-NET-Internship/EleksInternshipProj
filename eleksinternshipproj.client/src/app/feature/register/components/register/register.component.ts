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

  onRegister() {
    this.authService.register(this.registerPayload).subscribe({
      next: (response) => {
        this.router.navigate(['/login']);
        // add email verification when the backend implements it
        // <open modal for code input>
      },
      error: (error) => {
        console.log(error.error.message);
        this.snackBar.open('Акаунт з такою електронною поштою вже існує', 'Закрити', {
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
