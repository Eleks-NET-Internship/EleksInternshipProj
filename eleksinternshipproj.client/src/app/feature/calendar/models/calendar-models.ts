export interface TaskDTO {
  id: number,
  eventId: number,
  statusId: number,
  statusName: string,
  name: string,
  eventTime: string,
  isDeadline: boolean,
  description: string
}

export interface UniqueEventDTO {
  id: number,
  eventId: number,
  eventName: string,
  eventTime: string,
  spaceId: number,
  markers: MarkerDto[]
}

export interface MarkerDto {
  id: number,
  name: string,
  type: string,
  spaceId: number
}

export interface AddUniqueEventDto {
  id: number,
  eventName: string,
  eventTime: string,
  spaceId: number
}