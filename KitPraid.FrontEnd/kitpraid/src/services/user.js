import api from './api';

/**
 * User Service
 * Handles all user-related API calls
 */
export const userService = {
  /**
   * Get user profile
   * @returns {Promise}
   */
  getProfile: async () => {
    const response = await api.get('/user/profile');
    return response.data;
  },

  /**
   * Update user profile
   * @param {Object} profileData - Updated profile information
   * @returns {Promise}
   */
  updateProfile: async (profileData) => {
    const response = await api.put('/user/profile', profileData);
    return response.data;
  },

  /**
   * Get user's wishlist
   * @returns {Promise}
   */
  getWishlist: async () => {
    const response = await api.get('/user/wishlist');
    return response.data;
  },

  /**
   * Add item to wishlist
   * @param {string|number} productId - Product ID
   * @returns {Promise}
   */
  addToWishlist: async (productId) => {
    const response = await api.post('/user/wishlist', { productId });
    return response.data;
  },

  /**
   * Remove item from wishlist
   * @param {string|number} productId - Product ID
   * @returns {Promise}
   */
  removeFromWishlist: async (productId) => {
    const response = await api.delete(`/user/wishlist/${productId}`);
    return response.data;
  },

  /**
   * Get user's addresses
   * @returns {Promise}
   */
  getAddresses: async () => {
    const response = await api.get('/user/addresses');
    return response.data;
  },

  /**
   * Add new address
   * @param {Object} addressData - Address information
   * @returns {Promise}
   */
  addAddress: async (addressData) => {
    const response = await api.post('/user/addresses', addressData);
    return response.data;
  },

  /**
   * Update address
   * @param {string|number} addressId - Address ID
   * @param {Object} addressData - Updated address information
   * @returns {Promise}
   */
  updateAddress: async (addressId, addressData) => {
    const response = await api.put(`/user/addresses/${addressId}`, addressData);
    return response.data;
  },

  /**
   * Delete address
   * @param {string|number} addressId - Address ID
   * @returns {Promise}
   */
  deleteAddress: async (addressId) => {
    const response = await api.delete(`/user/addresses/${addressId}`);
    return response.data;
  },
};

export default userService;

