# OAuth Login Setup for React

## 📋 What Was Added

I've created a complete OAuth2 + PKCE flow integration for your React frontend to log in with IdentityServer. Here's what you got:

### New Files:
1. **`src/pages/Login.jsx`** - Login page that initiates OAuth flow with PKCE
2. **`src/pages/OAuthCallback.jsx`** - Callback handler that exchanges code for token
3. **Updated `src/router/index.jsx`** - Added `/login` and `/oauth-callback` routes

### Features:
✅ **PKCE Security** - Prevents authorization code interception  
✅ **Nonce Protection** - Prevents token replay attacks  
✅ **Console Logging** - Detailed logs for debugging (open browser DevTools: F12)  
✅ **Token Storage** - Stores access_token, refresh_token, id_token in localStorage  
✅ **Error Handling** - Displays errors and logs them to console  
✅ **Token Claims** - Automatically decodes and logs token claims  

---

## 🚀 How to Use

### Step 1: Start Your React App
```bash
cd c:\Users\ttloc\OneDrive - Netcompany\Desktop\KitPraid\KitPraid.FrontEnd\kitpraid
npm run dev
```
Your app will run at `http://localhost:5173` (or another port if 5173 is in use).

### Step 2: Start IdentityServer API
```bash
dotnet run --project c:\Users\ttloc\OneDrive - Netcompany\Desktop\KitPraid\IdentityServer.Api\IdentityServer.Api.csproj
```
IdentityServer will run at `https://localhost:7070` (or check your launchSettings.json)

### Step 3: Navigate to Login
Go to: `http://localhost:5173/login` (or whatever your React port is)

### Step 4: Click "Login with IdentityServer"
- You'll be redirected to IdentityServer login page
- Enter your credentials
- After successful login, you'll be redirected back to React
- Token will be stored in localStorage

### Step 5: Check Console Logs
Open DevTools (`F12` → Console tab) to see detailed OAuth flow logs:
```
🔓 Initiating OAuth login...
🔐 Generated Security Parameters:
  codeVerifier: ...
  codeChallenge: ...
  nonce: ...
  state: ...
🚀 Redirecting to OAuth provider...
🔐 OAuth Callback Received:
  code: ...
  state: ...
  sessionState: ...
📝 Exchanging authorization code for token...
✅ Token received successfully:
  access_token: ...
  token_type: Bearer
  expires_in: 3600
  scope: openid profile email api1
👤 Token Claims:
  sub: user-id-here
  email: user@example.com
  scope: openid profile email api1
  aud: external-app
  exp: 2025-11-17T...
🎉 Login successful! Redirecting to home...
```

---

## 🔧 Configuration

### What Client is Used?
The login uses the **`external-app`** client (configured in `IdentityServer.Infrastructure/Configuration/Config.cs`)

- **ClientId:** `external-app`
- **Redirect URIs:** 
  - `http://localhost:3000/oauth-callback`
  - `http://localhost:5173/oauth-callback`
  - (plus legacy `/callback` URIs)
- **Allowed Scopes:** `openid`, `profile`, `email`, `api1`, `web`

### Change React Port?
If you run React on a different port (e.g., 8080):
1. Update `OAuthCallback.jsx` line ~57:
   ```jsx
   const callbackUri = `${window.location.origin}/oauth-callback`;
   ```
   (This is already dynamic - it auto-detects your current origin!)

2. Update Config.cs to add your port:
   ```csharp
   "http://localhost:8080/oauth-callback"
   ```

### Token Expiration?
- Access token lifetime: **1 hour** (3600 seconds)
- To refresh before expiry, add:
   ```javascript
   // Before calling API
   const refreshToken = localStorage.getItem('refresh_token');
   if (refreshToken) {
     // Implement refresh token exchange
   }
   ```

---

## 🔴 Troubleshooting

### Problem: "redirect_uri mismatch" error
**Solution:** 
- The redirect URI in OAuth flow must match one in Config.cs
- Currently configured: `http://localhost:3000/oauth-callback`, `http://localhost:5173/oauth-callback`
- Check your actual React port in browser address bar
- Update Config.cs if needed and rebuild

### Problem: "invalid_client" error
**Solution:**
- Verify `ClientId` is `external-app` (check OAuthCallback.jsx line ~33)
- Verify `client_secret` is `external-secret` (check OAuthCallback.jsx line ~34)
- ⚠️ **SECURITY NOTE:** Move client_secret to backend in production!

### Problem: Token exchange fails with "CORS error"
**Solution:**
- This is expected in browser (JavaScript cannot directly call token endpoint with client_secret)
- Token exchange happens from browser (not ideal for security)
- **Better approach:** Use a backend proxy:
  ```javascript
  // Instead of calling IdentityServer directly, call your own API
  const response = await fetch('https://localhost:7070/api/auth/exchange-token', {
    method: 'POST',
    body: JSON.stringify({ code, code_verifier }),
  });
  ```

### Problem: Redirect infinite loop
**Solution:**
- Check that OAuth callback page has `navigate('/')` to redirect home
- Make sure home page doesn't auto-redirect to login

### Problem: "No token received" or console shows error
**Solution:**
- Check browser DevTools → Network tab → Find POST to `/connect/token`
- Check the response for error details
- Verify IdentityServer is running on correct port (check Program.cs)
- Check IdentityServer console for error logs

---

## 📝 API Usage After Login

Once logged in, access token is stored. Use it to call your API:

```javascript
// In any React component
const token = localStorage.getItem('access_token');

const response = await fetch('https://localhost:7070/api/auth/user/935cf5a9-042c-4b66-92d9-32ebbbbffcd9', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});

const data = await response.json();
console.log('API Response:', data);
```

---

## 🔒 Security Notes

### ✅ What's Secure:
- PKCE prevents authorization code interception (browser-based apps)
- Nonce prevents token replay attacks
- State parameter prevents CSRF attacks
- RequirePkce = true enforces S256 challenge method

### ⚠️ What Needs Improvement (for production):
1. **Move client_secret to backend**
   - Currently exposed in browser
   - Backend should exchange code for token
   - Frontend receives access_token from backend
   
2. **Use secure token storage**
   - Currently uses localStorage (accessible to XSS)
   - Better: httpOnly cookies or encrypted storage

3. **Implement token refresh**
   - Current implementation doesn't auto-refresh
   - Add refresh token handler before expiry

4. **HTTPS in production**
   - Ensure your React app and API run on HTTPS

---

## 🎯 Next Steps

1. **Test the flow:**
   - Run React app and IdentityServer
   - Click login, check console logs
   - Verify token is stored in localStorage

2. **Create an authenticated API service:**
   ```javascript
   // src/services/apiClient.js
   export const apiCall = async (url, options = {}) => {
     const token = localStorage.getItem('access_token');
     const headers = {
       ...options.headers,
       'Authorization': `Bearer ${token}`
     };
     return fetch(url, { ...options, headers });
   };
   ```

3. **Add logout functionality:**
   ```javascript
   const logout = () => {
     localStorage.removeItem('access_token');
     localStorage.removeItem('refresh_token');
     localStorage.removeItem('id_token');
     window.location.href = 'https://localhost:7070/connect/endsession';
   };
   ```

---

## 📞 Need Help?

Check console logs (F12) - they contain detailed information about each step of the OAuth flow. If something fails, the console will show exactly where and why.

Happy coding! 🚀
