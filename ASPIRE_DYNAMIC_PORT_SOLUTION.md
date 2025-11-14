# Giải Pháp Port Động cho Aspire

## Vấn đề

Khi chạy với .NET Aspire, FrontEnd được assign port động (ví dụ: 60392, 52031, etc.). Điều này gây lỗi "Invalid redirect_uri" vì IdentityServer không biết port mới.

## ✅ Giải Pháp Đã Triển Khai

### 1. Custom Redirect URI Validator (Development Only)

Tạo `LocalhostRedirectUriValidator` cho phép **bất kỳ localhost port nào** trong Development environment.

**File**: `IdentityServer.Infrastructure/Validation/LocalhostRedirectUriValidator.cs`

```csharp
public class LocalhostRedirectUriValidator : IRedirectUriValidator
{
    public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {
        // In development, allow any localhost port for react-client
        if (_isDevelopment && client.ClientId == "react-client")
        {
            if (Uri.TryCreate(requestedUri, UriKind.Absolute, out var uri))
            {
                if (uri.Host == "localhost" && uri.AbsolutePath == "/callback")
                {
                    return Task.FromResult(true);
                }
            }
        }
        // Fallback to default validation
        ...
    }
}
```

### 2. CORS Wildcard cho Localhost (Development Only)

Cấu hình CORS cho phép **bất kỳ localhost port nào** trong Development.

**File**: `Program.cs`

```csharp
if (builder.Environment.IsDevelopment())
{
    policy.SetIsOriginAllowed(origin =>
    {
        if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
        {
            return uri.Host == "localhost" || uri.Host == "127.0.0.1";
        }
        return false;
    })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}
```

## 🎯 Lợi Ích

✅ **Không cần cập nhật configuration** khi Aspire assign port mới  
✅ **Tự động hoạt động** với bất kỳ localhost port nào  
✅ **An toàn**: Chỉ áp dụng trong Development environment  
✅ **Production-ready**: Production vẫn sử dụng whitelist cụ thể  

## 📝 Cách Sử Dụng

### Development (Aspire)

1. Chạy Aspire:
   ```bash
   dotnet run --project KitPraid.AppHost
   ```

2. Aspire sẽ assign port cho FrontEnd (ví dụ: 52031)

3. IdentityServer **tự động chấp nhận** bất kỳ localhost port nào

4. Không cần restart hay cập nhật configuration

### Production

Trong Production, cần cấu hình rõ ràng trong `appsettings.Production.json`:

```json
{
  "IdentityServer": {
    "ReactClient": {
      "RedirectUris": [
        "https://yourdomain.com/callback"
      ],
      "AllowedCorsOrigins": [
        "https://yourdomain.com"
      ]
    }
  }
}
```

## 🔍 Kiểm Tra

### Test trong Development:

1. Chạy Aspire
2. Kiểm tra port của FrontEnd trong Aspire Dashboard (ví dụ: 52031)
3. Navigate đến React app: `http://localhost:52031`
4. Click "Sign In"
5. Redirect đến Razor login page thành công
6. Login và redirect về React callback thành công

### Không còn lỗi:

❌ `Invalid redirect_uri: http://localhost:52031/callback`  
✅ Accepts any localhost port

❌ `Token Issued Failure`  
✅ Token issued successfully

## 🛡️ Bảo Mật

- **Development**: Cho phép wildcard localhost (an toàn vì chỉ local)
- **Production**: Phải cấu hình whitelist cụ thể
- Validator chỉ áp dụng cho `react-client`
- Chỉ chấp nhận path `/callback` và `/` (post-logout)

## 📊 So Sánh

### Trước khi có giải pháp:

```
Port thay đổi: 60392 → 52031
↓
Lỗi: Invalid redirect_uri
↓
Phải update appsettings.Development.json
↓
Phải restart IdentityServer
```

### Sau khi có giải pháp:

```
Port thay đổi: 60392 → 52031
↓
Tự động chấp nhận
↓
Không cần làm gì
✅ Hoạt động ngay
```

## 🎓 Technical Details

### Redirect URI Validation Flow:

1. React app redirect: `http://localhost:52031` → IdentityServer `/connect/authorize`
2. IdentityServer kiểm tra `redirect_uri` parameter
3. `LocalhostRedirectUriValidator` được gọi
4. Validator kiểm tra:
   - Environment: Development? ✅
   - ClientId: react-client? ✅
   - Host: localhost? ✅
   - Path: /callback? ✅
5. Validation passed ✅

### CORS Flow:

1. React app gửi request từ `http://localhost:52031`
2. Browser gửi `Origin: http://localhost:52031` header
3. CORS policy kiểm tra origin
4. `SetIsOriginAllowed` callback được gọi
5. Kiểm tra host = localhost? ✅
6. CORS allowed ✅

## 💡 Tips

- **Không cần update port** trong appsettings.Development.json nữa
- **Aspire Dashboard** vẫn hữu ích để xem các services đang chạy
- **Production** phải cấu hình cụ thể (không dùng wildcard)
- **Logs** sẽ vẫn hiển thị RedirectUris từ config (cho production)

## 🔗 Related Files

- `IdentityServer.Infrastructure/Validation/LocalhostRedirectUriValidator.cs`
- `KitPraid.Services/IdentityServer/IdentityServer.UI/Program.cs`
- `KitPraid.Services/IdentityServer/IdentityServer.UI/appsettings.Development.json`

---

**Kết luận**: Với giải pháp này, bạn có thể sử dụng Aspire với dynamic ports mà không cần lo lắng về configuration updates!

