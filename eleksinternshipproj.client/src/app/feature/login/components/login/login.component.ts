import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private readonly authService: AuthService, private router: Router) { }

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
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.log(error.error.message);
        // ui update?
      }
    });
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
