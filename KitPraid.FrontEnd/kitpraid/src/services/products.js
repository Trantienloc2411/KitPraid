import api from './api';

/**
 * Products Service
 * Handles all product-related API calls
 */
export const productsService = {
  /**
   * Get list of products
   * @param {Object} params - Query parameters (page, limit, category, etc.)
   * @returns {Promise}
   */
  getProducts: async (params = {}) => {
    const response = await api.get('/products', { params });
    return response.data;
  },

  /**
   * Get product by ID
   * @param {string|number} id - Product ID
   * @returns {Promise}
   */
  getProductById: async (id) => {
    const response = await api.get(`/products/${id}`);
    return response.data;
  },

  /**
   * Search products
   * @param {Object} searchParams - { q, category, priceRange, etc. }
   * @returns {Promise}
   */
  searchProducts: async (searchParams) => {
    const response = await api.get('/products/search', { params: searchParams });
    return response.data;
  },

  /**
   * Get product categories
   * @returns {Promise}
   */
  getCategories: async () => {
    const response = await api.get('/products/categories');
    return response.data;
  },

  /**
   * Create new product (Admin only)
   * @param {Object} productData - Product information
   * @returns {Promise}
   */
  createProduct: async (productData) => {
    const response = await api.post('/products', productData);
    return response.data;
  },

  /**
   * Update product (Admin only)
   * @param {string|number} id - Product ID
   * @param {Object} productData - Updated product information
   * @returns {Promise}
   */
  updateProduct: async (id, productData) => {
    const response = await api.put(`/products/${id}`, productData);
    return response.data;
  },

  /**
   * Delete product (Admin only)
   * @param {string|number} id - Product ID
   * @returns {Promise}
   */
  deleteProduct: async (id) => {
    const response = await api.delete(`/products/${id}`);
    return response.data;
  },
};

export default productsService;

