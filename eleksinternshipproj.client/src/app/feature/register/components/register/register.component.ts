import { Component, signal } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private readonly authService: AuthService, private router: Router) { }

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
      }
    });
  }

  onGoogleSignIn() {
    this.authService.loginWithGoogle();
  }
}
