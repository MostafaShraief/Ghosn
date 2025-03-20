// client-app/src/pages/WinnerFormPage.jsx
import React from "react";
import {
  Typography,
  Box,
  Card,
  CardContent,
  Grid,
  Avatar,
} from "@mui/material";
import { useLocation } from "react-router-dom";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";

const WinnerFormPage = () => {
  const location = useLocation();
  const { winner, prize } = location.state || {};

  if (!winner || !prize) {
    return (
      <Box dir="rtl" sx={{ p: 3 }}>
        <Typography variant="h4" component="h1">
          نموذج الفائز
        </Typography>
        <Typography>لا توجد بيانات فائز متاحة.</Typography>
      </Box>
    );
  }

  return (
    <Box dir="rtl" sx={{ p: 3 }}>
      <Card>
        <CardContent>
          <Grid container spacing={2} alignItems="center">
            <Grid item>
              <Avatar sx={{ bgcolor: "green", width: 64, height: 64 }}>
                <CheckCircleIcon sx={{ fontSize: 48 }} />
              </Avatar>
            </Grid>
            <Grid item xs>
              <Typography variant="h4" component="h1" gutterBottom>
                تهانينا!
              </Typography>
            </Grid>
          </Grid>

          <Box mt={3}>
            <Typography variant="h6" component="h2">
              الفائز: {winner.firstName} {winner.lastName}
            </Typography>
            <Typography variant="body1">
              قيمة الجائزة: $ {prize.prizePrice}
            </Typography>
            <Typography variant="body1">
              تاريخ الجائزة: {prize.prizeDate}
            </Typography>
          </Box>
        </CardContent>
      </Card>
    </Box>
  );
};

export default WinnerFormPage;
