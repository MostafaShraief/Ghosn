import React, { useState, useEffect, useCallback } from "react";
import {
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Typography,
  Box,
  Button,
  Avatar,
  Card,
  CardContent,
  Grid,
  LinearProgress,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import MilitaryTechIcon from "@mui/icons-material/MilitaryTech";
import api from "@/services/api";

const AwardsPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [daysRemaining, setDaysRemaining] = useState(0);
  const [competitionEnded, setCompetitionEnded] = useState(false);
  const [sortedClients, setSortedClients] = useState([]);
  const [prize, setPrize] = useState({
    prizeID: 0,
    prizeMoney: 0,
    date: null,
  });

  // Fetch the plans ordered by area
  const fetchPlans = async () => {
    try {
      const response = await api.get("/api/Ghosn/Plans/OrderByArea");
      // Transform the data to match your client structure
      const clientsData = response.data.map((plan, index) => ({
        id: index + 1, // You might need to adjust this if you have actual client IDs
        firstName: plan.name.split(" ")[0] || "Client", // Assuming name format is "FirstName LastName"
        lastName: plan.name.split(" ")[1] || `#${index + 1}`,
        areaSize: plan.areaSize,
      }));
      setSortedClients(clientsData);
    } catch (err) {
      console.error("Error fetching plans:", err);
      setError("Failed to load leaderboard data");
    }
  };

  // Fetch the nearest prize
  const fetchPrize = async () => {
    try {
      const response = await api.get("/api/Ghosn/Prizes/Nearest");
      setPrize({
        prizeID: response.data.prizeID,
        prizeMoney: response.data.prizeMoney,
        date: new Date(response.data.date),
      });

      // Calculate days remaining
      if (response.data.date) {
        const today = new Date();
        const prizeDate = new Date(response.data.date);
        const diffTime = prizeDate - today;
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        setDaysRemaining(Math.max(0, diffDays));
        setCompetitionEnded(diffDays <= 0);
      }
    } catch (err) {
      console.error("Error fetching prize:", err);
      setError("Failed to load prize information");
    }
  };

  // Fetch winner when competition ends
  const fetchWinner = async () => {
    try {
      const response = await api.get("/api/Ghosn/Plan/ProduceWinner");

      // Get current user ID from localStorage
      const loggedInClientId = parseInt(localStorage.getItem("clientID"));

      // Show alert if current user is the winner
      if (response.data.planID === loggedInClientId) {
        alert(
          `لقد فزت بجائزة قيمتها ${response.data.prizeMoney}! تحقق من إشعاراتك.`
        );
      }

      // Create winner object from API response
      const winner = {
        id: response.data.planID,
        firstName: response.data.name.split(" ")[0] || "",
        lastName: response.data.name.split(" ")[1] || "",
        fullName: response.data.name,
      };

      // Create prize object from API response
      const prizeData = {
        prizeID: prize.prizeID,
        prizeMoney: response.data.prizeMoney,
        prizeDate: response.data.prizeDate,
      };

      // Navigate to winner page with data
      navigate("/app/winner-form", {
        state: {
          winner,
          prize: prizeData,
        },
      });
    } catch (err) {
      console.error("Error fetching winner:", err);
      // Don't set error state as this might be an optional operation
    }
  };

  // Load data on component mount
  useEffect(() => {
    const loadData = async () => {
      setLoading(true);
      try {
        await Promise.all([fetchPlans(), fetchPrize()]);
      } catch (err) {
        console.error("Error loading data:", err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  // Check if competition has ended and fetch winner
  useEffect(() => {
    if (competitionEnded) {
      fetchWinner();
    }
  }, [competitionEnded]);

  // This is for demonstration only - in a real app, you wouldn't simulate days passing
  const handleSimulateDay = () => {
    setDaysRemaining((prev) => Math.max(0, prev - 1));
    if (daysRemaining <= 1) {
      setCompetitionEnded(true);
    }
  };

  const getRankingColor = (rank) => {
    if (rank === 0) return "gold";
    if (rank === 1) return "silver";
    if (rank === 2) return "#cd7f32"; // Bronze
    return "transparent"; // Default color
  };

  const getRankingIcon = (rank) => {
    if (rank < 3) return <MilitaryTechIcon />;
    return null;
  };

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
      <Box sx={{ p: 3 }}>
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

      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Grid container spacing={2} alignItems="center">
            <Grid item xs={12} sm={6}>
              <Typography variant="h6" component="h2">
                الموعد النهائي
              </Typography>
              <Typography variant="h5" color="primary">
                {daysRemaining} أيام متبقية
              </Typography>
              {/* Optional: Linear Progress Bar */}
              <LinearProgress
                variant="determinate"
                value={(daysRemaining / 30) * 100} // Assuming 30 days is the maximum
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <Typography variant="h6" component="h2">
                الجائزة الحالية
              </Typography>
              <Typography variant="h5">$ {prize.prizeMoney}</Typography>
              {prize.date && (
                <Typography variant="body2">
                  {new Date(prize.date).toLocaleDateString("ar-SY")}
                </Typography>
              )}
            </Grid>
          </Grid>
          <Box mt={2}>
            <Button variant="contained" onClick={handleSimulateDay}>
              محاكاة مرور يوم
            </Button>
          </Box>
        </CardContent>
      </Card>

      <Typography variant="h5" component="h2" gutterBottom>
        قائمة المتصدرين
      </Typography>

      <List>
        {sortedClients.map((client, index) => (
          <Card
            key={client.id}
            sx={{
              mb: 2,
              bgcolor: getRankingColor(index),
              borderRadius: 2,
              boxShadow: index < 3 ? 3 : 1, // Add more shadow for top 3
              border: index < 3 ? "2px solid" : "none",
            }}
          >
            <CardContent>
              <ListItem disableGutters>
                <ListItemAvatar>
                  <Avatar
                    sx={{
                      bgcolor:
                        getRankingColor(index) !== "transparent"
                          ? getRankingColor(index)
                          : "grey.300",
                    }}
                  >
                    {client.firstName.charAt(0)}
                    {client.lastName.charAt(0)}
                  </Avatar>
                </ListItemAvatar>
                <ListItemText
                  primary={`${client.firstName} ${client.lastName}`}
                  secondary={`المساحة: ${client.areaSize} متر مربع`}
                  primaryTypographyProps={{
                    variant: "h6",
                    fontWeight: index < 3 ? 700 : 400,
                  }}
                />
                <Box sx={{ display: "flex", alignItems: "center" }}>
                  {getRankingIcon(index)}
                  <Typography variant="h6" sx={{ ml: 1 }}>
                    {index + 1}
                  </Typography>
                </Box>
              </ListItem>
            </CardContent>
          </Card>
        ))}
      </List>
    </Box>
  );
};

export default AwardsPage;
