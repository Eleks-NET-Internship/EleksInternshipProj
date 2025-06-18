import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationsSignalrService {
  private hubConnection!: signalR.HubConnection;

  private readonly apiBaseUrl = 'https://localhost:7050';

  startConnection(): void {
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      console.log('SignalR already connected.');
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiBaseUrl}/hubs/notifications`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log("SignalR connected!!!"))
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
