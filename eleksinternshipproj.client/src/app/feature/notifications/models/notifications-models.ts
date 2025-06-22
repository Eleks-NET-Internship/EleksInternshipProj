export interface NotificationsResponse {
  data: NotificationDTO[];
}

export interface NotificationDTO {
  spaceId: number;
  notificationType: number;
  relatedType: string | null;
  relatedId: number | null;
  title: string;
  message: string;
  sentAt: string;
  deadlineAt: string | null;
  sentBefore: number | null;
}
