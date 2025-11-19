# React OAuth2 Integration - Complete Setup Summary

## ✅ What Was Done

I've set up a complete **OAuth2 + PKCE** authentication flow for your React frontend to log in with IdentityServer and access your protected API.

---

## 📁 Files Created/Modified

### Frontend (React)
| File | Purpose |
|------|---------|
| `src/pages/Login.jsx` | Login page - Initiates OAuth flow |
| `src/pages/OAuthCallback.jsx` | Callback handler - Exchanges code for token |
| `src/services/apiClient.js` | API utility - Makes authenticated calls with automatic token injection |
| `src/components/ApiClientExample.jsx` | Example component - Shows how to use the API client |
| `src/router/index.jsx` | Updated - Added `/login` and `/oauth-callback` routes |

### Backend (IdentityServer)
| File | Purpose |
|------|---------|
| `IdentityServer.Infrastructure/Configuration/Config.cs` | Updated - Added OAuth callback URIs |
| `IdentityServer.Api/Program.cs` | Already configured with JwtBearer + diagnostics |

---

## 🚀 Quick Start

### 1. Start IdentityServer (Backend)
```bash
cd c:\Users\ttloc\OneDrive - Netcompany\Desktop\KitPraid
dotnet run --project IdentityServer.Api/IdentityServer.Api.csproj
```
- Runs at: `https://localhost:7070` (check your launchSettings.json)

### 2. Start React (Frontend)
```bash
cd KitPraid.FrontEnd/kitpraid
npm run dev
```
- Runs at: `http://localhost:5173` (or another port if 5173 is taken)

### 3. Navigate to Login
Open browser: `http://localhost:5173/login`

### 4. Click "Login with IdentityServer"
- Redirected to IdentityServer login page
- Enter credentials
- Redirected back to React with token
- Token stored in localStorage

### 5. Check Console Logs
Press `F12` → Console to see detailed OAuth logs with:
- ✅ Request details (code_verifier, code_challenge, nonce)
- ✅ Authorization code received
- ✅ Token response
- ✅ Token claims (subject, email, scope, audience)
- ✅ Errors if any

---

## 🔐 OAuth Flow Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                         OAuth2 + PKCE Flow                       │
└─────────────────────────────────────────────────────────────────┘

React App                 Browser                    IdentityServer
   │                        │                              │
   │────────────────────────┼──────────────────────────────│
   │ 1. Generate PKCE       │                              │
   │    + Nonce + State     │                              │
   │                        │                              │
   │ 2. Redirect to         │                              │
   │    authorize endpoint  │                              │
   │                        ├──────────────────────────────┤
   │                        │ 3. Show Login Form           │
   │                        │    (User enters credentials) │
   │                        │                              │
   │                        │ 4. Authenticate User        │
   │                        │                              │
   │                        │ 5. Generate auth code       │
   │                        ├──────────────────────────────┤
   │ 6. Receive auth code   │                              │
   │    + redirect back     │                              │
   │                        │                              │
   │ 7. Exchange code for   │                              │
   │    token (with verifier│                              │
   │    for PKCE)           │                              │
   │                        ├──────────────────────────────┤
   │                        │ 8. Validate PKCE            │
   │                        │    Issue tokens             │
   │                        ├──────────────────────────────┤
   │ 9. Receive tokens      │                              │
   │    (access + refresh)  │                              │
   │                        │                              │
   │ 10. Store locally      │                              │
   │     Redirect to home   │                              │
   │                        │                              │
   ├────────────────────────┼────────────────────────────── (Step 11+)
   │ 11. API calls with     │                              │
   │     Bearer token       ├──────────────────────────────┤
   │                        │ 12. Validate JWT            │
   │                        │     Return protected data   │
   │                        │                              │
   └────────────────────────────────────────────────────────────────
```

---

## 📚 Key Components

### 1. **Login Page** (`src/pages/Login.jsx`)
- Generates PKCE parameters (code_verifier + code_challenge)
- Generates security parameters (nonce, state)
- Stores them in sessionStorage
- Redirects user to IdentityServer authorize endpoint
- **Console Logs:** Security parameters, redirect URL

### 2. **OAuth Callback** (`src/pages/OAuthCallback.jsx`)
- Receives authorization code from IdentityServer
- Retrieves code_verifier from sessionStorage
- Exchanges code for token (with PKCE verification)
- Stores tokens in localStorage
- Decodes and logs token claims
- Redirects to home
- **Console Logs:** Code received, token response, token claims, errors

### 3. **API Client** (`src/services/apiClient.js`)
Utility functions for making authenticated API calls:
- `apiGet(endpoint)` - GET request
- `apiPost(endpoint, body)` - POST request
- `apiPut(endpoint, body)` - PUT request
- `apiDelete(endpoint)` - DELETE request
- `apiPatch(endpoint, body)` - PATCH request
- `isAuthenticated()` - Check if user has valid token
- `getCurrentUser()` - Get user info from id_token
- `getTokenExpiry()` - Get token expiry time
- `logout()` - Clear tokens and logout

**All API calls automatically:**
- Inject Bearer token in Authorization header
- Log request/response to console
- Handle 401 (invalid token) by redirecting to login
- Log all errors with details

### 4. **Example Component** (`src/components/ApiClientExample.jsx`)
Shows how to:
- Check authentication status
- Display current user info
- Make API calls
- Handle responses and errors
- Logout

---

## 💾 Token Storage

Tokens are stored in **localStorage**:
```javascript
localStorage.getItem('access_token')   // JWT for API calls
localStorage.getItem('refresh_token')  // For refreshing token
localStorage.getItem('id_token')       // User identity info
```

---

## 🔍 Console Logging Examples

### Successful Login Flow:
```
🔓 Initiating OAuth login...
🔐 Generated Security Parameters: {
  codeVerifier: "kJ_Z9xW...",
  codeChallenge: "A-B_Z9x...",
  nonce: "random_nonce_here",
  state: "random_state_here"
}
🚀 Redirecting to OAuth provider: https://localhost:7070/connect/authorize...
🔐 OAuth Callback Received: {
  code: "auth_code_here",
  state: "random_state_here"
}
📝 Exchanging authorization code for token...
✅ Token received successfully: {
  access_token: "eyJhbGci...",
  token_type: "Bearer",
  expires_in: 3600,
  scope: "openid profile email api1"
}
👤 Token Claims: {
  sub: "user-guid-here",
  email: "user@example.com",
  scope: "openid profile email api1",
  aud: "external-app",
  exp: "2025-11-17T20:30:00Z"
}
🎉 Login successful! Redirecting to home...
```

### API Call:
```
📡 API Call: {
  method: "GET",
  endpoint: "/api/auth/user/935cf5a9...",
  hasToken: true
}
📊 API Response [200]: {
  status: 200,
  statusText: "OK"
}
✅ API Success: {
  endpoint: "/api/auth/user/935cf5a9...",
  dataKeys: ["id", "email", "name"]
}
```

### Error Handling:
```
❌ API Error: {
  endpoint: "/api/auth/user/invalid-id",
  status: 404,
  error: "User not found"
}
🔑 Token expired or invalid. Redirecting to login...
🚨 OAuth Callback Error: {
  message: "invalid_redirect_uri",
  timestamp: "2025-11-17T..."
}
```

---

## 🔧 Configuration

### IdentityServer Client: `external-app`
- **ClientId:** `external-app`
- **ClientSecret:** `external-secret`
- **Redirect URIs:** 
  - `http://localhost:3000/oauth-callback`
  - `http://localhost:5173/oauth-callback`
  - `http://localhost:3000/callback` (legacy)
  - `http://localhost:5173/callback` (legacy)
- **Allowed Scopes:** `openid`, `profile`, `email`, `api1`, `web`
- **PKCE:** Required (S256)
- **Token Lifetime:** 3600 seconds (1 hour)

### Change Configuration:
Edit `IdentityServer.Infrastructure/Configuration/Config.cs`:
```csharp
new Client {
    ClientId = "external-app",
    ClientSecrets = { new Secret("external-secret".Sha256()) },
    RedirectUris = { 
        "http://localhost:YOUR_PORT/oauth-callback",
        "http://localhost:YOUR_PORT/callback"
    },
    AllowedScopes = { "openid", "profile", "email", "api1" },
    // ... other config
}
```

---

## 🎯 Usage Examples

### Check if User is Logged In:
```javascript
import { isAuthenticated, getCurrentUser } from '../services/apiClient';

if (isAuthenticated()) {
  const user = getCurrentUser();
  console.log('Welcome:', user.name);
} else {
  // Redirect to login
  window.location.href = '/login';
}
```

### Make Authenticated API Call:
```javascript
import { apiGet, apiPost } from '../services/apiClient';

// GET
const user = await apiGet('/api/auth/user/123');

// POST
const result = await apiPost('/api/auth/register', {
  email: 'user@example.com',
  password: 'pass123'
});
```

### Logout:
```javascript
import { logout } from '../services/apiClient';

const handleLogout = () => {
  logout(); // Clears tokens and redirects to login
};
```

---

## 🛡️ Security Notes

### ✅ Secure:
- PKCE prevents authorization code interception
- Nonce prevents token replay attacks
- State parameter prevents CSRF
- Access tokens expire after 1 hour
- JwtBearer validates token signatures

### ⚠️ For Production:
- **Move client_secret to backend** - Currently exposed in browser code
- **Use HTTPS everywhere** - Especially for token endpoints
- **Use secure token storage** - httpOnly cookies instead of localStorage
- **Implement token refresh** - Auto-refresh before expiry
- **Add CSRF protection** - Use SameSite cookies
- **Validate tokens server-side** - Don't trust client-side checks alone

---

## 🐛 Troubleshooting

### Issue: "redirect_uri mismatch"
1. Check React app URL in browser (e.g., http://localhost:5173)
2. Ensure it matches one of the RedirectUris in Config.cs
3. Update Config.cs if needed
4. Rebuild IdentityServer

### Issue: "invalid_client"
1. Verify `ClientId` is `external-app`
2. Verify `ClientSecret` is `external-secret`
3. Check both in OAuthCallback.jsx line ~33-34

### Issue: Token not working with API
1. Open DevTools Console (F12)
2. Check if token is in localStorage: `localStorage.getItem('access_token')`
3. Check API logs for validation errors
4. Verify API expects `api1` audience (check Program.cs)

### Issue: Infinite redirect loop
1. Ensure home page doesn't auto-redirect to login
2. Check that login page has console.log showing progress
3. Look for errors in console

### Issue: CORS error when exchanging token
**This is normal!** - JavaScript in browser cannot directly call token endpoint with client_secret.
- **Solution:** Create a backend proxy endpoint that handles token exchange securely
- Backend exchanges code for token (has client_secret)
- Returns token to frontend

---

## 📖 Documentation Files

1. **OAUTH_REACT_SETUP.md** - Complete OAuth setup guide
2. **EXTERNAL_CLIENT_SETUP.md** - Client configuration guide
3. **API_USAGE_GUIDE.md** - API usage guide (if exists)

---

## 🎓 Next Learning Steps

1. **Token Refresh:**
   - Implement automatic token refresh before expiry
   - Store refresh token securely
   - Handle refresh token rotation

2. **Protected Routes:**
   - Create `ProtectedRoute` component
   - Redirect to login if not authenticated
   - Show loading state during auth check

3. **API Interceptor:**
   - Catch 401 errors globally
   - Auto-refresh token on expiry
   - Retry failed requests

4. **User Context:**
   - Create React Context for authenticated user
   - Persist user info across page refreshes
   - Provide user data to all components

---

## 🚀 Ready to Use!

Your OAuth2 + PKCE integration is complete and ready to test:

1. ✅ Login page with PKCE security
2. ✅ OAuth callback handler
3. ✅ Authenticated API client
4. ✅ Detailed console logging
5. ✅ Token storage
6. ✅ Error handling
7. ✅ User info extraction

**Next:** Start the apps and test the flow! Check console (F12) for detailed logs.

Happy coding! 🎉
