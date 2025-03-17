import React, { useState } from "react";
import { TextField, Button, Box, Typography, Paper, Container } from "@mui/material";

function LoginForm() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!username || !password) {
      setError("يرجى ملء جميع الحقول");
      return;
    }
    // هنا يمكنك إضافة منطق إرسال البيانات إلى الخادم
    console.log("تم إرسال البيانات:", { username, password });
    setError("");
  };

  return (
    <Container
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "flex-start", // رفع الصندوق لأعلى
        height: "100vh",
        paddingTop: 4, // إضافة مسافة من الأعلى (اختياري)
      }}
    >
      <Paper
        elevation={3}
        sx={{
          padding: 8,
          width: "100%",
          maxWidth: "600px",
          textAlign: "center",
          marginTop: 8, // رفع الصندوق لأعلى
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
            sx={{ mb: 4, fontSize: "1.2rem" }} // زيادة المسافة بين الحقول وتكبير الخط
          />

          <TextField
            label="كلمة المرور"
            variant="outlined"
            type="password"
            fullWidth
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            sx={{ mb: 4, fontSize: "1.2rem" }} // زيادة المسافة بين الحقول وتكبير الخط
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ mt: 4, py: 2, fontSize: "1.2rem" }} // زيادة حجم الزر وتكبير الخط
          >
            تسجيل الدخول
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default LoginForm;