import axios from 'axios';

// API Base URL - can be set via environment variables
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:3000/api';

// Create axios instance
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor - Add auth token
apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('authToken') || sessionStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor - Handle errors
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Handle unauthorized - clear token, redirect to login
      localStorage.removeItem('authToken');
      sessionStorage.removeItem('authToken');
    }
    return Promise.reject(error);
  }
);

// Export axios instance as api
export const api = apiClient;

/**
 * API Endpoints - Centralized endpoint definitions
 */
export const endpoints = {
  auth: {
    login: '/auth/login',
    register: '/auth/register',
    logout: '/auth/logout',
    refresh: '/auth/refresh',
    me: '/auth/me',
  },
  products: {
    list: '/products',
    detail: (id) => `/products/${id}`,
    search: '/products/search',
    categories: '/products/categories',
  },
  cart: {
    get: '/cart',
    add: '/cart/add',
    update: (id) => `/cart/${id}`,
    remove: (id) => `/cart/${id}`,
    clear: '/cart/clear',
  },
  orders: {
    list: '/orders',
    detail: (id) => `/orders/${id}`,
    create: '/orders',
    cancel: (id) => `/orders/${id}/cancel`,
  },
  user: {
    profile: '/user/profile',
    update: '/user/profile',
    wishlist: '/user/wishlist',
    addresses: '/user/addresses',
  },
  newsletter: {
    subscribe: '/newsletter/subscribe',
    unsubscribe: '/newsletter/unsubscribe',
  },
};

export default api;

