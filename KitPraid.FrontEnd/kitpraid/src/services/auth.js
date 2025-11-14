import { getApiClient } from './api';

// Use auth-specific API client (can use different base URL)
const api = getApiClient('auth');

/**
 * Authentication Service
 * Handles all authentication-related API calls
 * Uses auth-specific base URL (can be different from default)
 */
export const authService = {
  /**
   * Login user
   * @param {Object} credentials - { email, password, grant_type, client_id, client_secret, scope }
   * @returns {Promise}
   */
  login: async (credentials) => {
    const body = new URLSearchParams({
      client_id: credentials.client_id,
      client_secret: credentials.client_secret,
      grant_type: credentials.grant_type,
      scope: credentials.scope,
      username: credentials.username,
      password: credentials.password,
    });
    const response = await api.post('/connect/token',body.toString(), {
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
    });
    return response.data;
  },

  /**
   * Register new user
   * @param {Object} userData - { name, email, password, ... }
   * @returns {Promise}
   */
  register: async (userData) => {
    const response = await api.post('/auth/register', userData);
    return response.data;
  },

  /**
   * Logout user
   * @returns {Promise}
   */
  logout: async () => {
    const response = await api.post('/auth/logout');
    return response.data;
  },

  /**
   * Refresh access token
   * @returns {Promise}
   */
  refreshToken: async () => {
    const response = await api.post('/auth/refresh');
    return response.data;
  },

  /**
   * Get current user info
   * @returns {Promise}
   */
  getMe: async () => {
    const response = await api.get('/auth/me');
    return response.data;
  },
};

export default authService;

