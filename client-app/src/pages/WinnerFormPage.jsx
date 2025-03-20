import React, { useState, useEffect } from "react";
import {
  Typography,
  Box,
  Card,
  CardContent,
  Grid,
  Avatar,
  CircularProgress,
  Button,
  Paper,
} from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { styled } from "@mui/system";
import { fetchWinner } from "@/services/api";

const WinnerCard = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(4),
  borderRadius: theme.shape.borderRadius * 2,
  backgroundColor: theme.palette.background.default,
  boxShadow: theme.shadows[3],
}));
const WinnerFormPage = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [winnerData, setWinnerData] = useState(null);

  useEffect(() => {
    const { winner, prize } = location.state || {};

    if (winner && prize) {
      setWinnerData({ winner, prize });
      return;
    }

    const getWinner = async () => {
      setLoading(true);
      setError(null);
      try {
        const winnerData = await fetchWinner();
        const winnerFromApi = {
          id: winnerData.planID,
          firstName: winnerData.name.split(" ")[0] || "",
          lastName: winnerData.name.split(" ")[1] || "",
          fullName: winnerData.name,
        };

        const prizeFromApi = {
          prizeMoney: winnerData.prizeMoney,
          prizeDate: winnerData.prizeDate,
        };
        setWinnerData({ winner: winnerFromApi, prize: prizeFromApi });
      } catch (err) {
        console.error("Error fetching winner:", err);
        setError("Failed to load winner information");
      } finally {
        setLoading(false);
      }
    };

    getWinner(); // Fetch if not provided
  }, [location.state]);

  if (loading) {
    return (
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
        }}
      >
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Box dir="rtl" sx={{ p: 3 }}>
        <Typography color="error" variant="h6">
          {error}
        </Typography>
        <Button variant="contained" onClick={() => navigate("/app/awards")}>
          العودة إلى صفحة الجوائز
        </Button>
      </Box>
    );
  }

  if (!winnerData) {
    return (
      <Box dir="rtl" sx={{ p: 3 }}>
        <Typography variant="h4" component="h1">
          نموذج الفائز
        </Typography>
        <Typography>لا توجد بيانات فائز متاحة.</Typography>
        <Button
          variant="contained"
          onClick={() => navigate("/app/awards")}
          sx={{ mt: 2 }}
        >
          العودة إلى صفحة الجوائز
        </Button>
      </Box>
    );
  }

  const { winner, prize } = winnerData;
  const formattedDate = prize.prizeDate
    ? new Date(prize.prizeDate).toLocaleDateString("ar-SA")
    : "";

  return (
    <Box dir="rtl" sx={{ p: 3, display: "flex", justifyContent: "center" }}>
      <WinnerCard elevation={3} sx={{ maxWidth: 600, width: "100%" }}>
        <Grid container spacing={2} alignItems="center" justifyContent="center">
          <Grid item>
            <Avatar sx={{ bgcolor: "green", width: 64, height: 64 }}>
              <CheckCircleIcon sx={{ fontSize: 48 }} />
            </Avatar>
          </Grid>
          <Grid item xs={12} sm>
            <Typography
              variant="h4"
              component="h1"
              gutterBottom
              textAlign="center"
            >
              تهانينا!
            </Typography>
          </Grid>
        </Grid>
        <Box mt={3}>
          <Typography variant="h6" component="h2">
            الفائز:{" "}
            {winner.fullName || `${winner.firstName} ${winner.lastName}`}
          </Typography>
          <Typography variant="body1">
            قيمة الجائزة: ${prize.prizeMoney.toLocaleString("en-US")}
          </Typography>
          <Typography variant="body1">
            تاريخ الجائزة: {formattedDate}
          </Typography>
        </Box>
        <Box mt={3} sx={{ display: "flex", justifyContent: "center" }}>
          <Button variant="contained" onClick={() => navigate("/app/awards")}>
            العودة إلى صفحة الجوائز
          </Button>
        </Box>
      </WinnerCard>
    </Box>
  );
};

export default WinnerFormPage;
