import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'access_token';

  constructor(private readonly http: HttpClient) { }

  getToken() {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  login(credentials: { email: string; password: string }) {
    this.http.post<{ accessToken: string }>('https://localhost:7050/api/auth/login', credentials)
      .subscribe(response => {
        this.setToken(response.accessToken);
      });
  }

  logout() {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }

  register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }) {
    this.http.post('https://localhost:7050/api/auth/register', registerPayload, { observe: 'response' })
      .subscribe(response => {
        if (response.ok) {
          console.log("Successful register.");
          this.login({ email: registerPayload.email, password: registerPayload.password });
        }
      });
  }

  loginWithGoogle() {
    window.location.href = 'https://localhost:7050/api/auth/login/google?returnUrl=https://localhost:4200/home';
  }
}
