/**
 * API Client with Automatic Token Injection
 * Handles Bearer token authorization and error logging
 */

const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL || "https://localhost:7070";

/**
 * Make an authenticated API call with Bearer token
 * @param {string} endpoint - API endpoint (e.g., '/api/auth/user/123')
 * @param {object} options - Fetch options
 * @returns {Promise}
 */
export const apiCall = async (endpoint, options = {}) => {
  const token = localStorage.getItem("authToken");

  if (!token) {
    console.warn("⚠️ No access token found. User may not be logged in.");
  }

  const headers = {
    "Content-Type": "application/json",
    ...options.headers,
  };

  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }

  const url = `${API_BASE_URL}${endpoint}`;

  console.log("📡 API Call:", {
    method: options.method || "GET",
    endpoint,
    hasToken: !!token,
    timestamp: new Date().toISOString(),
  });

  try {
    const response = await fetch(url, {
      ...options,
      headers,
    });

    // Log response status
    console.log(`📊 API Response [${response.status}]:`, {
      endpoint,
      status: response.status,
      statusText: response.statusText,
      contentType: response.headers.get("content-type"),
    });

    // Handle 401 Unauthorized
    if (response.status === 401) {
      console.error("🔑 Token expired or invalid. Redirecting to login...");
      localStorage.removeItem("authToken");
      localStorage.removeItem("refresh_token");
      localStorage.removeItem("id_token");
      window.location.href = "/login";
      return null;
    }

    // Handle other errors
    if (!response.ok) {
      const errorData = await response.text();
      console.error("❌ API Error:", {
        endpoint,
        status: response.status,
        error: errorData,
      });
      throw new Error(`API Error: ${response.statusText}`);
    }

    const data = await response.json();
    console.log("✅ API Success:", {
      endpoint,
      dataKeys: Object.keys(data),
    });

    return data;
  } catch (error) {
    console.error("🚨 API Call Failed:", {
      endpoint,
      message: error.message,
      stack: error.stack,
      timestamp: new Date().toISOString(),
    });
    throw error;
  }
};

/**
 * GET request
 */
export const apiGet = (endpoint, options = {}) =>
  apiCall(endpoint, { ...options, method: "GET" });

/**
 * POST request
 */
export const apiPost = (endpoint, body, options = {}) =>
  apiCall(endpoint, {
    ...options,
    method: "POST",
    body: JSON.stringify(body),
  });

/**
 * PUT request
 */
export const apiPut = (endpoint, body, options = {}) =>
  apiCall(endpoint, {
    ...options,
    method: "PUT",
    body: JSON.stringify(body),
  });

/**
 * DELETE request
 */
export const apiDelete = (endpoint, options = {}) =>
  apiCall(endpoint, { ...options, method: "DELETE" });

/**
 * PATCH request
 */
export const apiPatch = (endpoint, body, options = {}) =>
  apiCall(endpoint, {
    ...options,
    method: "PATCH",
    body: JSON.stringify(body),
  });

/**
 * Check if user is authenticated
 */
export const isAuthenticated = () => {
  const token = localStorage.getItem("authToken");
  if (!token) return false;

  try {
    // Decode token and check expiry
    const parts = token.split(".");
    if (parts.length !== 3) return false;

    const payload = JSON.parse(atob(parts[1]));
    const expiryTime = payload.exp * 1000; // Convert to milliseconds
    return Date.now() < expiryTime;
  } catch (e) {
    console.error("Failed to validate token:", e.message);
    return false;
  }
};

/**
 * Get token expiry time
 */
export const getTokenExpiry = () => {
  const token = localStorage.getItem("authToken");
  if (!token) return null;

  try {
    const parts = token.split(".");
    const payload = JSON.parse(atob(parts[1]));
    return new Date(payload.exp * 1000);
  } catch (e) {
    console.error("Failed to parse token expiry:", e.message);
    return null;
  }
};

/**
 * Get current user from id_token
 */
export const getCurrentUser = () => {
  const idToken = localStorage.getItem("id_token");
  if (!idToken) return null;

  try {
    const parts = idToken.split(".");
    const payload = JSON.parse(atob(parts[1]));
    return {
      sub: payload.sub,
      email: payload.email,
      name: payload.name,
      family_name: payload.family_name,
      given_name: payload.given_name,
    };
  } catch (e) {
    console.error("Failed to parse id_token:", e.message);
    return null;
  }
};

/**
 * Logout function
 */
export const logout = () => {
  console.log("🚪 Logging out...");
  localStorage.removeItem("authToken");
  localStorage.removeItem("refresh_token");
  localStorage.removeItem("id_token");

  // Optional: Redirect to IdentityServer logout endpoint
  // const logoutUrl = `https://localhost:7070/connect/endsession?id_token_hint=${localStorage.getItem("id_token")}&post_logout_redirect_uri=${window.location.origin}/`;

  console.log("✅ Tokens cleared. Redirecting to login...");
  window.location.href = "/login";
};

export default {
  apiCall,
  apiGet,
  apiPost,
  apiPut,
  apiDelete,
  apiPatch,
  isAuthenticated,
  getTokenExpiry,
  getCurrentUser,
  logout,
};
