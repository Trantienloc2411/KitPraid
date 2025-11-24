# Hướng dẫn sử dụng Multiple API URLs

## Tổng quan

Hệ thống hỗ trợ nhiều base URLs cho các services khác nhau. Mỗi service có thể sử dụng base URL riêng hoặc chia sẻ cùng một URL.

## Cấu hình

### Bước 1: Cấu hình trong `.env`

Tạo file `.env` trong root directory:

```env
# Default API (dùng cho hầu hết services)
VITE_API_BASE_URL=http://localhost:3000/api

# Auth API (có thể khác hoặc giống default)
VITE_AUTH_API_URL=http://localhost:3001/auth
# Nếu không set, sẽ dùng VITE_API_BASE_URL

# Payment API
VITE_PAYMENT_API_URL=https://payment.example.com/api
# Nếu không set, sẽ dùng VITE_API_BASE_URL

# Storage API (file upload)
VITE_STORAGE_API_URL=https://storage.example.com/api
# Nếu không set, sẽ dùng VITE_API_BASE_URL
```

### Bước 2: Sử dụng trong Services

**Option 1: Service tự động chọn base URL**

Services đã được cấu hình sẵn để sử dụng base URL phù hợp:

```jsx
// auth.js - Tự động dùng auth base URL
import { getApiClient } from "./api";
const api = getApiClient("auth"); // Uses VITE_AUTH_API_URL

// payment.js - Tự động dùng payment base URL
import { getApiClient } from "./api";
const api = getApiClient("payment"); // Uses VITE_PAYMENT_API_URL

// products.js - Dùng default base URL
import api from "./api"; // Uses VITE_API_BASE_URL
```

**Option 2: Tạo service với base URL tùy chỉnh**

```jsx
// src/services/custom.js
import { createApi } from "./api";

// Tạo API client với base URL tùy chỉnh
const customApi = createApi("custom"); // Sẽ tìm VITE_CUSTOM_API_URL

// Hoặc tạo với URL trực tiếp
const directApi = createApi("https://custom-api.example.com");

export const customService = {
  getData: async () => {
    const response = await customApi.get("/data");
    return response.data;
  },
};
```

## Ví dụ sử dụng

### Ví dụ 1: Services dùng cùng base URL

```jsx
// products.js và cart.js đều dùng default API
import api from "./api"; // Cùng base URL

export const productsService = {
  getProducts: async () => {
    const response = await api.get("/products");
    return response.data;
  },
};

export const cartService = {
  getCart: async () => {
    const response = await api.get("/cart");
    return response.data;
  },
};
```

### Ví dụ 2: Service dùng base URL riêng

```jsx
// auth.js dùng auth-specific base URL
import { getApiClient } from "./api";
const api = getApiClient("auth"); // URL khác với default

export const authService = {
  login: async (credentials) => {
    const response = await api.post("/login", credentials);
    return response.data;
  },
};
```

### Ví dụ 3: Tạo service mới với base URL tùy chỉnh

```jsx
// src/services/analytics.js
import { createApi } from "./api";

// Option 1: Dùng service mapping (thêm vào api.js)
const api = createApi("analytics"); // Tìm VITE_ANALYTICS_API_URL

// Option 2: Dùng URL trực tiếp
const api = createApi("https://analytics.example.com/api");

export const analyticsService = {
  trackEvent: async (eventData) => {
    const response = await api.post("/events", eventData);
    return response.data;
  },
};
```

### Ví dụ 4: Service có thể switch base URL

```jsx
// src/services/flexible.js
import { getApiClient, createApi } from "./api";

export const flexibleService = {
  // Dùng default API
  getDefaultData: async () => {
    const defaultApi = getApiClient("default");
    const response = await defaultApi.get("/data");
    return response.data;
  },

  // Dùng custom API
  getCustomData: async (customUrl) => {
    const customApi = createApi(customUrl);
    const response = await customApi.get("/data");
    return response.data;
  },
};
```

## Thêm Base URL mới

### Cách 1: Thêm vào API_BASE_URLS

Mở `src/services/api.js` và thêm vào `API_BASE_URLS`:

```jsx
const API_BASE_URLS = {
  default: import.meta.env.VITE_API_BASE_URL || "http://localhost:3000/api",
  auth:
    import.meta.env.VITE_AUTH_API_URL ||
    import.meta.env.VITE_API_BASE_URL ||
    "http://localhost:3000/api",
  analytics:
    import.meta.env.VITE_ANALYTICS_API_URL ||
    import.meta.env.VITE_API_BASE_URL ||
    "http://localhost:3000/api",
  // Thêm mới ở đây
};
```

Thêm vào service mapping trong `getApiClient`:

```jsx
const serviceMap = {
  auth: "auth",
  payment: "payment",
  analytics: "analytics", // Thêm mới
};
```

### Cách 2: Dùng trực tiếp URL

```jsx
import { createApi } from "./api";

// Tạo API client với URL trực tiếp
const api = createApi("https://new-api.example.com/api");
```

## Best Practices

1. **Naming Convention**:
   - Environment variables: `VITE_<SERVICE>_API_URL`
   - Service names: lowercase (auth, payment, storage)

2. **Fallback Strategy**:
   - Nếu service-specific URL không được set, fallback về default URL
   - Đảm bảo app vẫn hoạt động nếu thiếu config

3. **Service Organization**:
   - Group services theo base URL
   - Services cùng base URL có thể import cùng `api` instance

4. **Documentation**:
   - Document base URL cho mỗi service
   - Update `.env.example` với tất cả URLs

## Ví dụ đầy đủ

```jsx
// .env
VITE_API_BASE_URL=https://api.example.com
VITE_AUTH_API_URL=https://auth.example.com
VITE_PAYMENT_API_URL=https://payment.example.com

// Component sử dụng
import { useQuery } from '@tanstack/react-query';
import { authService, productsService, paymentService } from '../services';

function Dashboard() {
  // Auth service dùng https://auth.example.com
  const { data: user } = useQuery({
    queryKey: ['user'],
    queryFn: () => authService.getMe(),
  });

  // Products service dùng https://api.example.com
  const { data: products } = useQuery({
    queryKey: ['products'],
    queryFn: () => productsService.getProducts(),
  });

  // Payment service dùng https://payment.example.com
  const { data: paymentMethods } = useQuery({
    queryKey: ['payment-methods'],
    queryFn: () => paymentService.getPaymentMethods(),
  });

  return <div>...</div>;
}
```
