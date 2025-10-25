import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import type { IAuthState } from "../types/auth";
import type { IUserLogin } from "../types/user";

const initialState: IAuthState = {
    loading: false,
    error: "",
    accessToken: null,
    refreshToken: null,
    user: null,
    isAuthenticated: false,
};

export const login = createAsyncThunk(
    "auth/login",
    async (credentials: IUserLogin) => {
        const params = new URLSearchParams();
        params.append("grant_type", "password");
        params.append("client_id", "postman-client");
        params.append("client_secret", "secret");
        params.append("username", credentials.email);
        params.append("password", credentials.password);
        params.append("scope", "openid profile email roles");
        // Use a fallback URL if environment variable is not set
        const authUrl = import.meta.env.VITE_AUTHORIZATION_URL_PATH || "http://localhost:8080/oauth2/token";
        
        try {
            const response = await axios.post(authUrl, params, {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                },
            });
            return response.data;
        }
        catch (error : any) {
            console.error("Login error:", error);
            if (error.response?.data?.error) {
                throw new Error(error.response.data.error);
            }
            throw new Error("Login failed. Please try again.");
        }
    }
);

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        logout: (state) => {
            state.accessToken = null;
            state.refreshToken = null;
            state.user = null;
            state.isAuthenticated = false;
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("user");
            localStorage.removeItem("isAuthenticated");
        },
        loadAuthFromLocalStorage: (state) => {
            const saved = localStorage.getItem("auth");
            if (saved) {
                const auth = JSON.parse(saved) as IAuthState;
                state.accessToken = auth.accessToken;
                state.refreshToken = auth.refreshToken;
                state.user = auth.user;
                state.isAuthenticated = auth.isAuthenticated;
            }
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(login.pending, (state) => {
                state.loading = true;
                state.error = "";
            })
            .addCase(login.fulfilled, (state, action) => {
                state.loading = false;
                state.error = "";
                state.accessToken = action.payload.access_token;
                state.refreshToken = action.payload.refresh_token;
                state.user = action.payload.user;
                state.isAuthenticated = true;
                localStorage.setItem("auth", JSON.stringify(state));
            })
            .addCase(login.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || "Login failed";
            });
    }
})
export const { logout, loadAuthFromLocalStorage } = authSlice.actions;
export default authSlice.reducer;

