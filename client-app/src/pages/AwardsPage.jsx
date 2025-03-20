import React, { useState, useEffect, useCallback } from "react";
import {
  Typography,
  Box,
  Grid,
  CircularProgress,
  Alert,
  useTheme,
  Button,
} from "@mui/material"; // Added Button
import { useNavigate } from "react-router-dom";
import Leaderboard from "@/components/Leaderboard";
import PrizeCountdown from "@/components/PrizeCountdown";
import { fetchPlans, fetchNearestPrize, fetchWinner } from "@/services/api";
import { Skeleton } from "@mui/material";

const AwardsPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [clients, setClients] = useState([]);
  const [prize, setPrize] = useState({ prizeID: 0, prizeMoney: 0, date: null });
  const theme = useTheme();
  const [showAlert, setShowAlert] = useState(false);
  const [simulatedDaysRemaining, setSimulatedDaysRemaining] = useState(null); // Track simulated days

  // Load data on component mount (same as before)
  useEffect(() => {
    const loadData = async () => {
      setLoading(true);
      setError(null);
      try {
        const [plansData, prizeData] = await Promise.all([
          fetchPlans(),
          fetchNearestPrize(),
        ]);
        setClients(plansData);
        setPrize(prizeData);
        //Initialize simulatedDaysRemaining with actual days remaining:
        if (prizeData.date) {
          const today = new Date();
          const prizeDate = new Date(prizeData.date);
          const diffTime = prizeDate.getTime() - today.getTime();
          const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
          setSimulatedDaysRemaining(Math.max(0, diffDays));
        }
      } catch (err) {
        console.error("Error loading data:", err);
        console.log(err.response.data);
        setError(
          "حدث خطأ أثناء تحميل البيانات.  يرجى المحاولة مرة أخرى. تفاصيل المشكلة: " +
            err.response.data
        );
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  const handleSimulateDay = useCallback(() => {
    setSimulatedDaysRemaining((prev) => {
      const newRemaining = Math.max(0, prev - 1); // Decrement, but not below 0
      if (newRemaining === 0) {
        checkWinnerForSimulation(); // Call a separate function for simulation
      }
      return newRemaining;
    });
  }, []);

  const checkWinnerForSimulation = useCallback(async () => {
    setLoading(true);
    try {
      const winnerData = await fetchWinner();
      const loggedInClientId = parseInt(localStorage.getItem("clientID"));

      if (winnerData.planID === loggedInClientId) {
        setShowAlert(true); // Show alert only for the logged-in user
      }

      navigate("/app/winner-form", {
        state: {
          winner: {
            id: winnerData.planID,
            firstName: winnerData.name.split(" ")[0] || "",
            lastName: winnerData.name.split(" ")[1] || "",
            fullName: winnerData.name,
          },
          prize: {
            prizeID: prize.prizeID, //Keep using the *current* prize ID
            prizeMoney: winnerData.prizeMoney,
            prizeDate: winnerData.prizeDate,
          },
        },
      });
    } catch (err) {
      console.error("Error fetching winner during simulation:", err);
      setError("حدث خطأ أثناء محاكاة الفائز."); // Show simulation-specific error
    } finally {
      setLoading(false);
    }
  }, [navigate, prize.prizeID]);

  const checkWinner = useCallback(
    async (currentPrize) => {
      if (!currentPrize?.date) return;

      const today = new Date();
      const prizeDate = new Date(currentPrize.date);
      // Real Check winner
      if (prizeDate <= today && simulatedDaysRemaining === null) {
        try {
          const winnerData = await fetchWinner();
          // Get current user ID from localStorage
          const loggedInClientId = parseInt(localStorage.getItem("clientID"));

          // Show alert if current user is the winner
          if (winnerData.planID === loggedInClientId) {
            setShowAlert(true);
          }
          navigate("/app/winner-form", {
            state: {
              winner: {
                id: winnerData.planID,
                firstName: winnerData.name.split(" ")[0] || "",
                lastName: winnerData.name.split(" ")[1] || "",
                fullName: winnerData.name,
              },
              prize: {
                prizeID: prize.prizeID,
                prizeMoney: winnerData.prizeMoney,
                prizeDate: winnerData.prizeDate,
              },
            },
          });
        } catch (winnerErr) {
          console.error("Error fetching winner (in checkWinner):", winnerErr);
        }
      }
    },
    [navigate, prize.prizeID, simulatedDaysRemaining]
  );

  useEffect(() => {
    checkWinner(prize);
  }, [checkWinner, prize]);

  if (loading) {
    return (
      // Same loading screen as before
      <Box sx={{ p: 3 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          الجوائز / قائمة المتصدرين
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={12} md={6}>
            <Skeleton variant="rectangular" width="100%" height={120} />
          </Grid>
          <Grid item xs={12} md={6}>
            <Skeleton variant="rectangular" width="100%" height={120} />
          </Grid>
          <Grid item xs={12}>
            <Skeleton variant="rectangular" width="100%" height={200} />
          </Grid>
        </Grid>
      </Box>
    );
  }

  if (error) {
    // Same error handling as before
    return (
      <Box dir="rtl" sx={{ p: 3 }}>
        <Typography color="error" variant="h6">
          {error}
        </Typography>
        <Button variant="contained" onClick={() => window.location.reload()}>
          إعادة المحاولة
        </Button>
      </Box>
    );
  }

  return (
    <Box dir="rtl" sx={{ p: 3 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        الجوائز / قائمة المتصدرين
      </Typography>
      {showAlert && (
        <Alert
          severity="success"
          onClose={() => setShowAlert(false)}
          sx={{ mb: 2 }}
        >
          لقد فزت بجائزة! تحقق من إشعاراتك.
        </Alert>
      )}
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <PrizeCountdown
            prize={{
              ...prize,
              date:
                simulatedDaysRemaining !== null
                  ? addDays(new Date(), simulatedDaysRemaining)
                  : prize.date,
            }} // Use simulated date if available
            onSimulateDay={handleSimulateDay}
          />
        </Grid>
        <Grid item xs={12}>
          <Typography variant="h5" component="h2" gutterBottom>
            قائمة المتصدرين
          </Typography>
          <Leaderboard clients={clients} />
        </Grid>
      </Grid>
    </Box>
  );
};

// Helper function to add days to a date (for simulation)
function addDays(date, days) {
  const result = new Date(date);
  result.setDate(result.getDate() + days);
  return result;
}

export default AwardsPage;
