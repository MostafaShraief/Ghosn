import React, { useState, useEffect } from "react";
import {
  Container,
  Typography,
  Grid,
  Card,
  CardContent,
  Button,
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
  const [tips, setTips] = useState([]); // حالة لتخزين النصائح

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
      setTips([
        "استخدم الأسمدة العضوية لتحسين جودة التربة.",
        "قم بري النباتات في الصباح الباكر.",
        "تأكد من تعرض النباتات لأشعة الشمس الكافية.",
      ]); // نصائح وهمية
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
          مرحبا بك في الصفحة الرئيسية!
        </Typography>
        <Typography variant="subtitle1">
          إدارة زراعتك بسهولة وفعالية.
        </Typography>
      </Box>

      <Grid container spacing={3}>
        {/* الإشعارات */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2, height: "100%" }}>
            <CardContent sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <NotificationsIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">الإشعارات</Typography>
              </Box>
              <Box sx={{ flexGrow: 1 }}>
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
              </Box>
            </CardContent>
          </Card>
        </Grid>

        {/* اقتراحات اليوم */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2, height: "100%" }}>
            <CardContent sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <LightbulbIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">اقتراحات اليوم</Typography>
              </Box>
              <Box sx={{ flexGrow: 1 }}>
                {suggestions.map((suggestion, index) => (
                  <Typography key={index} variant="body1" sx={{ mb: 1 }}>
                    - {suggestion}
                  </Typography>
                ))}
              </Box>
            </CardContent>
          </Card>
        </Grid>

        {/* الإجراءات السريعة */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2, height: "100%" }}>
            <CardContent sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <AddCircleIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">إجراءات سريعة</Typography>
              </Box>
              <Box sx={{ flexGrow: 1 }}>
                <Grid container spacing={2}>
                  <Grid item xs={12}>
                    <Button
                      variant="contained"
                      color="primary"
                      fullWidth
                      startIcon={<AddCircleIcon sx={{ ml: 1 }} />}
                      href="/planting-form"
                    >
                      إضافة خطة زراعة
                    </Button>
                  </Grid>

                  <Grid item xs={12}>
                    <Button
                      variant="outlined"
                      color="primary"
                      fullWidth
                      startIcon={<ChatBubbleIcon sx={{ ml: 1 }} />}
                      href="/chat"
                    >
                      الدردشة
                    </Button>
                  </Grid>

                  <Grid item xs={12}>
                    <Button
                      variant="outlined"
                      color="primary"
                      fullWidth
                      startIcon={<AssessmentIcon sx={{ ml: 1 }} />}
                      href="/reports"
                    >
                      عرض التقارير
                    </Button>
                  </Grid>
                </Grid>
              </Box>
            </CardContent>
          </Card>
        </Grid>

        {/* النصائح */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2, height: "100%" }}>
            <CardContent sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <LightbulbIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6">نصائح</Typography>
              </Box>
              <Box sx={{ flexGrow: 1 }}>
                {tips.map((tip, index) => (
                  <Typography key={index} variant="body1" sx={{ mb: 1 }}>
                    - {tip}
                  </Typography>
                ))}
              </Box>
            </CardContent>
          </Card>
        </Grid>

        {/* النشاط الأخير */}
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