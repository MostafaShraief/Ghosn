import { useState, useEffect } from "react";
import {
  Container,
  Typography,
  Box,
  Alert,
  CircularProgress,
  Fade,
} from "@mui/material";
import { testBackendConnection } from "@/services/api";
import AIPrompt from "@/components/AIPrompt";
import PlantingForm from "@/components/PlantingForm";

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
    <Container maxWidth="md">
      <Fade in={true} timeout={500}>
        <Box sx={{ my: 6, textAlign: "center" }}>
          <Typography
            variant="h2"
            component="h1"
            sx={{
              fontWeight: 700,
            }}
          >
            Ghosn
          </Typography>
        </Box>
      </Fade>

      {loading ? (
        <Box sx={{ display: "flex", justifyContent: "center", my: 6 }}>
          <CircularProgress size={48} thickness={4} />
        </Box>
      ) : (
        <Fade in={true} timeout={800}>
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
        </Fade>
      )}
      <AIPrompt />
      <PlantingForm />

    </Container>
  );
}

export default App;
