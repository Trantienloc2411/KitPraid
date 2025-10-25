import type { IUser } from "./user";

export interface IAuthState {
    loading: boolean;
    error: string | null;
    accessToken: string | null;
    refreshToken: string | null;
    user: IUser | null;
    isAuthenticated: boolean;
}

