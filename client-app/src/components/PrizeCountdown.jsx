import React, { useState, useEffect } from "react";
import {
  Typography,
  Box,
  LinearProgress,
  Grid,
  Paper,
  Tooltip,
  IconButton,
} from "@mui/material";
import { styled } from "@mui/system";
import UpdateIcon from "@mui/icons-material/Update";

const CountdownBox = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(3),
  borderRadius: theme.shape.borderRadius * 2,
  backgroundColor: theme.palette.background.default,
  boxShadow: theme.shadows[2],
  textAlign: "center",
}));

const PrizeCountdown = ({ prize, onSimulateDay }) => {
  const [daysRemaining, setDaysRemaining] = useState(0);
  const [competitionEnded, setCompetitionEnded] = useState(false);
  const [progress, setProgress] = useState(100); // LinearProgress value

  useEffect(() => {
    const calculateDaysRemaining = () => {
      if (prize.date) {
        const today = new Date();
        const prizeDate = new Date(prize.date);
        const diffTime = prizeDate.getTime() - today.getTime();
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        const remaining = Math.max(0, diffDays);
        setDaysRemaining(remaining);
        setCompetitionEnded(remaining <= 0);
        const totalDays = 30;
        setProgress(Math.max(0, (remaining / totalDays) * 100));
      }
    };

    calculateDaysRemaining();

    const intervalId = setInterval(calculateDaysRemaining, 1000 * 60); // Update every minute

    return () => clearInterval(intervalId);
  }, [prize.date]);

  const formattedDate = prize.date
    ? new Date(prize.date).toLocaleDateString("ar-SY")
    : "";

  return (
    <CountdownBox elevation={3}>
      <Grid container spacing={2} alignItems="center" justifyContent="center">
        <Grid item xs={12} sm={6}>
          <Typography variant="h6" component="h2" gutterBottom>
            الموعد النهائي
          </Typography>
          <Typography variant="h5" color="primary" gutterBottom>
            {daysRemaining} أيام متبقية
          </Typography>
          <Tooltip title="شريط تقدم تقديري بناءً على 30 يومًا">
            <LinearProgress variant="determinate" value={progress} />
          </Tooltip>
        </Grid>
        <Grid item xs={12} sm={6}>
          <Typography variant="h6" component="h2" gutterBottom>
            الجائزة الحالية
          </Typography>
          <Typography variant="h5" gutterBottom>
            ${prize.prizeMoney.toLocaleString("en-US")}
          </Typography>
          <Typography variant="body2">{formattedDate}</Typography>
        </Grid>
        {/*  Restore the button, but make sure it's visually clear it's for testing */}
        <Grid item xs={12}>
          <Tooltip title="محاكاة مرور يوم (للتجربة فقط)">
            <IconButton
              onClick={onSimulateDay}
              color="primary"
              disabled={competitionEnded}
            >
              <UpdateIcon /> محاكاة مرور يوم
            </IconButton>
          </Tooltip>
        </Grid>
      </Grid>
    </CountdownBox>
  );
};

export default PrizeCountdown;
