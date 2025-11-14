import { getApiClient } from './api';

// Use storage-specific API client (can use different base URL)
const api = getApiClient('storage');

/**
 * Storage Service
 * Handles file upload and storage API calls
 * Uses storage-specific base URL
 */
export const storageService = {
  /**
   * Upload file
   * @param {FormData|File} file - File to upload
   * @param {Object} options - Upload options (folder, metadata, etc.)
   * @returns {Promise}
   */
  uploadFile: async (file, options = {}) => {
    const formData = new FormData();
    if (file instanceof File) {
      formData.append('file', file);
    } else {
      formData.append('file', file);
    }
    
    // Add options to formData
    Object.keys(options).forEach(key => {
      formData.append(key, options[key]);
    });

    const response = await api.post('/upload', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  /**
   * Delete file
   * @param {string} fileId - File ID
   * @returns {Promise}
   */
  deleteFile: async (fileId) => {
    const response = await api.delete(`/files/${fileId}`);
    return response.data;
  },

  /**
   * Get file URL
   * @param {string} fileId - File ID
   * @returns {Promise}
   */
  getFileUrl: async (fileId) => {
    const response = await api.get(`/files/${fileId}/url`);
    return response.data;
  },
};

export default storageService;

