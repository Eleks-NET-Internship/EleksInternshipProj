import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiBaseUrl = 'https://localhost:7050';
  private readonly TOKEN_KEY = 'access_token';

  constructor(private readonly http: HttpClient) { }

  getToken() {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  async login(credentials: { email: string; password: string }) : Promise<boolean> {
    try {
      const response = await firstValueFrom(this.http.post<{ accessToken: string }>(this.apiBaseUrl + '/api/auth/login', credentials, { observe: 'response' }));
      if (response.ok) {
        if (response.body?.accessToken !== undefined) {
          this.setToken(response.body.accessToken);
          console.log('Successful login.');
          return true;
        }
        else return false;
      }
      else return false;
    }
    catch (error) {
      console.error('Login failed: ', error);
      return false;
    }
  }

  logout() {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }

  async register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }): Promise<boolean> {
    try {
      const response = await firstValueFrom(this.http.post(this.apiBaseUrl + '/api/auth/register', registerPayload, { observe: 'response' }));
      if (response.ok) {
        const login = await this.login({ email: registerPayload.email, password: registerPayload.password});
        if (login) {
          console.log('Successful register.');
          return true;
        }
        else return false;
      }
      else return false;
    }
    catch (error) {
      console.error('Register failed: ', error);
      return false;
    }
  }

  loginWithGoogle() {
    window.location.href = this.apiBaseUrl + '/api/auth/login/google?returnUrl=https://localhost:4200/home';
  }
}
