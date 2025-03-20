// client-app/src/pages/DonorHomePage.jsx
import React from "react";
import { Box, Typography, Button, Grid, Paper, Icon } from "@mui/material"; // Import Grid, Paper, Icon
import { useNavigate } from "react-router-dom";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents"; // Prize Icon
import VolunteerActivismIcon from "@mui/icons-material/VolunteerActivism"; // Direct Support Icon
import { useTheme } from "@mui/material/styles"; // For theme-aware styling

const DonorHomePage = () => {
  const navigate = useNavigate();
  const theme = useTheme(); // Get theme for styling

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom component="h1">
        {" "}
        {/* Make it h1 for page title */}
        مرحباً بكم في قسم المتبرعين
      </Typography>

      <Typography variant="body1" paragraph sx={{ color: "text.secondary" }}>
        {" "}
        {/* Added introductory paragraph */}
        شكراً لمساهمتكم القيمة في دعم مجتمع المزارعين. من خلال هذا القسم، يمكنكم
        إنشاء جوائز لتحفيز المزارعين وتقديم دعم مباشر للمشاريع الزراعية.
        مساهمتكم تحدث فرقاً حقيقياً في تعزيز الاستدامة الزراعية وتمكين المجتمعات
        المحلية.
      </Typography>

      <Grid container spacing={3} mt={3}>
        {" "}
        {/* Use Grid for layout */}
        <Grid item xs={12} md={6}>
          {" "}
          {/* Card for Create Prize */}
          <Paper
            elevation={3}
            sx={{
              p: 3,
              textAlign: "center",
              height: "100%",
              display: "flex",
              flexDirection: "column",
              justifyContent: "space-between",
            }}
          >
            {" "}
            {/* Paper for card effect */}
            <Box>
              <Icon
                sx={{ fontSize: 60, color: theme.palette.primary.main, mb: 1 }}
              >
                {" "}
                {/* Icon */}
                <EmojiEventsIcon sx={{ fontSize: 60 }} />
              </Icon>
              <Typography variant="h6" gutterBottom>
                إنشاء جائزة جديدة
              </Typography>
              <Typography variant="body2" color="text.secondary" paragraph>
                أنشئ جائزة لتحفيز المزارعين وتشجيع التميز في مشاريعهم الزراعية.
                يمكنك تحديد قيمة الجائزة وتاريخ منحها.
              </Typography>
            </Box>
            <Button
              variant="contained"
              size="large"
              onClick={() => navigate("/donor/create-prize")}
              sx={{ mt: 2 }}
            >
              إنشاء جائزة
            </Button>
          </Paper>
        </Grid>
        <Grid item xs={12} md={6}>
          {" "}
          {/* Card for Direct Support */}
          <Paper
            elevation={3}
            sx={{
              p: 3,
              textAlign: "center",
              height: "100%",
              display: "flex",
              flexDirection: "column",
              justifyContent: "space-between",
            }}
          >
            {" "}
            {/* Paper for card effect */}
            <Box>
              <Icon
                sx={{ fontSize: 60, color: theme.palette.primary.main, mb: 1 }}
              >
                {" "}
                {/* Icon */}
                <VolunteerActivismIcon sx={{ fontSize: 60 }} />
              </Icon>
              <Typography variant="h6" gutterBottom>
                الدعم المباشر
              </Typography>
              <Typography variant="body2" color="text.secondary" paragraph>
                قدم دعماً مباشراً للمزارعين إما بتوفير الأدوات الزراعية أو الدعم
                المالي للمشاريع الزراعية القائمة.
              </Typography>
            </Box>
            <Button
              variant="contained"
              size="large"
              onClick={() => navigate("/donor/direct-support")}
              sx={{ mt: 2 }}
            >
              الدعم المباشر
            </Button>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
};

export default DonorHomePage;
