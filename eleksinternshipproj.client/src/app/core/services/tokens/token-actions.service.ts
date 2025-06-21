import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenActionsService {

  private readonly TOKEN_KEY = 'access_token';
  private token: string | null;

  constructor() {
    this.token = sessionStorage.getItem(this.TOKEN_KEY);
  }

  getToken() {
    return this.token;
  }

  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
    this.token = token;
  }

  removeToken() {
    sessionStorage.removeItem(this.TOKEN_KEY);
    this.token = null;
  }
}
