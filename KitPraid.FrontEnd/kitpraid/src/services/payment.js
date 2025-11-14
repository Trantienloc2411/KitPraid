import { getApiClient } from './api';

// Use payment-specific API client (can use different base URL)
const api = getApiClient('payment');

/**
 * Payment Service
 * Handles all payment-related API calls
 * Uses payment-specific base URL
 */
export const paymentService = {
  /**
   * Process payment
   * @param {Object} paymentData - Payment information
   * @returns {Promise}
   */
  processPayment: async (paymentData) => {
    const response = await api.post('/payments/process', paymentData);
    return response.data;
  },

  /**
   * Get payment status
   * @param {string|number} paymentId - Payment ID
   * @returns {Promise}
   */
  getPaymentStatus: async (paymentId) => {
    const response = await api.get(`/payments/${paymentId}/status`);
    return response.data;
  },

  /**
   * Refund payment
   * @param {string|number} paymentId - Payment ID
   * @param {Object} refundData - Refund information
   * @returns {Promise}
   */
  refundPayment: async (paymentId, refundData) => {
    const response = await api.post(`/payments/${paymentId}/refund`, refundData);
    return response.data;
  },
};

export default paymentService;

