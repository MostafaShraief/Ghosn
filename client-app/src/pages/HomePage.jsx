import React, { useState, useEffect } from "react";
import {
  Container,
  Typography,
  Grid,
  Card,
  CardContent,
  Button,
  IconButton,
  Box,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Divider,
  CircularProgress,
} from "@mui/material";
import {
  Notifications as NotificationsIcon,
  Lightbulb as LightbulbIcon,
  AddCircle as AddCircleIcon,
  Assessment as AssessmentIcon,
  ChatBubble as ChatBubbleIcon,
  Event as EventIcon,
  WaterDrop,
} from "@mui/icons-material";
import {
  Timeline,
  TimelineItem,
  TimelineSeparator,
  TimelineConnector,
  TimelineContent,
  TimelineDot,
} from "@mui/lab";

function HomePage() {
  const [loading, setLoading] = useState(true);
  const [notifications, setNotifications] = useState([]);
  const [suggestions, setSuggestions] = useState([]);
  const [recentActivity, setRecentActivity] = useState([]);

  useEffect(() => {
    const timer = setTimeout(() => {
      setNotifications([
        { id: 1, text: "تذكير: سقاية المحصول الذي لديك.", icon: <WaterDrop /> },
        { id: 2, text: "تذكير: قم بري النباتات اليوم.", icon: <EventIcon /> },
      ]);
      setSuggestions([
        "تحقق من رطوبة التربة اليوم.",
        "قم بتسميد النباتات المغذية.",
        "تفقد النباتات بحثًا عن أي آفات.",
      ]);
      setRecentActivity([
        { time: "اليوم", event: "تم إنشاء تقرير جديد" },
        { time: "أمس", event: "تمت إضافة خطة زراعة" },
        { time: "منذ يومين", event: "تم تسجيل الدخول" },
      ]);
      setLoading(false);
    }, 500); 

    return () => clearTimeout(timer);
  }, []);

  if (loading) {
    return (
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "50vh",
        }}
      >
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Box
        sx={{
          background: "linear-gradient(to right, #43a047, #00c853)",
          color: "white",
          p: 4,
          mb: 4,
          borderRadius: 2,
          textAlign: "center",
        }}
      >
        <Typography variant="h4" component="h1" gutterBottom>
          مرحبا بك في صفحة الرئيسية!
        </Typography>
        <Typography variant="subtitle1">
          إدارة زراعتك بسهولة وفعالية.
        </Typography>
      </Box>

      <Grid container spacing={3}>
        {/* Notifications Panel */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <NotificationsIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">الإشعارات</Typography>
              </Box>
              <List>
                {notifications.map((notification) => (
                  <React.Fragment key={notification.id}>
                    <ListItem>
                      <ListItemIcon>{notification.icon}</ListItemIcon>
                      <ListItemText primary={notification.text} />
                    </ListItem>
                    <Divider />
                  </React.Fragment>
                ))}
              </List>
            </CardContent>
          </Card>
        </Grid>

        {/* Today's Suggestions */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <LightbulbIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">اقتراحات اليوم</Typography>
              </Box>
              {suggestions.map((suggestion, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 1 }}>
                  - {suggestion}
                </Typography>
              ))}
            </CardContent>
          </Card>
        </Grid>

        {/* Quick Actions */}
        <Grid item xs={12}>
          <Card elevation={3} sx={{ borderRadius: 2, p: 2 }}>
            <Typography variant="h6" gutterBottom>
              إجراءات سريعة
            </Typography>
            <Grid container spacing={2}>
              <Grid item>
                <Button
                  variant="contained"
                  color="primary"
                  startIcon={<AddCircleIcon sx={{ ml: 1 }} />}
                  href="/planting-form"
                >
                  إضافة خطة زراعة
                </Button>
              </Grid>

              <Grid item>
                <Button
                  variant="outlined"
                  color="primary"
                  startIcon={<ChatBubbleIcon sx={{ ml: 1 }} />} 
                  href="/chat"
                >
                  الدردشة
                </Button>
              </Grid>
              <Grid item>
                <Button
                  variant="outlined"
                  color="primary"
                  startIcon={<AssessmentIcon sx={{ ml: 1 }} />} 
                  href="/reports"
                >
                  عرض التقارير
                </Button>
              </Grid>
            </Grid>
          </Card>
        </Grid>

        {/* Recent Activity */}
        <Grid item xs={12}>
          <Card elevation={3} sx={{ borderRadius: 2, p: 2 }}>
            <Typography variant="h6" gutterBottom>
              النشاط الأخير
            </Typography>
            <Timeline position="alternate">
              {recentActivity.map((activity, index) => (
                <TimelineItem key={index}>
                  <TimelineSeparator>
                    <TimelineDot />
                    {index < recentActivity.length - 1 && <TimelineConnector />}
                  </TimelineSeparator>
                  <TimelineContent>
                    <Typography variant="body2" color="textSecondary">
                      {activity.time}
                    </Typography>
                    <Typography>{activity.event}</Typography>
                  </TimelineContent>
                </TimelineItem>
              ))}
            </Timeline>
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
}

export default HomePage;
