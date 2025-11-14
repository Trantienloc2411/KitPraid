import React from "react";
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate,
  Outlet,
} from "react-router-dom";
import MainLayout from "../layouts/MainLayout";
import HomePage from "../pages/HomePage";
import Authorization from "../pages/Authorization";
import Callback from "../pages/Callback";

/**
 * Layout Route Wrapper
 * Wraps routes with MainLayout
 * This component uses Outlet to render child routes
 */
const LayoutRoute = ({ showNavigation = true, showFooter = true }) => {
  return (
    <MainLayout showNavigation={showNavigation} showFooter={showFooter}>
      <Outlet />
    </MainLayout>
  );
};

/**
 * Router configuration
 * Add your routes here
 *
 * IMPORTANT: All routes inside LayoutRoute will automatically have Navigation and Footer
 */
const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* 
          Routes with Navigation and Footer 
          All child routes will automatically have Navigation and Footer
        */}
        <Route
          element={<LayoutRoute showNavigation={true} showFooter={true} />}
        >
          {/* Home Page */}
          <Route path="/" element={<HomePage />} />

          {/* Authorization Page - with Navigation and Footer */}
          <Route path="/auth" element={<Authorization />} />

          {/* Add more routes with Navigation and Footer here */}
          {/* Example:
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/products/:id" element={<ProductDetailPage />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/profile" element={<ProfilePage />} />
          */}
        </Route>

        {/* 
          Routes without Navigation (e.g., Auth pages) 
          These pages won't have Navigation or Footer
        */}
        <Route
          element={<LayoutRoute showNavigation={false} showFooter={false} />}
        >
          {/* OAuth Callback - handles redirect from IdentityServer */}
          <Route path="/callback" element={<Callback />} />
          {/* Add more routes without Navigation here */}
          {/* Example:
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          */}
        </Route>

        {/* 404 - Not Found - Redirects to home */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
};

export default Router;
