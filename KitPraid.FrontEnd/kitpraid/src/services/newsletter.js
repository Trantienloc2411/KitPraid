import api from './api';

/**
 * Newsletter Service
 * Handles newsletter subscription API calls
 */
export const newsletterService = {
  /**
   * Subscribe to newsletter
   * @param {Object} data - { email, ... }
   * @returns {Promise}
   */
  subscribe: async (data) => {
    const response = await api.post('/newsletter/subscribe', data);
    return response.data;
  },

  /**
   * Unsubscribe from newsletter
   * @param {Object} data - { email, token, ... }
   * @returns {Promise}
   */
  unsubscribe: async (data) => {
    const response = await api.post('/newsletter/unsubscribe', data);
    return response.data;
  },
};

export default newsletterService;

