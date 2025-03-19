import React from "react";
import { Box, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const DonorHomePage = () => {
  const navigate = useNavigate();

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom>
        مرحباً بكم في قسم المتبرعين
      </Typography>
      <Box sx={{ display: "flex", gap: 2, mt: 4 }}>
        <Button
          variant="contained"
          size="large"
          onClick={() => navigate("/donor/create-prize")}
          sx={{ flex: 1 }}
        >
          إنشاء جائزة
        </Button>
        <Button
          variant="contained"
          size="large"
          onClick={() => navigate("/donor/direct-support")}
          sx={{ flex: 1 }}
        >
          الدعم المباشر
        </Button>
      </Box>
    </Box>
  );
};

export default DonorHomePage;
