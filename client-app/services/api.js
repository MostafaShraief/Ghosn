import axios from "axios";

const API_URL = "http://localhost:5025";

const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export const testBackendConnection = async () => {
  try {
    const response = await api.get("/api/test");
    return response.data;
  } catch (error) {
    console.error("Error connecting to backend:", error);
    throw error;
  }
};

export const getAICompletion = async (prompt) => {
  try {
    const response = await api.post("/api/ai/completion", { prompt });
    return response.data;
  } catch (error) {
    console.error("Error getting AI completion:", error);
    throw error;
  }
};

export default api;
