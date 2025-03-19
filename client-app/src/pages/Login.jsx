import React, { useState } from "react";
import {
  TextField,
  Button,
  Box,
  Typography,
  Paper,
  Container,
} from "@mui/material";
import { login } from "@/services/api";
import { useNavigate } from "react-router-dom";

function LoginForm() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!username || !password) {
      setError("يرجى ملء جميع الحقول");
      return;
    }

    try {
      const responseData = await login(username, password);

      localStorage.setItem("clientID", responseData.clientID);
      localStorage.setItem("username", username);
      navigate("/");
      setError("");
    } catch (error) {
      setError(error.message);
    }
  };

  return (
    <Container
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <Paper
        elevation={3}
        sx={{
          padding: 8,
          width: "100%",
          maxWidth: "600px",
          textAlign: "center",
          backgroundImage: "url('')",
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundColor: "rgba(255, 255, 255, 0.8)",
          backdropFilter: "blur(5px)",
        }}
      >
        <Typography variant="h4" component="h1" gutterBottom>
          تسجيل الدخول
        </Typography>

        {error && (
          <Typography color="error" gutterBottom>
            {error}
          </Typography>
        )}

        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
          <TextField
            label="اسم المستخدم"
            variant="outlined"
            fullWidth
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            sx={{ mb: 3, fontSize: "1.2rem" }}
          />

          <TextField
            label="كلمة المرور"
            variant="outlined"
            type="password"
            fullWidth
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            sx={{ mb: 3, fontSize: "1.2rem" }}
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ mt: 3, py: 2, fontSize: "1.2rem" }}
          >
            تسجيل الدخول
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default LoginForm;
