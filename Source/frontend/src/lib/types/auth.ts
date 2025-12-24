export interface UserDto {
  email: string;
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RenewTokenRequest {
  refreshToken: string;
}
