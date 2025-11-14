/**
 * Services Barrel Export
 * Export all services from a single file for easy importing
 */

// Base API client and utilities
export { 
  api, 
  default as apiClient,
  createApi,
  getApiClient,
  apiBaseURLs 
} from './api';

// Service modules
export { authService } from './auth';
export { productsService } from './products';
export { cartService } from './cart';
export { ordersService } from './orders';
export { userService } from './user';
export { newsletterService } from './newsletter';
export { paymentService } from './payment';
export { storageService } from './storage';

// Default exports
export { default as auth } from './auth';
export { default as products } from './products';
export { default as cart } from './cart';
export { default as orders } from './orders';
export { default as user } from './user';
export { default as newsletter } from './newsletter';
export { default as payment } from './payment';
export { default as storage } from './storage';

