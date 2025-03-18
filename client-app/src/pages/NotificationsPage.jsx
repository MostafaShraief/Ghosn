import React, { useState, useEffect } from "react";
import {
  Container,
  Typography,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Divider,
  Paper,
  Box,
  CircularProgress,
} from "@mui/material";
import { Notifications as NotificationsIcon, Circle } from "@mui/icons-material";

function NotificationsPage() {
  const [notifications, setNotifications] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // بيانات وهمية للإشعارات
    const mockNotifications = [
      {
        id: 1,
        title: "تذكير: سقاية المحصول",
        description: "يرجى سقاية المحصول الذي لديك اليوم.",
        date: "منذ 5 دقائق",
        read: false, // إشعار غير مقروء
      },
      {
        id: 2,
        title: "تذكير: قم بري النباتات",
        description: "يرجى ري النباتات في الصباح الباكر.",
        date: "منذ ساعة",
        read: true, // إشعار مقروء
      },
      {
        id: 3,
        title: "تذكير: تفقد الآفات",
        description: "تفقد النباتات بحثًا عن أي آفات.",
        date: "منذ يومين",
        read: false, // إشعار غير مقروء
      },
    ];

    // محاكاة جلب البيانات من الباك إند
    const fetchNotifications = async () => {
      try {
        // يمكنك استبدال هذا الجزء بطلب API حقيقي
        setTimeout(() => {
          setNotifications(mockNotifications);
          setLoading(false);
        }, 1000); // محاكاة تأخير الشبكة
      } catch (error) {
        console.error("Error fetching notifications:", error);
        setLoading(false);
      }
    };

    fetchNotifications();
  }, []);

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

  return (
    <Container maxWidth="md" sx={{ py: 4 }}>
      <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
        <Typography variant="h4" component="h1" gutterBottom sx={{ fontWeight: 700 }}>
          الإشعارات
        </Typography>

        <List>
          {notifications.map((notification) => (
            <React.Fragment key={notification.id}>
              <ListItem
                sx={{
                  bgcolor: notification.read ? "background.paper" : "action.hover",
                  borderRadius: 2,
                  mb: 1,
                }}
              >
                <ListItemIcon>
                  {!notification.read && (
                    <Circle sx={{ color: "primary.main", fontSize: "12px" }} />
                  )}
                </ListItemIcon>
                <ListItemText
                  primary={
                    <Typography variant="body1" sx={{ fontWeight: 600 }}>
                      {notification.title}
                    </Typography>
                  }
                  secondary={
                    <>
                      <Typography variant="body2" color="textSecondary">
                        {notification.description}
                      </Typography>
                      <Typography variant="caption" color="textSecondary">
                        {notification.date}
                      </Typography>
                    </>
                  }
                />
              </ListItem>
              <Divider />
            </React.Fragment>
          ))}
        </List>
      </Paper>
    </Container>
  );
}

export default NotificationsPage;