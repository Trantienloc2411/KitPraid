import api from "./api";

export const categoryService = {
  getCategory: async () => {
    const response = await api.get("/category/tree");
    return response.data;
  },
};
