export interface NotificationsResponse {
  data: NotificationDTO[];
}

export interface NotificationDTO {
  title: string;
  message: string;
  relatedType: string;
  relatedId: number;
  sentAt: string;
  deadlineAt: string;
  spaceId: number;
  read: boolean;
}
