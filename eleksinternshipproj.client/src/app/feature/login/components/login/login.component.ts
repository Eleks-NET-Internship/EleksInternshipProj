import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private readonly authService: AuthService) {}

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
    this.authService.login(this.loginPayload);
    //redirect to home
  }

  onGoogleSignIn() {
    // Google sign in
  }
}
