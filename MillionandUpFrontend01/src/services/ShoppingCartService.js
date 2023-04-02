import { api } from "src/boot/axios";

export const addToCartAsync = async (data = {}) => {
  const response = await api.post("ShoppingCart/AddToCart", data);
  return response.data;
};

export const retrieveAsync = async () => {
  const response = await api.get("ShoppingCart/Retrieve");
  return response.data;
};

export const checkoutAsync = async () => {
  const response = await api.post("ShoppingCart/Checkout");
  return response.data;
};
