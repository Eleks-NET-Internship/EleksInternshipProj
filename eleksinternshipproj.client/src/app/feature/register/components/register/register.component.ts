import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private readonly authService: AuthService) {}

  registerPayload = {
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: ''
  };

  onRegister() {
    this.authService.register(this.registerPayload);
  }
}
