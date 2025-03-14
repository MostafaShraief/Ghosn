import { useState, useEffect } from "react";
import {
  Container,
  Typography,
  Box,
  Alert,
  CircularProgress,
} from "@mui/material";
import { testBackendConnection } from "@/services/api";
import AIPrompt from "@/components/AIPrompt";

function App() {
  const [backendConnected, setBackendConnected] = useState(false);
  const [connectionError, setConnectionError] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkBackendConnection = async () => {
      try {
        await testBackendConnection();
        setBackendConnected(true);
      } catch (error) {
        console.error("Backend connection failed:", error);
        setConnectionError(true);
      } finally {
        setLoading(false);
      }
    };

    checkBackendConnection();
  }, []);

  return (
    <Container maxWidth="lg">
      <Box sx={{ my: 4, textAlign: "center" }}>
        <Typography variant="h3" component="h1" gutterBottom>
          Ghosn AI Assistant
        </Typography>
      </Box>

      {loading ? (
        <Box sx={{ display: "flex", justifyContent: "center", my: 4 }}>
          <CircularProgress />
        </Box>
      ) : connectionError ? (
        <Alert severity="error" sx={{ mb: 4 }}>
          Could not connect to the backend. Please make sure the backend server
          is running.
        </Alert>
      ) : (
        backendConnected && (
          <Alert severity="success" sx={{ mb: 4 }}>
            Successfully connected to backend!
          </Alert>
        )
      )}

      {/*  AIPrompt is now a separate route, so remove it from here */}
      <AIPrompt />
    </Container>
  );
}

export default App;
