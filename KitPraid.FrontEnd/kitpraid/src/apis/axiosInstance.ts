import axios from "axios";
import { getFromLocalStorage } from "../utils/localStorage";
import type { IAuthState } from "../types/auth";

const api = axios.create({
    baseURL: process.env.PROCESS_API_URL_PATH,
});
api.interceptors.request.use(async (config) => {
    const auth = getFromLocalStorage("auth") as IAuthState;
    if (auth) {
        config.headers.Authorization = `Bearer ${auth.accessToken}`;
    }
    return config;
});

export default api;
