# Hướng dẫn Fetch Data từ Server

## Tổng quan

Project đã được setup sẵn với:

- **Axios** cho HTTP requests
- **React Query (@tanstack/react-query)** cho data fetching, caching, và state management
- **Services Architecture** - API calls được tách thành các file riêng theo resource

## Cấu trúc Services

API calls được tổ chức trong thư mục `src/services/`:

```
src/services/
├── api.js          # Axios instance và interceptors
├── auth.js         # Authentication services
├── products.js     # Product services
├── cart.js         # Cart services
├── orders.js       # Order services
├── user.js         # User services
├── newsletter.js   # Newsletter services
└── index.js        # Barrel export (export tất cả)
```

## Các bước thực hiện

### Bước 1: Cấu hình API Base URL

Tạo file `.env` trong thư mục root của project:

```env
VITE_API_BASE_URL=http://localhost:3000/api
# hoặc
VITE_API_BASE_URL=https://api.yourdomain.com/api
```

### Bước 2: Sử dụng Services có sẵn

Các services đã được tạo sẵn, bạn chỉ cần import và sử dụng:

```javascript
// Import từ services
import { productsService, authService, cartService } from "../services";
// hoặc import riêng
import productsService from "../services/products";
```

### Bước 3: Sử dụng Services với React Query

**Option 1: Sử dụng trực tiếp trong component**

```jsx
import { useQuery, useMutation } from "@tanstack/react-query";
import { productsService } from "../services";

function ProductsPage() {
  // GET Request - Fetch danh sách products
  const { data, isLoading, error, refetch } = useQuery({
    queryKey: ["products"], // Unique key cho cache
    queryFn: () => productsService.getProducts(), // Sử dụng service
    staleTime: 5 * 60 * 1000, // Data fresh trong 5 phút
  });

  // POST Request - Tạo product mới
  const createMutation = useMutation({
    mutationFn: (newProduct) => productsService.createProduct(newProduct), // Sử dụng service
    onSuccess: () => {
      // Invalidate cache để refetch danh sách
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h1>Products</h1>
      {data?.map((product) => (
        <div key={product.id}>{product.name}</div>
      ))}
      <button onClick={() => createMutation.mutate({ name: "New Product" })}>
        Create Product
      </button>
    </div>
  );
}
```

**Option 2: Tạo Custom Hook (Recommended)**

Tạo file `src/hooks/useProducts.js`:

```jsx
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { productsService } from "../services";

// GET - Fetch danh sách products
export const useProducts = (params) => {
  return useQuery({
    queryKey: ["products", params],
    queryFn: () => productsService.getProducts(params),
  });
};

// GET - Fetch chi tiết product
export const useProduct = (id) => {
  return useQuery({
    queryKey: ["products", id],
    queryFn: () => productsService.getProductById(id),
    enabled: !!id, // Chỉ fetch khi có id
  });
};

// POST - Tạo product mới
export const useCreateProduct = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (productData) => productsService.createProduct(productData),
    onSuccess: () => {
      // Invalidate và refetch danh sách
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
};

// PUT - Cập nhật product
export const useUpdateProduct = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, ...productData }) =>
      productsService.updateProduct(id, productData),
    onSuccess: (data, variables) => {
      // Update cache cho product cụ thể
      queryClient.setQueryData(["products", variables.id], data);
      // Invalidate danh sách
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
};

// DELETE - Xóa product
export const useDeleteProduct = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id) => productsService.deleteProduct(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });
};
```

Sử dụng trong component:

```jsx
import { useProducts, useCreateProduct } from "../hooks/useProducts";

function ProductsPage() {
  const {
    data: products,
    isLoading,
    error,
  } = useProducts({ page: 1, limit: 10 });
  const createProduct = useCreateProduct();

  const handleCreate = () => {
    createProduct.mutate(
      { name: "New Product", price: 100 },
      {
        onSuccess: () => {
          console.log("Product created!");
        },
        onError: (error) => {
          console.error("Error:", error);
        },
      }
    );
  };

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      {products?.map((product) => (
        <div key={product.id}>{product.name}</div>
      ))}
      <button onClick={handleCreate}>Create</button>
    </div>
  );
}
```

### Bước 4: Thêm Service mới (nếu cần)

Nếu bạn cần thêm service mới, tạo file trong `src/services/`:

**Ví dụ: `src/services/reviews.js`**

```jsx
import api from "./api";

export const reviewsService = {
  getReviews: async (productId) => {
    const response = await api.get(`/products/${productId}/reviews`);
    return response.data;
  },

  createReview: async (productId, reviewData) => {
    const response = await api.post(
      `/products/${productId}/reviews`,
      reviewData
    );
    return response.data;
  },
};

export default reviewsService;
```

Sau đó export trong `src/services/index.js`:

```jsx
export { reviewsService } from "./reviews";
export { default as reviews } from "./reviews";
```

### Bước 5: Xử lý Authentication

Sử dụng `authService`:

```jsx
import { authService } from "../services";
import { useMutation } from "@tanstack/react-query";

function LoginPage() {
  const loginMutation = useMutation({
    mutationFn: (credentials) => authService.login(credentials),
    onSuccess: (data) => {
      const { token } = data;
      // Lưu token
      localStorage.setItem("authToken", token);
      // Token sẽ tự động được thêm vào các request tiếp theo
    },
  });

  const handleLogin = (email, password) => {
    loginMutation.mutate({ email, password });
  };

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        handleLogin(e.target.email.value, e.target.password.value);
      }}
    >
      {/* Form fields */}
    </form>
  );
}
```

### Bước 6: Xử lý Errors

API client đã có error handling, nhưng bạn có thể customize trong service hoặc component:

```jsx
// Trong component
const { data, error, isError } = useQuery({
  queryKey: ["products"],
  queryFn: async () => {
    try {
      return await productsService.getProducts();
    } catch (error) {
      // Xử lý error cụ thể
      if (error.response?.status === 404) {
        throw new Error("Products not found");
      }
      if (error.response?.status === 401) {
        // Redirect to login
        window.location.href = "/auth";
      }
      throw error;
    }
  },
  retry: 1, // Retry 1 lần nếu fail
  retryDelay: 1000, // Đợi 1 giây trước khi retry
});
```

## Ví dụ đầy đủ

### 1. Component với GET Request

```jsx
import { useQuery } from "@tanstack/react-query";
import { productsService } from "../services";

function ProductList() {
  const { data, isLoading, error, refetch } = useQuery({
    queryKey: ["products"],
    queryFn: () => productsService.getProducts(),
  });

  if (isLoading) return <div>Loading products...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <button onClick={() => refetch()}>Refresh</button>
      {data?.map((product) => (
        <div key={product.id}>{product.name}</div>
      ))}
    </div>
  );
}
```

### 2. Component với POST Request

```jsx
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { productsService } from "../services";

function CreateProduct() {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (productData) => productsService.createProduct(productData),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
      alert("Product created!");
    },
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    mutation.mutate({
      name: formData.get("name"),
      price: formData.get("price"),
    });
  };

  return (
    <form onSubmit={handleSubmit}>
      <input name="name" placeholder="Product name" />
      <input name="price" type="number" placeholder="Price" />
      <button type="submit" disabled={mutation.isPending}>
        {mutation.isPending ? "Creating..." : "Create Product"}
      </button>
    </form>
  );
}
```

### 3. Component với Query Parameters

```jsx
import { useQuery } from "@tanstack/react-query";
import { productsService } from "../services";

function SearchProducts({ searchTerm }) {
  const { data } = useQuery({
    queryKey: ["products", "search", searchTerm], // Include searchTerm in key
    queryFn: () => productsService.searchProducts({ q: searchTerm }),
    enabled: !!searchTerm, // Chỉ fetch khi có searchTerm
  });

  return <div>{/* Render results */}</div>;
}
```

### 4. Sử dụng nhiều Services

```jsx
import { useQuery } from "@tanstack/react-query";
import { productsService, cartService, userService } from "../services";

function Dashboard() {
  const { data: products } = useQuery({
    queryKey: ["products"],
    queryFn: () => productsService.getProducts(),
  });

  const { data: cart } = useQuery({
    queryKey: ["cart"],
    queryFn: () => cartService.getCart(),
  });

  const { data: profile } = useQuery({
    queryKey: ["user", "profile"],
    queryFn: () => userService.getProfile(),
  });

  return (
    <div>
      <h1>Dashboard</h1>
      <p>Products: {products?.length}</p>
      <p>Cart items: {cart?.items?.length}</p>
      <p>Welcome, {profile?.name}</p>
    </div>
  );
}
```

## Best Practices

1. **Sử dụng Custom Hooks**: Tạo custom hooks cho mỗi resource để code dễ maintain
2. **Query Keys**: Sử dụng query keys có cấu trúc: `['resource', id, 'sub-resource']`
3. **Cache Management**: Sử dụng `invalidateQueries` để refresh data sau mutations
4. **Error Handling**: Luôn xử lý loading và error states
5. **Optimistic Updates**: Sử dụng `setQueryData` để update UI ngay lập tức

## Tài liệu tham khảo

- [React Query Documentation](https://tanstack.com/query/latest)
- [Axios Documentation](https://axios-http.com/docs/intro)
