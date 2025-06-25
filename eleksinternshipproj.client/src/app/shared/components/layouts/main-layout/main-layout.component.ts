import { Component } from '@angular/core';
import { NotificationsSignalrService } from '../../../../core/services/notifications/notifications-signalr.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  constructor(private notificationsSignalRService: NotificationsSignalrService) { }
  ngOnInit() {
    this.notificationsSignalRService.startConnection();
  }
}
