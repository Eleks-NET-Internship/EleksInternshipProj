import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { NotificationsSignalrService } from '../notifications/notifications-signalr.service';
import { TokenActionsService } from '../tokens/token-actions.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient, private router: Router, private tokenService: TokenActionsService, private notificationSignalRService: NotificationsSignalrService) { }

  login(credentials: { email: string; password: string }): Observable<{ accessToken: string }> {
    return this.http.post<{ accessToken: string }>(`${this.apiBaseUrl}/api/auth/login`, credentials);
  }

  logOut() {
    this.notificationSignalRService.stopConnection();
    window.sessionStorage.clear();
    window.localStorage.clear();
    this.router.navigate(['/login']);
  }

  register(registerPayload: { firstName: string, lastName: string, username: string, email: string, password: string }) {
    return this.http.post(`${this.apiBaseUrl}/api/auth/register`, registerPayload);
  }

  loginWithGoogle() {
    window.location.href = `${this.apiBaseUrl}/api/auth/login/google?returnUrl=/spaces`;
  }
}
