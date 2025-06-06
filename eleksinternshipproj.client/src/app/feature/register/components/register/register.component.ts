import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private readonly authService: AuthService) {}

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
    this.authService.register(this.registerPayload);
    // redirect to home
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
