import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from '../auth/auth.service';
import { TokenActionsService } from '../tokens/token-actions.service';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsSignalrService {
  private hubConnection!: signalR.HubConnection;

  private notificationReceivedSource = new Subject<void>();
  notificationReceived$ = this.notificationReceivedSource.asObservable();

  private readonly apiBaseUrl = 'https://localhost:7050';

  constructor(private tokenActionsService: TokenActionsService) { }

  startConnection(): void {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      console.log('SignalR already connected.');
      return;
    }

    const options: signalR.IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return this.tokenActionsService.getToken() ?? ""
      }
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiBaseUrl}/hubs/notifications`,
        options
      )
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log("SignalR connected!!!");
        this.hubConnection.invoke("JoinSpaces")
          .catch((err => console.error("Error joining spaces", err)));
      })
      .catch((err => console.error("Error starting up signalR", err)));


    this.hubConnection.on("ReceiveReminderNotification", (data) => {
      this.notificationReceivedSource.next();

      if (Notification.permission === "granted") {
        this.createReminderNotification(data);
      } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(permission => {
          if (permission === "granted") {
            this.createReminderNotification(data);
          }
        })
      }
    });

    this.hubConnection.on("ReceiveSpaceNotification", (data) => {
      this.notificationReceivedSource.next();

      if (Notification.permission === "granted") {
        this.createSpaceNotification(data);
      } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(permission => {
          if (permission === "granted") {
            this.createSpaceNotification(data);
          }
        })
      }
    });
    

  }

  createReminderNotification(data: any): Notification {
    const deadlineDate = new Date(data.deadlineAt);
    return new Notification(data.title, {
      body: `${data.message} Коли? ${deadlineDate.toLocaleString()}`
    });
  }

  createSpaceNotification(data: any): Notification {
    return new Notification(data.title, {
      body: `${data.message}`
    });
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
