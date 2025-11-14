# External Client Access Guide

This guide explains how to configure external applications (web apps, mobile apps, desktop apps) to access your IdentityServer-protected API.

## Quick Summary

- **IdentityServer** (https://localhost:5001) is your Authorization Server — it authenticates users and issues tokens.
- **Your API** (https://localhost:5001/api) validates those tokens via JwtBearer.
- **External clients** get tokens from IdentityServer and include them when calling your API.

---

## Step 1: Register Your Client in IdentityServer

The `external-app` client has already been added to `IdentityServer.Infrastructure/Configuration/Config.cs`. Here's what it does:

```csharp
new Client
{
    ClientId = "external-app",                           // Unique identifier for your client
    ClientSecrets = { new Secret("external-secret".Sha256()) },  // Client password
    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,    // Login + service account
    RedirectUris = { "http://localhost:3000/callback", ... },   // Where browser redirects after login
    AllowOfflineAccess = true,                           // Can get refresh tokens
    AllowedScopes = { "openid", "profile", "email", "roles", "api1" },  // What the client can request
    AllowedCorsOrigins = { "http://localhost:3000", ... },     // CORS origins allowed
    RequirePkce = true,                                  // Enforce security for browser apps
}
```

**Customize this for your needs:**

- Change `RedirectUris` to your app's actual callback URLs (e.g., `https://myapp.com/callback`).
- Update `AllowedCorsOrigins` to your app's domain.
- If you only need server-to-server access, use `AllowedGrantTypes = GrantTypes.ClientCredentials`.

---

## Step 2: Request an Access Token

Depending on your scenario, choose one:

### Option A: Authorization Code (User Login) — Recommended for Web/Mobile

**Flow:**

1. User clicks "Login with IdentityServer"
2. Your app redirects to IdentityServer's authorize endpoint
3. User signs in
4. IdentityServer redirects back to your app with an authorization code
5. Your app exchanges the code for an access token (server-to-server)

**PowerShell Example:**

```powershell
# Step 1: Get authorization code (user does this interactively in browser or OAuth library handles it)
# Browser redirects to:
# https://localhost:5001/connect/authorize?
#   client_id=external-app
#   &redirect_uri=http://localhost:3000/callback
#   &response_type=code
#   &scope=openid profile api1
#   &code_challenge=...
#   &code_challenge_method=S256

# Step 2: After user logs in, browser redirects to:
# http://localhost:3000/callback?code=<authorization_code>

# Step 3: Exchange code for token (server-side)
$authCode = "<authorization_code_from_step2>"
$body = @{
  grant_type    = 'authorization_code'
  client_id     = 'external-app'
  client_secret = 'external-secret'
  code          = $authCode
  redirect_uri  = 'http://localhost:3000/callback'
  code_verifier = '<pkce_code_verifier>'  # Generated in step 1 by your OAuth library
}

$response = Invoke-RestMethod -Uri 'https://localhost:5001/connect/token' -Method Post -Body $body -SkipCertificateCheck

$accessToken = $response.access_token
$refreshToken = $response.refresh_token  # Use this to get new tokens without re-login

Write-Host "Access Token: $accessToken"
```

**Libraries that handle this for you:**

- **JavaScript/Web:** `oidc-client-ts`, `@auth0/auth0-spa-js`, React Router, Next.js auth
- **React Native/Mobile:** `react-native-oauth`, Expo `expo-auth-session`
- **Flutter:** `flutter_appauth`, `openid_client`
- **.NET:** `IdentityModel`

---

### Option B: Client Credentials (Server-to-Server, No User)

Use this if your external service doesn't have a user — it just needs to call the API as a service account.

**Edit `Config.cs` to use Client Credentials only:**

```csharp
AllowedGrantTypes = GrantTypes.ClientCredentials,  // Remove CodeAndClientCredentials
```

**PowerShell to get token:**

```powershell
$body = @{
  grant_type    = 'client_credentials'
  client_id     = 'external-app'
  client_secret = 'external-secret'
  scope         = 'api1'
}

$response = Invoke-RestMethod -Uri 'https://localhost:5001/connect/token' -Method Post -Body $body -SkipCertificateCheck
$accessToken = $response.access_token

Write-Host "Access Token: $accessToken"
```

---

## Step 3: Call Your API with the Token

Once you have an access token, include it in the `Authorization` header:

```powershell
$headers = @{
  Authorization = "Bearer $accessToken"
}

# Call your API
$response = Invoke-RestMethod `
  -Uri 'https://localhost:5001/api/auth/user/935cf5a9-042c-4b66-92d9-32ebbbbffcd9' `
  -Headers $headers `
  -SkipCertificateCheck

$response | ConvertTo-Json
```

**Or in cURL:**

```bash
curl -H "Authorization: Bearer $ACCESS_TOKEN" \
  https://localhost:5001/api/auth/user/935cf5a9-042c-4b66-92d9-32ebbbbffcd9
```

**Or in JavaScript/React:**

```javascript
const response = await fetch('https://localhost:5001/api/auth/user/...' {
  headers: {
    Authorization: `Bearer ${accessToken}`
  }
});
const data = await response.json();
```

---

## Step 4: Handle Token Refresh (Optional)

If your token expires and you requested `AllowOfflineAccess = true`, you got a `refresh_token` in the token response. Use it to get a new access token without re-login:

```powershell
$body = @{
  grant_type    = 'refresh_token'
  client_id     = 'external-app'
  client_secret = 'external-secret'
  refresh_token = $refreshToken
}

$response = Invoke-RestMethod -Uri 'https://localhost:5001/connect/token' -Method Post -Body $body -SkipCertificateCheck
$newAccessToken = $response.access_token
```

---

## Troubleshooting

### Error: `invalid_client` or `Unauthorized client`

- Verify the `ClientId` and `ClientSecret` match what's in `Config.cs`
- Ensure the client is registered (it is: "external-app")

### Error: `invalid_redirect_uri` or `Redirect URI mismatch`

- The `redirect_uri` in your token request must match one of the `RedirectUris` in the client config
- Add your actual redirect URI to `Config.cs` and rebuild

### Error: `invalid_scope` or `Requested scope not available`

- The `scope` you request must be in the client's `AllowedScopes`
- "api1" is allowed — add others as needed

### Error: `CORS error` from browser

- The client's origin must be in `AllowedCorsOrigins`
- Update `Config.cs` and rebuild

### Error: `invalid_grant` on token request

- Verify the authorization code hasn't expired (usually 5 min)
- Verify the code_verifier (PKCE) matches the code_challenge
- Verify the redirect_uri matches

### API returns `401 Unauthorized`

- The token may be expired — request a new one
- The token scope may not include "api1"
- Check the JwtBearer diagnostic logs (run API with `LogLevel: Debug`)

---

## Complete Flow Diagram

```
External App                 IdentityServer                     Your API
     |                            |                               |
     |--- (1) Redirect to Login --|                               |
     |                            |                               |
     |<-- (2) Show Login Form ----|                               |
     |                            |                               |
     |--- (3) User Credentials ---|                               |
     |                            |                               |
     |<-- (4) Auth Code + Redirect|                               |
     |                            |                               |
     |--- (5) Exchange Code for Token (server-to-server) --->|    |
     |                            |                               |
     |<-- (6) Access Token + Refresh Token --------------|        |
     |                            |                               |
     |--- (7) API Call + Bearer Token ----------------------->    |
     |                            |                               |
     |<-- (8) API Response ----------------------------------------|
```

---

## Configuration Summary

**Your current client (external-app):**

- **ClientId:** external-app
- **Secret:** external-secret
- **Grant Types:** Authorization Code + Client Credentials
- **Redirect URIs:** http://localhost:3000/callback, http://localhost:3000/oauth2-redirect, http://localhost:8080/callback, capacitor://localhost/callback
- **Allowed Scopes:** openid, profile, email, roles, api1
- **Allowed CORS Origins:** http://localhost:3000, http://localhost:8080
- **Token Endpoint:** https://localhost:5001/connect/token
- **Authorize Endpoint:** https://localhost:5001/connect/authorize
- **Your API:** https://localhost:5001/api (expects `Authorization: Bearer <access_token>` with `aud=api1`)

---

## Next Steps

1. **For Web Apps (React, Vue, Angular):**

   - Use an OAuth library like `oidc-client-ts` or `@react-oauth/google`
   - Configure with ClientId `external-app`, redirect URI, scopes `openid profile api1`

2. **For Mobile Apps (React Native, Flutter):**

   - Use `expo-auth-session` (Expo) or `react-native-oauth`
   - Same OAuth flow

3. **For Backend Services (Python, Node, Java):**

   - Use Client Credentials flow
   - Libraries: `requests-oauthlib`, `passport-oauth2`, `spring-security-oauth2`

4. **Custom Implementation:**
   - Follow OAuth 2.0 + PKCE specs
   - Token endpoint: POST https://localhost:5001/connect/token

---

## Important Security Notes

- Never expose `client_secret` in browser code — always use Client Credentials on the server
- Always use HTTPS in production (enforce with `RequireHttpsMetadata = true` in Program.cs)
- Use PKCE for browser/mobile apps (enforced: `RequirePkce = true`)
- Validate token signatures (your API does this automatically via JwtBearer)
- Use short-lived access tokens (default ~1 hour) and refresh tokens for long-lived access
