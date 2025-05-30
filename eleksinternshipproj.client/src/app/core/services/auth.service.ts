import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'access_token';

  constructor(private readonly http: HttpClient) { }

  login(credentials: { email: string; password: string }) {
    this.http.post<{ token: string }>('https://127.0.0.1/api/auth/login', credentials)
      .subscribe(response => {
        sessionStorage.setItem(this.TOKEN_KEY, response.token);
      });
  }

  logout() {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }

  register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }) {
    this.http.post('https://127.0.0.1/api/auth/register', registerPayload);
  }
}
