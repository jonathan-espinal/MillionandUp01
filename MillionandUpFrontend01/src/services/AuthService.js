import { api } from "src/boot/axios";

export const authAsync = async (data) => {
  const response = await api.post("Auth", data);
  return response.data;
};
