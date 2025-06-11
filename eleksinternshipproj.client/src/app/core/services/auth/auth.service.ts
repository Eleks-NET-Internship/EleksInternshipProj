import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

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

  login(credentials: { email: string; password: string }): Observable<{ accessToken: string }> {
    return this.http.post<{ accessToken: string }>(`${this.apiBaseUrl}/api/auth/login`, credentials);
  }

  logout() {
    sessionStorage.removeItem(this.TOKEN_KEY);
    this.router.navigate(['/login']);
  }

  register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }) {
    return this.http.post(`${this.apiBaseUrl}/api/auth/register`, registerPayload);
  }

  loginWithGoogle() {
    window.location.href = `${this.apiBaseUrl}/api/auth/login/google?returnUrl=/spaces`;
  }
}
