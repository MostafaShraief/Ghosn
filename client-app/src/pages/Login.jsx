// client-app/src/pages/Login.jsx
import React, { useState } from "react";
import {
  TextField,
  Button,
  Box,
  Typography,
  Paper,
  Container,
  Grid,
  Link,
  Alert,
  CircularProgress,
} from "@mui/material";
import { login } from "@/services/api";
import { useNavigate } from "react-router-dom";
import { LockOutlined, AccountCircle, Badge } from "@mui/icons-material"; // Import icons

function LoginForm() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false); // Loading state
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!username || !password || !firstName || !lastName) {
      setError("يرجى ملء جميع الحقول");
      return;
    }

    setLoading(true); // Start loading
    setError(""); // Clear previous errors

    try {
      const responseData = await login(username, password, firstName, lastName);

      localStorage.setItem("clientID", responseData.clientID);
      localStorage.setItem("username", username);
      localStorage.setItem("firstName", firstName); // Store first name
      localStorage.setItem("lastName", lastName); // Store last name
      navigate("/");
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false); // Stop loading, regardless of success/failure
    }
  };

  return (
    <Container
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        background: "linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%)", // Modern gradient
      }}
    >
      <Paper
        elevation={24} // Increased elevation for a more modern look
        sx={{
          padding: 4, // Reduced padding
          width: "100%",
          maxWidth: "450px", // Adjusted max width
          textAlign: "center",
          borderRadius: "16px", // Rounded corners
          backgroundColor: "rgba(255, 255, 255, 0.95)", // Slightly more opaque
          backdropFilter: "blur(10px)", // Increased blur
        }}
      >
        <AccountCircle sx={{ fontSize: 60, color: "primary.main", mb: 2 }} />{" "}
        {/* Larger icon */}
        <Typography variant="h5" component="h1" gutterBottom>
          تسجيل الدخول
        </Typography>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                label="الاسم الأول"
                variant="outlined"
                fullWidth
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                InputProps={{
                  startAdornment: <Badge color="primary" sx={{ mr: 1 }} />,
                }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                label="اسم العائلة"
                variant="outlined"
                fullWidth
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                InputProps={{
                  startAdornment: <Badge color="primary" sx={{ mr: 1 }} />,
                }}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="اسم المستخدم"
                variant="outlined"
                fullWidth
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                InputProps={{
                  startAdornment: (
                    <AccountCircle color="primary" sx={{ mr: 1 }} />
                  ),
                }}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="كلمة المرور"
                variant="outlined"
                type="password"
                fullWidth
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                InputProps={{
                  startAdornment: (
                    <LockOutlined color="primary" sx={{ mr: 1 }} />
                  ),
                }}
              />
            </Grid>
          </Grid>

          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            disabled={loading} // Disable button while loading
            sx={{ mt: 3, py: 1.5, fontSize: "1.1rem", borderRadius: "8px" }}
          >
            {loading ? (
              <CircularProgress size={24} color="inherit" />
            ) : (
              "تسجيل الدخول"
            )}
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default LoginForm;
