import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenActionsService {

  private readonly TOKEN_KEY = 'access_token';

  constructor() { }
  getToken() {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  removeToken() {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }
}
