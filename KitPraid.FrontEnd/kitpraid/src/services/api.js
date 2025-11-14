import axios from 'axios';

// Multiple API Base URLs - can be set via environment variables
const API_BASE_URLS = {
  default: import.meta.env.VITE_API_BASE_URL || 'http://localhost:3000/api',
  auth: import.meta.env.VITE_API_AUTHORIZATION_URL,
  payment: import.meta.env.VITE_PAYMENT_API_URL,
  storage: import.meta.env.VITE_STORAGE_API_URL,
  // Add more base URLs as needed
};

// Create default axios instance
const createApiClient = (baseURL) => {
  const client = axios.create({
    baseURL,
    headers: {
      'Content-Type': 'x-www-form-urlencoded',
    },
  });

  // Request interceptor - Add auth token
  client.interceptors.request.use(
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
  client.interceptors.response.use(
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

  return client;
};

// Default API client (uses default base URL)
export const api = createApiClient(API_BASE_URLS.default);

// Create API client with custom base URL
// Accepts either a key (string) or a full URL
export const createApi = (baseURLKeyOrUrl = 'default') => {
  // If it's a full URL (starts with http:// or https://), use it directly
  if (typeof baseURLKeyOrUrl === 'string' && (baseURLKeyOrUrl.startsWith('http://') || baseURLKeyOrUrl.startsWith('https://'))) {
    return createApiClient(baseURLKeyOrUrl);
  }
  // Otherwise, treat it as a key and look up in API_BASE_URLS
  const baseURL = API_BASE_URLS[baseURLKeyOrUrl] || API_BASE_URLS.default;
  return createApiClient(baseURL);
};

// Export base URLs for reference
export const apiBaseURLs = API_BASE_URLS;

// Helper function to get API client for a specific service
export const getApiClient = (serviceName = 'default') => {
  // Map service names to base URL keys
  const serviceMap = {
    auth: 'auth',
    payment: 'payment',
    storage: 'storage',
    // Add more mappings as needed
  };

  const baseURLKey = serviceMap[serviceName] || 'default';
  return createApi(baseURLKey);
};

export default api;

