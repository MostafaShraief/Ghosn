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
import { api, getNotifications } from "@/services/api";
function HomePage() {
  const [loading, setLoading] = useState(true);
  const [notifications, setNotifications] = useState([]);
  const [suggestions, setSuggestions] = useState([]);
  const [tips, setTips] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch Notifications
        const notificationsResponse = await getNotifications();
        // Get the last three notifications, and map the icons
        const lastThreeNotifications = notificationsResponse
          .slice(-3)
          .map((notification) => ({
            id: notification.notificationID,
            text: notification.body,
            title: notification.title, //added title
            date: notification.dateAndTime, // added date
            icon: <EventIcon />, //  Default icon, you might want a mapping based on notification type
          }));
        setNotifications(lastThreeNotifications);

        // Fetch Suggestions
        const suggestionsResponse = await api.get("/api/Ghosn/Suggestion");
        setSuggestions(
          suggestionsResponse.data.res.split("،").map((item) => item.trim())
        );

        // Fetch Tips
        const tipsResponse = await api.get("/api/Ghosn/Tip");
        setTips(tipsResponse.data.res.split("،").map((item) => item.trim()));

        setLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
        //  Handle errors appropriately, e.g., show an error message to the user
        setLoading(false); // Ensure loading is set to false even on error
      }
    };

    fetchData();
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
        <Typography
          variant="h4"
          component="h1"
          gutterBottom
          sx={{ fontWeight: 700 }}
        >
          مرحبا بك في الصفحة الرئيسية!
        </Typography>
        <Typography variant="subtitle1" sx={{ fontWeight: 500 }}>
          إدارة زراعتك بسهولة وفعالية.
        </Typography>
      </Box>

      <Grid container spacing={3}>
        {/* الإشعارات */}
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2, height: "100%" }}>
            <CardContent
              sx={{ display: "flex", flexDirection: "column", height: "100%" }}
            >
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <NotificationsIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" sx={{ fontWeight: 700 }}>
                  الإشعارات
                </Typography>
              </Box>
              <Box sx={{ flexGrow: 1 }}>
                <List>
                  {notifications.map((notification) => (
                    <React.Fragment key={notification.id}>
                      <ListItem>
                        <ListItemIcon>{notification.icon}</ListItemIcon>
                        <ListItemText
                          primary={notification.text}
                          secondary={
                            <>
                              <Typography
                                component="span"
                                variant="body2"
                                color="textPrimary"
                              >
                                {notification.title}
                              </Typography>
                              {` — ${new Date(
                                notification.date
                              ).toLocaleString()}`}
                            </>
                          }
                        />
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
            <CardContent
              sx={{ display: "flex", flexDirection: "column", height: "100%" }}
            >
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <LightbulbIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" sx={{ fontWeight: 700 }}>
                  اقتراحات اليوم
                </Typography>
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
            <CardContent
              sx={{ display: "flex", flexDirection: "column", height: "100%" }}
            >
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <AddCircleIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" sx={{ fontWeight: 700 }}>
                  إجراءات سريعة
                </Typography>
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
            <CardContent
              sx={{ display: "flex", flexDirection: "column", height: "100%" }}
            >
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <LightbulbIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" sx={{ fontWeight: 700 }}>
                  نصائح
                </Typography>
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
      </Grid>
    </Container>
  );
}

export default HomePage;
