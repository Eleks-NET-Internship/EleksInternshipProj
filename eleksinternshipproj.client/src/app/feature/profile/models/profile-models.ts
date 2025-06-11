export interface ProfileDto {
  username: string

  firstName?: string

  lastName?: string

  email: string
}

export interface UpdateProfileDto {
  username: string

  firstName?: string

  lastName?: string
}

export interface ProfileResponse {
  data: ProfileDto;
}
