export interface SpaceDto {
  id: number;
  name: string;
  userSpaces?: UserSpaceDto[];
}

export interface UserSpaceDto {
  id: number;
  userId: number;
  spaceId: number;
  roleId: number;
}
