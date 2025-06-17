export interface NotificationsResponse {
  data: NotificationDTO[];
}

export interface NotificationDTO {
  title: string;
  message: string;
  relatedType: string;
  relatedId: string;
  sentAt: string;
}
