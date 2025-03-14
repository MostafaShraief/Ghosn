import React, { useState } from "react";
import {
  Box,
  TextField,
  Button,
  Paper,
  Typography,
  CircularProgress,
  Fade,
} from "@mui/material";
import { getAICompletion } from "../services/api";

const AIPrompt = () => {
  const [prompt, setPrompt] = useState("");
  const [response, setResponse] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");

    try {
      const result = await getAICompletion(prompt);
      setResponse(result.text);
    } catch (err) {
      setError("Failed to get AI response. Please try again.");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Fade in={true} timeout={500}>
      <Paper
        elevation={2}
        sx={{
          p: 4,
          borderRadius: 3,
          bgcolor: "white",
          maxWidth: 800,
          mx: "auto",
        }}
      >
        <Typography
          variant="h5"
          gutterBottom
          sx={{
            fontWeight: 600,
            color: "primary.main",
            mb: 3,
          }}
        >
          Test Prompt
        </Typography>

        <form onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Enter your prompt"
            variant="outlined"
            value={prompt}
            onChange={(e) => setPrompt(e.target.value)}
            multiline
            rows={6}
            margin="normal"
            sx={{
              "& .MuiOutlinedInput-root": {
                borderRadius: 2,
                bgcolor: "grey.50",
                "&:hover fieldset": {
                  borderColor: "primary.main",
                },
              },
            }}
          />

          <Box sx={{ mt: 3, display: "flex", justifyContent: "flex-end" }}>
            <Button
              type="submit"
              variant="contained"
              disabled={loading || !prompt.trim()}
              startIcon={loading && <CircularProgress size={20} />}
              size="large"
              sx={{
                px: 2,
                boxShadow: "0 2px 4px rgba(25, 118, 210, 0.2)",
                "&:hover": {
                  boxShadow: "0 4px 8px rgba(25, 118, 210, 0.3)",
                },
              }}
            >
              {loading ? "Processing..." : "Generate"}
            </Button>
          </Box>
        </form>

        {error && (
          <Typography
            color="white"
            sx={{
              mt: 2,
              p: 2,
              bgcolor: "error.dark",
              borderRadius: 2,
            }}
          >
            {error}
          </Typography>
        )}

        {response && (
          <Fade in={true} timeout={500}>
            <Box
              sx={{
                mt: 4,
                p: 3,
                bgcolor: "grey.50",
                borderRadius: 2,
                border: "1px solid",
                borderColor: "grey.200",
              }}
            >
              <Typography variant="h6" gutterBottom sx={{ fontWeight: 500 }}>
                AI Response
              </Typography>
              <Typography sx={{ whiteSpace: "pre-wrap" }}>
                {response}
              </Typography>
            </Box>
          </Fade>
        )}
      </Paper>
    </Fade>
  );
};

export default AIPrompt;
