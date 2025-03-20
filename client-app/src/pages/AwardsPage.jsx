// client-app/src/pages/AwardsPage.jsx
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
  LinearProgress, // Optional: for a progress bar
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import MilitaryTechIcon from "@mui/icons-material/MilitaryTech";

const AwardsPage = () => {
  const navigate = useNavigate();
  const [daysRemaining, setDaysRemaining] = useState(10);
  const [competitionEnded, setCompetitionEnded] = useState(false);
  const [sortedClients, setSortedClients] = useState([]);
  const [clients, setClients] = useState([
    { id: 1, firstName: "Abubakr", lastName: "Alsheikh", areaSize: 300 },
    { id: 2, firstName: "Mustafa", lastName: "Sharif", areaSize: 299 },
    { id: 3, firstName: "Fatima", lastName: "Omar", areaSize: 200 },
    { id: 4, firstName: "Khalid", lastName: "Yusuf", areaSize: 150 },
    { id: 5, firstName: "Layla", lastName: "Hassan", areaSize: 100 },
  ]);

  const prize = {
    prizePrice: 500,
    prizeDate: "2024-05-30",
  };

  const checkWinner = useCallback(() => {
    if (competitionEnded && sortedClients.length > 0) {
      const winner = sortedClients[0];
      const loggedInClientId = parseInt(localStorage.getItem("clientID"));

      if (winner && winner.id === loggedInClientId) {
        alert(`لقد فزت بجائزة قيمتها ${prize.prizePrice}! تحقق من إشعاراتك.`);
      }
      navigate("/app/winner-form", { state: { winner, prize } });
    }
  }, [competitionEnded, sortedClients, navigate, prize]);

  useEffect(() => {
    const sorted = [...clients].sort((a, b) => b.areaSize - a.areaSize);
    setSortedClients(sorted);
  }, [clients]);

  useEffect(() => {
    const timer = setInterval(() => {
      setDaysRemaining((prevDays) => {
        if (prevDays > 0) {
          return prevDays - 1;
        } else {
          clearInterval(timer);
          setCompetitionEnded(true);
          return 0;
        }
      });
    }, 5000);

    return () => clearInterval(timer);
  }, []);

  useEffect(() => {
    checkWinner();
  }, [competitionEnded, sortedClients, checkWinner]);

  const handleSimulateDay = () => {
    setDaysRemaining((prev) => Math.max(0, prev - 1));
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
                value={(daysRemaining / 10) * 100}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <Typography variant="h6" component="h2">
                الجائزة الحالية
              </Typography>
              <Typography variant="h5">$ {prize.prizePrice}</Typography>
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
