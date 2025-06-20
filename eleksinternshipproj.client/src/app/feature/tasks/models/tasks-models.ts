export interface TaskDto {
  id: number;
  eventId: number;
  statusId: number;

  // Опційне поле (для POST/PUT)
  statusName?: string;

  name: string;
  eventTime: string;
  isDeadline: boolean;
  description: string;
}

export interface TaskModelStatusDto {
  id: number;
  name: string;
}
