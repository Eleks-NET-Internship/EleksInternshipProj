export interface MarkerDto {
  id: number;
  name: string;
  type: string;
  spaceId: number;
}

export interface EventDto {
  id: number;
  name: string;
  spaceId: number;
  markers: MarkerDto[];
}

export interface CreateEventDto {
  name: string;
  spaceId: number;
}

export interface UpdateEventDto {
 id: number;
 name: string;
}
