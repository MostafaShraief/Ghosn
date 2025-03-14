import React, { useState } from "react";
import {
  Box,
  TextField,
  Button,
  Paper,
  Typography,
  CircularProgress,
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
    <Paper sx={{ p: 3, minWidth: "100%", mx: "auto", mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        AI Assistant
      </Typography>

      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          label="Enter your prompt"
          variant="outlined"
          value={prompt}
          onChange={(e) => setPrompt(e.target.value)}
          multiline
          rows={4}
          margin="normal"
        />

        <Box sx={{ mt: 2, display: "flex", justifyContent: "flex-end" }}>
          <Button
            type="submit"
            variant="contained"
            disabled={loading || !prompt.trim()}
            startIcon={loading && <CircularProgress size={20} />}
          >
            {loading ? "Processing..." : "Send"}
          </Button>
        </Box>
      </form>

      {error && (
        <Typography color="error" sx={{ mt: 2 }}>
          {error}
        </Typography>
      )}

      {response && (
        <Box
          sx={{ mt: 3, p: 2, bgcolor: "rgba(0, 0, 0, 0.03)", borderRadius: 1 }}
        >
          <Typography variant="h6" gutterBottom>
            AI Response:
          </Typography>
          <Typography>{response}</Typography>
        </Box>
      )}
    </Paper>
  );
};

export default AIPrompt;
