import { Component } from '@angular/core';
import { NotificationsService } from '../../services/notifications.service';
import { NotificationDTO, NotificationsResponse } from '../../models/notifications-models';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent {
  notifications: NotificationDTO[] = []; 

  constructor(private notificationsService: NotificationsService) { }

  ngOnInit(): void {
    this.getNotifications();
  }

  getNotifications(): void {
    this.notificationsService.getNotifications().subscribe({
      next: (response: NotificationsResponse) => {
        this.notifications = response.data;
        console.log(this.notifications);
      },
      error: (error) => {
        console.error(error.error.message);
      }
    });
  }

}
