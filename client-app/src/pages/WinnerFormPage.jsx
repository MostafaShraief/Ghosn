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
} from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";

const WinnerFormPage = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [winnerData, setWinnerData] = useState(null);

  // Get winner data from location state or fetch it if not available
  useEffect(() => {
    const { winner, prize } = location.state || {};

    // If data is passed through navigation state, use it
    if (winner && prize) {
      setWinnerData({ winner, prize });
      return;
    }

    // Otherwise, fetch the data from API
    const fetchWinner = async () => {
      setLoading(true);
      try {
        // Create winner and prize objects from API response
        const winnerFromApi = {
          id: winnerData.winner.planID,
          firstName: winnerData.winner.name.split(" ")[0] || "",
          lastName: winnerData.winner.name.split(" ")[1] || "",
          fullName: winnerData.winner.name,
        };

        const prizeFromApi = {
          prizeMoney: winnerData.prize.prizeMoney,
          prizeDate: winnerData.prize.prizeDate,
        };

        setWinnerData({
          winner: winnerFromApi,
          prize: prizeFromApi,
        });
      } catch (err) {
        console.error("Error fetching winner:", err);
        setError("Failed to load winner information");
      } finally {
        setLoading(false);
      }
    };

    fetchWinner();
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

  // Format date to locale string
  const formatDate = (dateString) => {
    try {
      return new Date(dateString).toLocaleDateString("ar-SA");
    } catch (e) {
      return dateString;
    }
  };

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
              الفائز:{" "}
              {winner.fullName || `${winner.firstName} ${winner.lastName}`}
            </Typography>
            <Typography variant="body1">
              قيمة الجائزة: $ {prize.prizeMoney}
            </Typography>
            <Typography variant="body1">
              تاريخ الجائزة: {formatDate(prize.prizeDate)}
            </Typography>
          </Box>
          <Box mt={3} sx={{ display: "flex", justifyContent: "center" }}>
            <Button variant="contained" onClick={() => navigate("/app/awards")}>
              العودة إلى صفحة الجوائز
            </Button>
          </Box>
        </CardContent>
      </Card>
    </Box>
  );
};

export default WinnerFormPage;
