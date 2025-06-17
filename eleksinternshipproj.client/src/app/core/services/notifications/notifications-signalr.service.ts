import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationsSignalrService {
  private hubConnection!: signalR.HubConnection;

  private readonly apiBaseUrl = 'https://localhost:7050';

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiBaseUrl}/hubs/notifications`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log("SignalR connected!!!"))
      .catch((err => console.error("Error starting up signalR", err)));

    this.hubConnection.on("ReceiveNotification", (data) => {
      console.log("New notification: ", data);
    })
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
