# Routing & Layout Guide

## How MainLayout Works

The `MainLayout` component automatically wraps all pages with Navigation and Footer. You don't need to add Navigation to each page manually.

## Adding New Pages

### Step 1: Create Your Page Component

Create a new file in `src/pages/`:

```jsx
// src/pages/ProductsPage.jsx
import React from "react";

const ProductsPage = () => {
  return (
    <div style={{ padding: "2rem" }}>
      <h1>Products Page</h1>
      {/* Your content here */}
    </div>
  );
};

export default ProductsPage;
```

### Step 2: Add Route to Router

Open `src/router/index.jsx` and add your route:

**For pages WITH Navigation and Footer:**

```jsx
<Route element={<LayoutRoute showNavigation={true} showFooter={true} />}>
  <Route path="/" element={<HomePage />} />
  <Route path="/products" element={<ProductsPage />} /> {/* Add here */}
  <Route path="/cart" element={<CartPage />} /> {/* Add here */}
</Route>
```

**For pages WITHOUT Navigation (e.g., Login):**

```jsx
<Route element={<LayoutRoute showNavigation={false} showFooter={false} />}>
  <Route path="/auth" element={<Authorization />} />
  <Route path="/login" element={<LoginPage />} /> {/* Add here */}
</Route>
```

## Testing

1. Navigate to `http://localhost:5173/` - Should show Navigation
2. Navigate to `http://localhost:5173/test` - Should show Navigation
3. Navigate to `http://localhost:5173/auth` - Should NOT show Navigation

## Important Notes

- ✅ All routes inside `LayoutRoute` with `showNavigation={true}` will have Navigation
- ✅ You don't need to import Navigation in your page components
- ✅ Navigation is automatically included by MainLayout
- ❌ Don't add `<Navigation />` manually in your pages

## Current Pages

- `/` - HomePage (with Navigation & Footer)
- `/test` - TestPage (with Navigation & Footer)
- `/auth` - Authorization (without Navigation & Footer)
