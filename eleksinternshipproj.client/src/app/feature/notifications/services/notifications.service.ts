import { Injectable } from '@angular/core';
import { NotificationDTO, NotificationsResponse, SpaceNotificationDTO } from '../models/notifications-models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private readonly http: HttpClient) { }

  getNotifications(): Observable<NotificationDTO[]> {
    return this.http.get<NotificationDTO[]>(`${this.apiBaseUrl}/api/notifications`);
  }

  notifySpace(dto: SpaceNotificationDTO): Observable<any> {
    return this.http.post(`${this.apiBaseUrl}/api/spaces/${dto.spaceId}/notifications`, dto);
  }
}
