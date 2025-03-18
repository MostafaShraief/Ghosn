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
        alignItems: "center",
        height: "100vh", // لجعل النموذج في وسط الشاشة
        backgroundImage: "url('https://via.placeholder.com/1920x1080')", // صورة خلفية للصفحة (اختياري)
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
          backgroundImage: "url('')", // صورة خلفية للصندوق
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundColor: "rgba(255, 255, 255, 0.8)", // شفافية الخلفية
          backdropFilter: "blur(5px)", // تأثير ضبابي
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
            sx={{ mb: 3, fontSize: "1.2rem" }} // زيادة المسافة بين الحقول
          />

          <TextField
            label="كلمة المرور"
            variant="outlined"
            type="password"
            fullWidth
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            sx={{ mb: 3, fontSize: "1.2rem" }} // زيادة المسافة بين الحقول
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ mt: 3, py: 2, fontSize: "1.2rem" }} // زيادة المسافة فوق الزر
          >
            تسجيل الدخول
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default LoginForm;