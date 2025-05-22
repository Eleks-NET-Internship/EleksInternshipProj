import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginPayload = {
    username: '',
    password: ''
  }

  onSignIn() {
    console.log(this.loginPayload);

    // Send login payload to specific API endpoint
  }

  onGoogleSignIn() {
    // Google sign in
  }
}
