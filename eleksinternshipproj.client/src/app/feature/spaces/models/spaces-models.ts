export interface SpaceDto {
  id: number;
  name: string;
  userSpaces?: UserSpaceDto[];
  timetable?: TimetableDtoShort;
}

export interface SpaceDtoShort {
  id: number;
  name: string;
  userSpaces?: UserSpaceDtoShort[];
  timetable?: TimetableDtoShort;
}

export interface UserSpaceDto {
  id: number;
  userId: number;
  spaceId: number;
  roleId: number;
  user: UserDto;
}

export interface UserSpaceDtoShort {
  id: number;
  userId: number;
  spaceId: number;
  roleId: number;
}

export interface UserDto {
  username: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface TimetableDtoShort {
  id: number;
  spaceId: number;
}

export interface SpaceRenameDto {
  id: number;
  name: string;
}
