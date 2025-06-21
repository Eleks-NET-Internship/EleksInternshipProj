export interface TimetableDto {
  id: number;
  spaceId: number;
  days: TimetableDayDto[];
}

export interface TimetableDayDto {
  id: number;
  dayName: string;
  timetableId: number;
  eventDays: EventDayDto[];
}

export interface EventDayDto {
  id: number;
  eventId: number;
  dayId: number;
  startTime: string; // Format: "HH:mm:ss"
  endTime: string;   // Format: "HH:mm:ss"
  event: EventDto;
}

export interface EventDto {
  id: number;
  name: string;
  spaceId: number;
  markers: EventMarkerDto[];
}

export interface EventMarkerDto {
  id: number;
  eventId: number;
  markerId: number;
  marker: MarkerDto;
}

export interface MarkerDto {
  id: number;
  name: string;
  type: string;
  spaceId: number;
}
