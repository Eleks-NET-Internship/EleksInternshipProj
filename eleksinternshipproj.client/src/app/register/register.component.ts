import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerPayload = {
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: ''
  };

  onRegister() {
    console.log(this.registerPayload);

    // Send register payload to specific API endpoint
  }
}
