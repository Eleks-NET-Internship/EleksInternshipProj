import { Component } from '@angular/core';
import { NotificationsService } from '../../services/notifications.service';
import { NotificationDTO, NotificationsResponse, SpaceNotificationDTO } from '../../models/notifications-models';
import { MatDialog } from '@angular/material/dialog';
import { NotifySpaceComponent } from '../notify-space/notify-space.component';
import { SpacesService } from '../../../spaces/services/spaces.service';
import { SpaceRenameDto } from '../../../spaces/models/spaces-models';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent {
  notifications: NotificationDTO[] = []; 
  canNotifySpaces: boolean = false;
  constructor(private notificationsService: NotificationsService, private dialog: MatDialog, private spacesService: SpacesService) { }

  ngOnInit(): void {
    this.getNotifications();
    this.spacesService.getSpacesWhereAdmin().subscribe({
      next: (result: SpaceRenameDto[]) => {
        if (result.length > 0) {
          this.canNotifySpaces = true;
        }
      },
      error: (error) => {
        console.log(error.error.message)
      }
    });
  }

  getNotifications(): void {
    this.notificationsService.getNotifications().subscribe({
      next: (response: NotificationDTO[]) => {
        this.notifications = response;
      },
      error: (error) => {
        console.error(error.error.message);
      }
    });
  }
  openNotifySpaceDialog(): void {
    const dialogRef = this.dialog.open(NotifySpaceComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe((newNotification: SpaceNotificationDTO) => {
      if (newNotification) {
        this.notificationsService.notifySpace(newNotification).subscribe({
          next: () => this.getNotifications(),
          error: err => console.error('Помилка при створенні завдання:', err)
        });
      }
    });
  }

}
