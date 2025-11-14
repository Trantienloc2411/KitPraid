import api from './api';

/**
 * Orders Service
 * Handles all order-related API calls
 */
export const ordersService = {
  /**
   * Get user's orders
   * @param {Object} params - Query parameters (page, limit, status, etc.)
   * @returns {Promise}
   */
  getOrders: async (params = {}) => {
    const response = await api.get('/orders', { params });
    return response.data;
  },

  /**
   * Get order by ID
   * @param {string|number} id - Order ID
   * @returns {Promise}
   */
  getOrderById: async (id) => {
    const response = await api.get(`/orders/${id}`);
    return response.data;
  },

  /**
   * Create new order
   * @param {Object} orderData - Order information
   * @returns {Promise}
   */
  createOrder: async (orderData) => {
    const response = await api.post('/orders', orderData);
    return response.data;
  },

  /**
   * Cancel order
   * @param {string|number} id - Order ID
   * @returns {Promise}
   */
  cancelOrder: async (id) => {
    const response = await api.post(`/orders/${id}/cancel`);
    return response.data;
  },
};

export default ordersService;

