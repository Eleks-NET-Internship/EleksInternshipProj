import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from '../auth/auth.service';
import { TokenActionsService } from '../tokens/token-actions.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationsSignalrService {
  private hubConnection!: signalR.HubConnection;

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


    this.hubConnection.on("ReceiveNotification", (data) => {
      if (Notification.permission === "granted") {
        this.createNotification(data);
      } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(permission => {
          if (permission === "granted") {
            this.createNotification(data);
          }
        })
      }
    })
  }

  createNotification(data: any): Notification {
    const deadlineDate = new Date(data.deadlineAt);
    return new Notification(data.title, {
      body: `${data.message} Коли? ${deadlineDate.toLocaleString()}`
    });
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
