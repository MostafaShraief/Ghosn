import axios from "axios";

const API_URL =
  import.meta.env.VITE_API_URL || "http://mostafashraief.bsite.net";

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

// Updated getAICompletion function to use GET and URL parameters
export const getAICompletion = async (prompt) => {
  try {
    const response = await api.post("/api/Ghosn/Ai", { prompt });
    console.log("API Response:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error getting AI completion:", error);
    if (error.response) {
      console.error("Server Response Data:", error.response.data);
      console.error("Server Response Status:", error.response.status);
      console.error("Server Response Headers:", error.response.headers);
    }
    throw error;
  }
};

export const login = async (username, password) => {
  try {
    const response = await api.post("/api/Ghosn/Client", {
      // Corrected endpoint
      personID: 0,
      firstName: "",
      lastName: "",
      email: "",
      clientID: 0,
      username: username,
      password: password,
    });
    return response.data; //  Return the entire response data.
  } catch (error) {
    console.error("Error during login:", error);

    if (error.response) {
      // The request was made and the server responded with a status code
      // that falls out of the range of 2xx
      console.error("Server Response Data:", error.response.data);
      console.error("Server Response Status:", error.response.status);
      console.error("Server Response Headers:", error.response.headers);
      throw new Error(
        error.response.data.message ||
          "Login failed.  Please check your credentials."
      ); //Best Practice
    } else if (error.request) {
      // The request was made but no response was received
      // `error.request` is an instance of XMLHttpRequest in the browser
      throw new Error(
        "No response received from the server.  Please check your network connection."
      ); //Best Practice
    } else {
      // Something happened in setting up the request that triggered an Error
      throw new Error("An error occurred while setting up the login request."); //Best Practice
    }
  }
};

// Added function to fetch notifications
export const getNotifications = async () => {
  try {
    const response = await api.get(
      "/api/Ghosn/Notifications/AllClientNotifications"
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching notifications:", error);
    if (error.response) {
      console.error("Server Response Data:", error.response.data);
      console.error("Server Response Status:", error.response.status);
      console.error("Server Response Headers:", error.response.headers);
    }
    throw error; // Re-throw the error for handling in the component
  }
};

export default api;
