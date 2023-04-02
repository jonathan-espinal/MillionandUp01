import { api } from "src/boot/axios";

export const listAsync = async (filters = {}) => {
  const response = await api.get("Products", {
    params: filters,
  });
  return response.data;
};

export const retrieveAsync = async (source, id) => {
  const response = await api.get("Products/" + source + "/" + id);
  return response.data;
};
