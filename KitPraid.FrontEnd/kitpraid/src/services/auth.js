import { getApiClient, createApi } from './api';
import { generatePKCEPair } from '../utils/pkce';

// IdentityServer configuration
const IDENTITY_SERVER_URL = import.meta.env.VITE_IDENTITY_SERVER_URL || 'https://localhost:7070';
const CLIENT_ID = 'react-client'; // Use react-client instead of external-app
const REDIRECT_URI = `${window.location.origin}/callback`;

// Use auth-specific API client (can use different base URL)
const api = getApiClient('auth');

/**
 * Generate random string for state parameter
 * @param {number} length - Length of the string
 * @returns {string} Random string
 */
function generateRandomString(length) {
  const charset = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let randomString = '';
  const randomValues = new Uint8Array(length);
  crypto.getRandomValues(randomValues);
  
  for (let i = 0; i < length; i++) {
    randomString += charset[randomValues[i] % charset.length];
  }
  
  return randomString;
}

/**
 * Authentication Service
 * Handles all authentication-related API calls
 * Uses auth-specific base URL (can be different from default)
 */
export const authService = {
  /**
   * Redirect to IdentityServer login page (Razor UI)
   * This will redirect the browser to IdentityServer's authorization endpoint
   */
  redirectToLogin: async () => {
    // Generate PKCE pair
    const { codeVerifier, codeChallenge } = await generatePKCEPair();
    
    // Store code verifier in sessionStorage for later use
    sessionStorage.setItem('pkce_code_verifier', codeVerifier);
    sessionStorage.setItem('pkce_state', generateRandomString(32));
    
    // Build authorization URL
    const params = new URLSearchParams({
      client_id: CLIENT_ID,
      redirect_uri: REDIRECT_URI,
      response_type: 'code',
      scope: 'openid profile email roles api1 web',
      code_challenge: codeChallenge,
      code_challenge_method: 'S256',
      state: sessionStorage.getItem('pkce_state'),
    });
    
    const authUrl = `${IDENTITY_SERVER_URL}/connect/authorize?${params.toString()}`;
    
    // Redirect browser to IdentityServer
    window.location.href = authUrl;
  },

  /**
   * Exchange authorization code for tokens
   * @param {string} code - Authorization code from callback
   * @param {string} state - State parameter for CSRF protection
   * @returns {Promise} Token response
   */
  exchangeCodeForTokens: async (code, state) => {
    // Verify state
    const storedState = sessionStorage.getItem('pkce_state');
    if (state !== storedState) {
      throw new Error('Invalid state parameter');
    }
    
    // Get code verifier
    const codeVerifier = sessionStorage.getItem('pkce_code_verifier');
    if (!codeVerifier) {
      throw new Error('Code verifier not found');
    }
    
    // Exchange code for tokens
    const body = new URLSearchParams({
      grant_type: 'authorization_code',
      client_id: CLIENT_ID,
      code: code,
      redirect_uri: REDIRECT_URI,
      code_verifier: codeVerifier,
    });
    
    try {
      // Use IdentityServer URL directly for token endpoint
      const tokenApi = createApi(IDENTITY_SERVER_URL);
      const response = await tokenApi.post('/connect/token', body.toString(), {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
      });
      
      // Store tokens
      if (response.data.access_token) {
        localStorage.setItem('authToken', response.data.access_token);
      }
      if (response.data.refresh_token) {
        localStorage.setItem('refreshToken', response.data.refresh_token);
      }
      
      // Clean up PKCE data
      sessionStorage.removeItem('pkce_code_verifier');
      sessionStorage.removeItem('pkce_state');
      
      return response.data;
    } catch (error) {
      // Clean up on error
      sessionStorage.removeItem('pkce_code_verifier');
      sessionStorage.removeItem('pkce_state');
      throw error;
    }
  },

  /**
   * Login user (Legacy - Resource Owner Password flow)
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

