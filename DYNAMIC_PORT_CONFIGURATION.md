# Cấu Hình Port Động cho React Client với Aspire

## 📋 Tổng Quan

Khi sử dụng .NET Aspire, FrontEnd có thể được assign port động. IdentityServer cần biết port này để cấu hình RedirectUris và CORS origins.

## 🔧 Cách 1: Sử dụng Environment Variable từ Aspire (Khuyến nghị)

### Bước 1: Cập nhật AppHost.cs

```csharp
var kitpraid = builder.AddViteApp(  
    name: "kitpraid",  
    workingDirectory: Path.Combine(Directory.GetCurrentDirectory(), "../KitPraid.FrontEnd/kitpraid"),  
    packageManager: "npm")  
    .WithReference(identityServerUi)
    .WithEnvironment("PORT", "3000")
    .WithNpmPackageInstallation()  
    .WaitFor(identityServer);

// Pass FrontEnd port to IdentityServer.UI via environment variable
identityServerUi.WithEnvironment("REACT_CLIENT_PORT", kitpraid.GetEndpoint("http").Port.ToString());
```

### Bước 2: Cập nhật appsettings.Development.json

```json
{
  "IdentityServer": {
    "ReactClient": {
      "Port": "${REACT_CLIENT_PORT}"
    }
  }
}
```

## 🔧 Cách 2: Sử dụng Configuration trong appsettings.json

### Cấu hình trong appsettings.Development.json:

```json
{
  "IdentityServer": {
    "ReactClient": {
      // Option 1: Single port
      "Port": "63992",
      
      // Option 2: Multiple redirect URIs (array)
      "RedirectUris": [
        "http://localhost:63992/callback",
        "http://localhost:3000/callback"
      ],
      "PostLogoutRedirectUris": [
        "http://localhost:63992/",
        "http://localhost:3000/"
      ],
      "AllowedCorsOrigins": [
        "http://localhost:63992",
        "http://localhost:3000"
      ]
    }
  }
}
```

## 🔧 Cách 3: Sử dụng Environment Variable trực tiếp

Set environment variable khi chạy:

```bash
# Windows PowerShell
$env:IdentityServer__ReactClient__Port = "63992"
dotnet run --project KitPraid.Services/IdentityServer/IdentityServer.UI

# Linux/Mac
export IdentityServer__ReactClient__Port=63992
dotnet run --project KitPraid.Services/IdentityServer/IdentityServer.UI
```

## 📝 Cách Hoạt Động

1. **Config.GetClients()** đọc configuration từ `appsettings.json` hoặc environment variables
2. Nếu có `IdentityServer:ReactClient:Port`, tự động thêm:
   - `http://localhost:{Port}/callback` vào RedirectUris
   - `http://localhost:{Port}/` vào PostLogoutRedirectUris
   - `http://localhost:{Port}` vào AllowedCorsOrigins
3. Nếu có arrays trong configuration, merge với default values
4. Loại bỏ duplicates tự động

## ✅ Kiểm Tra

Sau khi cấu hình, kiểm tra logs khi start IdentityServer:

```
info: Duende.IdentityServer.Startup[0]
      Using the default authentication scheme Identity.Application for IdentityServer
```

Nếu có lỗi "Invalid redirect_uri", kiểm tra:
1. Port trong configuration có đúng với port FrontEnd đang chạy không
2. Format của RedirectUris có đúng không (phải có `/callback`)
3. CORS origins có match với port không

## 🎯 Ví Dụ Cấu Hình Cho Aspire

### appsettings.Development.json:

```json
{
  "IdentityServer": {
    "IssuerUri": "https://localhost:7072",
    "ReactClient": {
      "Port": ""  // Sẽ được set bởi Aspire qua environment variable
    }
  }
}
```

### AppHost.cs:

```csharp
var kitpraid = builder.AddViteApp(...)
    .WithEnvironment("PORT", "3000");

// Get the actual port assigned by Aspire
var kitpraidHttpEndpoint = kitpraid.GetEndpoint("http");

// Pass to IdentityServer.UI
identityServerUi.WithEnvironment("IdentityServer__ReactClient__Port", 
    kitpraidHttpEndpoint.Port.ToString());
```

## 📚 Lưu Ý

- **Development**: Có thể sử dụng port động hoặc cố định
- **Production**: Nên sử dụng cố định và cấu hình rõ ràng trong appsettings.Production.json
- **Security**: Chỉ allow localhost trong development. Production cần domain thật

## 🔍 Debug

Nếu gặp vấn đề, kiểm tra:

1. **Logs của IdentityServer**: Xem RedirectUris nào được load
2. **Browser Console**: Xem redirect_uri nào được gửi
3. **Network Tab**: Xem request đến `/connect/authorize` có redirect_uri gì

Có thể thêm logging trong `GetReactClient()` để debug:

```csharp
var logger = configuration.GetService<ILogger<Config>>();
logger?.LogInformation("React Client RedirectUris: {RedirectUris}", 
    string.Join(", ", defaultRedirectUris));
```

