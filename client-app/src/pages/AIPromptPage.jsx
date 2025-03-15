import React, { useState, useEffect } from "react";
import { Container, Box, Alert, CircularProgress } from "@mui/material";
import { testBackendConnection } from "@/services/api";
import AIPrompt from "@/components/AIPrompt";

const AIPromptPage = () => {
  const [backendConnected, setBackendConnected] = useState(false);
  const [connectionError, setConnectionError] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkBackendConnection = async () => {
      try {
        setLoading(true);
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
    <Container>
      {loading ? (
        <Box sx={{ display: "flex", justifyContent: "center", my: 6 }}>
          <CircularProgress size={48} thickness={4} />
        </Box>
      ) : (
        <Box>
          {connectionError ? (
            <Alert
              severity="error"
              sx={{
                mb: 4,
                borderRadius: 2,
                boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
              }}
            >
              Could not connect to the backend. Please check your server
              connection.
            </Alert>
          ) : (
            backendConnected && (
              <Alert
                severity="success"
                sx={{
                  mb: 4,
                  borderRadius: 2,
                  boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                }}
              >
                Successfully connected to backend!
              </Alert>
            )
          )}
        </Box>
      )}
      <AIPrompt />
    </Container>
  );
};

export default AIPromptPage;
