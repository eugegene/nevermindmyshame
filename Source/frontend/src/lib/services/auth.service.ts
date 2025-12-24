import { api } from "$lib/api";
import type { LoginRequest, RenewTokenRequest, UserDto } from "$lib/types/auth";

export const AuthService = {
  login: async (creds: LoginRequest, customFetch?: typeof fetch) => {
    return await api.post<UserDto>("auth/login", creds, undefined, customFetch);
  },

  renewToken: async (refreshToken: string, customFetch?: typeof fetch) => {
    const payload: RenewTokenRequest = { refreshToken };
    return await api.post<UserDto>(
      "auth/renewToken",
      payload,
      undefined,
      customFetch
    );
  },

  testAdmin: async (token: string, customFetch?: typeof fetch) => {
    return await api.get<string>("auth/test", token, customFetch);
  },
};
