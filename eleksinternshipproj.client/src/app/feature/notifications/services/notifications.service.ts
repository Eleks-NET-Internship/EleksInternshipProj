import { Injectable } from '@angular/core';
import { NotificationDTO, NotificationsResponse } from '../models/notifications-models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }

  getNotifications(): Observable<NotificationsResponse> {
    return this.http.get<NotificationsResponse>(`${this.apiBaseUrl}/api/notifications`);
  }
}
