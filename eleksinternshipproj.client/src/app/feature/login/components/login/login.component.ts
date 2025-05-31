import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private readonly authService: AuthService) {}

  loginPayload = {
    email: '',
    password: ''
  };

  onSignIn() {
    this.authService.login(this.loginPayload)
  }

  onGoogleSignIn() {
    // Google sign in
  }
}
