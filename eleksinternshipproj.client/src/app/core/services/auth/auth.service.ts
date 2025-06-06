import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiBaseUrl = 'https://localhost:7050';
  private readonly TOKEN_KEY = 'access_token';

  constructor(private readonly http: HttpClient, private router: Router) { }

  getToken() {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  login(credentials: { email: string; password: string }) {
    this.http.post<{ accessToken: string }>(this.apiBaseUrl + '/api/auth/login', credentials)
      .subscribe(response => {
        this.setToken(response.accessToken);
      });
    this.router.navigate(['/home']);
  }

  logout() {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }

  register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }) {
    this.http.post(this.apiBaseUrl + '/api/auth/register', registerPayload, { observe: 'response' })
      .subscribe(response => {
        if (response.ok) {
          console.log("Successful register.");
          this.login({ email: registerPayload.email, password: registerPayload.password });
        }
      });
  }

  loginWithGoogle() {
    window.location.href = this.apiBaseUrl + '/api/auth/login/google?returnUrl=https://localhost:4200/home';
  }
}
