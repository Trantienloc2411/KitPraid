import api from './api';

/**
 * Cart Service
 * Handles all cart-related API calls
 */
export const cartService = {
  /**
   * Get user's cart
   * @returns {Promise}
   */
  getCart: async () => {
    const response = await api.get('/cart');
    return response.data;
  },

  /**
   * Add item to cart
   * @param {Object} itemData - { productId, quantity, ... }
   * @returns {Promise}
   */
  addToCart: async (itemData) => {
    const response = await api.post('/cart/add', itemData);
    return response.data;
  },

  /**
   * Update cart item
   * @param {string|number} itemId - Cart item ID
   * @param {Object} updateData - { quantity, ... }
   * @returns {Promise}
   */
  updateCartItem: async (itemId, updateData) => {
    const response = await api.put(`/cart/${itemId}`, updateData);
    return response.data;
  },

  /**
   * Remove item from cart
   * @param {string|number} itemId - Cart item ID
   * @returns {Promise}
   */
  removeFromCart: async (itemId) => {
    const response = await api.delete(`/cart/${itemId}`);
    return response.data;
  },

  /**
   * Clear entire cart
   * @returns {Promise}
   */
  clearCart: async () => {
    const response = await api.post('/cart/clear');
    return response.data;
  },
};

export default cartService;

