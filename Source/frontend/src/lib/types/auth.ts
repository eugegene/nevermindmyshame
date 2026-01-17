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

export interface RegisterRequest {
	username: string;
	email: string;
	password: string;
	role?: string;
}

export interface RenewTokenRequest {
	refreshToken: string;
}
