# Hướng Dẫn Sử Dụng IdentityServer Authorization Flow với Razor UI

## 📋 Tổng Quan

Flow này sử dụng **OAuth 2.0 Authorization Code Grant với PKCE** để authenticate users từ React app thông qua IdentityServer với Razor UI login page.

## 🏗️ Kiến Trúc

```
React Client (Port 3000/5173)
    ↓
IdentityServer.UI (Port 7070) - Razor Pages Login
    ↓
IdentityServer Authorization Endpoint (/connect/authorize)
    ↓
Token Endpoint (/connect/token)
    ↓
API Services (với Bearer Token)
```

---

## 📝 Các Bước Setup và Sử Dụng

### **Bước 1: Kiểm Tra Database Connection**

1. Mở file `KitPraid.Services/IdentityServer/IdentityServer.UI/appsettings.json`
2. Kiểm tra connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=KitPraid;User Id=sa;Password=Trantienloc2411;TrustServerCertificate=true"
  }
}
```
3. Đảm bảo SQL Server đang chạy và database `KitPraid` đã được tạo

### **Bước 2: Chạy Database Migrations (Nếu cần)**

Nếu database chưa có tables, chạy migrations:

```bash
# Navigate to IdentityServer.Infrastructure project
cd IdentityServer.Infrastructure

# Run migrations
dotnet ef database update --startup-project ../KitPraid.Services/IdentityServer/IdentityServer.UI/IdentityServer.UI.csproj
```

### **Bước 3: Tạo User Test (Nếu chưa có)**

Bạn có thể tạo user thông qua:
- Register page trong React app (`/auth` → Sign Up tab)
- Hoặc trực tiếp trong database
- Hoặc qua IdentityServer API

### **Bước 4: Cấu Hình React App Environment Variables**

1. Tạo file `.env` trong `KitPraid.FrontEnd/kitpraid/` (nếu chưa có):
```env
VITE_IDENTITY_SERVER_URL=https://localhost:7070
VITE_API_BASE_URL=http://localhost:3000/api
```

2. Hoặc kiểm tra trong code (`src/services/auth.js`):
   - Mặc định: `https://localhost:7070` (đã đúng)

### **Bước 5: Kiểm Tra Client Configuration**

Mở file `IdentityServer.Infrastructure/Configuration/Config.cs` và đảm bảo `react-client` được cấu hình:

```csharp
new Client
{
    ClientId = "react-client",
    RequireClientSecret = false,
    AllowedGrantTypes = GrantTypes.Code,
    RedirectUris = {
        "http://localhost:3000/callback",
        "http://localhost:5173/callback"  // Nếu dùng Vite
    },
    PostLogoutRedirectUris = { 
        "http://localhost:3000/",
        "http://localhost:5173/"
    },
    AllowOfflineAccess = true,
    AllowedScopes = { "openid", "profile", "email", "roles", "api1", "web" },
    AllowedCorsOrigins = { 
        "http://localhost:3000", 
        "http://localhost:5173" 
    },
    RequirePkce = true,
    AllowPlainTextPkce = false,
}
```

### **Bước 6: Chạy IdentityServer.UI**

1. Mở terminal/PowerShell
2. Navigate đến project:
```bash
cd KitPraid.Services/IdentityServer/IdentityServer.UI
```

3. Chạy project:
```bash
dotnet run
```

Hoặc trong Visual Studio/Rider:
- Set `IdentityServer.UI` làm startup project
- Nhấn F5 hoặc Run

4. Verify IdentityServer đang chạy:
   - Mở browser: `https://localhost:7070`
   - Bạn sẽ thấy IdentityServer home page hoặc error page (điều này là bình thường)

### **Bước 7: Chạy React App**

1. Mở terminal/PowerShell mới
2. Navigate đến React app:
```bash
cd KitPraid.FrontEnd/kitpraid
```

3. Install dependencies (nếu chưa):
```bash
npm install
# hoặc
yarn install
```

4. Chạy React app:
```bash
npm run dev
# hoặc
yarn dev
```

5. Verify React app đang chạy:
   - Mở browser: `http://localhost:3000` hoặc `http://localhost:5173`
   - Bạn sẽ thấy home page

---

## 🚀 Sử Dụng Flow

### **Scenario 1: User Login từ React App**

1. **Mở React App** trong browser: `http://localhost:3000` hoặc `http://localhost:5173`

2. **Navigate đến Login Page**:
   - Click vào "Sign In" button hoặc navigate đến `/auth`
   - Bạn sẽ thấy Authorization page với Sign In/Sign Up tabs

3. **Click "Sign In" Button**:
   - React app sẽ tự động:
     - Generate PKCE pair (codeVerifier + codeChallenge)
     - Store trong sessionStorage
     - Redirect browser đến: `https://localhost:7070/connect/authorize?...`

4. **IdentityServer Redirect đến Login Page**:
   - Browser sẽ redirect đến: `https://localhost:7070/Account/Login?returnUrl=...`
   - Đây là Razor UI login page

5. **Nhập Credentials**:
   - Email: (email của user đã tạo)
   - Password: (password của user)
   - Click "Login"

6. **IdentityServer Authenticates**:
   - `IAccountService` sử dụng `SignInManager` để authenticate
   - Nếu thành công, redirect về `/connect/authorize` với authenticated cookie

7. **IdentityServer Issues Authorization Code**:
   - IdentityServer phát hành authorization code
   - Redirect về: `http://localhost:3000/callback?code=...&state=...`

8. **Callback Page Xử Lý**:
   - `Callback.jsx` extract code và state từ URL
   - Verify state parameter (CSRF protection)
   - Exchange code cho tokens tại `/connect/token` với PKCE verifier

9. **Tokens Được Lưu**:
   - `access_token` và `refresh_token` được lưu trong localStorage
   - User được redirect về home page

10. **Sử Dụng Token để Gọi API**:
    - React app tự động thêm `Authorization: Bearer {token}` header
    - Gọi các protected API endpoints

---

## 🔍 Kiểm Tra Flow Hoạt Động

### **Check 1: IdentityServer Endpoints**

Mở browser và kiểm tra các endpoints:

1. **Discovery Document**:
   ```
   https://localhost:7070/.well-known/openid-configuration
   ```
   - Phải trả về JSON với các endpoints

2. **Authorization Endpoint**:
   ```
   https://localhost:7070/connect/authorize?client_id=react-client&redirect_uri=http://localhost:3000/callback&response_type=code&scope=openid profile&code_challenge=...&code_challenge_method=S256
   ```
   - Phải redirect đến login page nếu chưa authenticated

3. **Login Page**:
   ```
   https://localhost:7070/Account/Login
   ```
   - Phải hiển thị Razor UI login form

### **Check 2: React App**

1. **Open Browser Console** (F12)
2. **Navigate đến `/auth`**
3. **Click "Sign In"**
4. **Kiểm tra**:
   - sessionStorage có `pkce_code_verifier` và `pkce_state`
   - Browser redirect đến IdentityServer
   - Sau khi login, redirect về `/callback`
   - localStorage có `authToken`

### **Check 3: Network Tab**

1. Mở Browser DevTools → Network tab
2. Thực hiện login flow
3. Kiểm tra các requests:
   - `GET /connect/authorize` → 302 redirect
   - `GET /Account/Login` → 200 (Razor page)
   - `POST /Account/Login` → 302 redirect
   - `GET /connect/authorize` → 302 redirect với code
   - `GET /callback?code=...` → 200 (React page)
   - `POST /connect/token` → 200 (tokens response)

---

## 🐛 Troubleshooting

### **Vấn Đề 1: CORS Error**

**Triệu chứng**: Browser console hiển thị CORS error khi gọi `/connect/token`

**Giải pháp**:
1. Kiểm tra `Program.cs` trong IdentityServer.UI:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
```

2. Đảm bảo `app.UseCors("AllowReactClient")` được gọi trước `UseIdentityServer()`

### **Vấn Đề 2: Invalid Redirect URI**

**Triệu chứng**: Error "Invalid redirect_uri" khi redirect về callback

**Giải pháp**:
1. Kiểm tra `RedirectUris` trong `Config.cs` phải match chính xác:
   - `http://localhost:3000/callback` (không có trailing slash)
   - Hoặc `http://localhost:5173/callback` nếu dùng Vite

2. Kiểm tra `REDIRECT_URI` trong React app (`auth.js`):
```javascript
const REDIRECT_URI = `${window.location.origin}/callback`;
```

### **Vấn Đề 3: PKCE Validation Failed**

**Triệu chứng**: Error "Invalid code_verifier" khi exchange token

**Giải pháp**:
1. Kiểm tra `pkce.js` utility đang generate đúng format
2. Đảm bảo `code_verifier` được lưu trong sessionStorage trước khi redirect
3. Kiểm tra `code_challenge_method` phải là `S256`

### **Vấn Đề 4: Login Page Không Hiển Thị**

**Triệu chứng**: Redirect đến `/connect/authorize` nhưng không thấy login page

**Giải pháp**:
1. Kiểm tra `Program.cs` có cấu hình:
```csharp
options.UserInteraction.LoginUrl = "/Account/Login";
```

2. Kiểm tra Razor Pages được map:
```csharp
app.MapRazorPages();
```

3. Kiểm tra `Login.cshtml` và `Login.cshtml.cs` tồn tại trong `Pages/Account/`

### **Vấn Đề 5: SSL Certificate Error**

**Triệu chứng**: Browser warning về SSL certificate khi truy cập `https://localhost:7070`

**Giải pháp**:
1. Click "Advanced" → "Proceed to localhost" (development only)
2. Hoặc trust development certificate:
```bash
dotnet dev-certs https --trust
```

### **Vấn Đề 6: User Không Được Authenticate**

**Triệu chứng**: Login thành công nhưng vẫn redirect về login page

**Giải pháp**:
1. Kiểm tra `AccountService.SignInAsync()` có return `Success = true`
2. Kiểm tra cookie authentication được cấu hình đúng
3. Kiểm tra `SignInManager.PasswordSignInAsync()` result
4. Kiểm tra user `IsActive = true` trong database

---

## 📊 Flow Diagram Chi Tiết

```
┌─────────────┐
│ React App   │
│ /auth       │
└──────┬──────┘
       │ 1. User clicks "Sign In"
       │ 2. Generate PKCE pair
       │ 3. Store in sessionStorage
       │ 4. Redirect to /connect/authorize
       ↓
┌──────────────────────────────────────┐
│ IdentityServer                        │
│ /connect/authorize                    │
│ (Not authenticated)                   │
└──────┬───────────────────────────────┘
       │ 5. Redirect to login
       ↓
┌──────────────────────────────────────┐
│ IdentityServer.UI                    │
│ /Account/Login                       │
│ (Razor Page)                         │
└──────┬───────────────────────────────┘
       │ 6. User enters credentials
       │ 7. POST to /Account/Login
       │ 8. IAccountService authenticates
       │ 9. SignInManager signs in user
       ↓
┌──────────────────────────────────────┐
│ IdentityServer                        │
│ /connect/authorize                    │
│ (Authenticated cookie)                │
└──────┬───────────────────────────────┘
       │ 10. Issue authorization code
       │ 11. Redirect to callback
       ↓
┌─────────────┐
│ React App   │
│ /callback   │
│ ?code=...   │
└──────┬──────┘
       │ 12. Extract code & state
       │ 13. Exchange for tokens
       ↓
┌──────────────────────────────────────┐
│ IdentityServer                        │
│ /connect/token                        │
│ POST with code + PKCE verifier       │
└──────┬───────────────────────────────┘
       │ 14. Validate PKCE
       │ 15. Return tokens
       ↓
┌─────────────┐
│ React App   │
│ Store tokens│
│ Redirect /  │
└─────────────┘
```

---

## ✅ Checklist Trước Khi Test

- [ ] SQL Server đang chạy
- [ ] Database `KitPraid` đã được tạo
- [ ] Migrations đã chạy (nếu cần)
- [ ] User test đã được tạo
- [ ] IdentityServer.UI đang chạy trên `https://localhost:7070`
- [ ] React app đang chạy trên `http://localhost:3000` hoặc `http://localhost:5173`
- [ ] Browser console không có errors
- [ ] CORS được cấu hình đúng
- [ ] Redirect URIs match trong Config.cs và React app
- [ ] PKCE utility hoạt động đúng

---

## 🎯 Test Cases

### **Test Case 1: Successful Login**
1. Navigate đến `/auth`
2. Click "Sign In"
3. Nhập credentials đúng
4. ✅ Expected: Redirect về home page với token trong localStorage

### **Test Case 2: Invalid Credentials**
1. Navigate đến `/auth`
2. Click "Sign In"
3. Nhập credentials sai
4. ✅ Expected: Error message trên login page, không redirect

### **Test Case 3: Cancel Login**
1. Navigate đến `/auth`
2. Click "Sign In"
3. Trên login page, click browser back
4. ✅ Expected: Quay về React app

### **Test Case 4: Token Usage**
1. Login thành công
2. Gọi một protected API endpoint
3. ✅ Expected: Request có `Authorization: Bearer {token}` header

---

## 📚 Tài Liệu Tham Khảo

- [Duende IdentityServer Documentation](https://docs.duendesoftware.com/identityserver)
- [OAuth 2.0 Authorization Code Flow with PKCE](https://oauth.net/2/pkce/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)

---

## 💡 Tips

1. **Development**: Luôn kiểm tra browser console và network tab để debug
2. **Security**: Trong production, đảm bảo sử dụng HTTPS cho tất cả endpoints
3. **Tokens**: Access tokens có lifetime 1 hour, refresh tokens có thể được dùng để renew
4. **Logging**: Enable logging trong IdentityServer để debug:
   ```csharp
   options.Events.RaiseErrorEvents = true;
   options.Events.RaiseInformationEvents = true;
   ```

---

**Chúc bạn thành công! 🎉**

