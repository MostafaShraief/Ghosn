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
import {
  Notifications as NotificationsIcon,
  Circle,
} from "@mui/icons-material";
import { getNotifications } from "../services/api"; // Import the API function
import { formatDistanceToNow } from "date-fns";
import { ar } from "date-fns/locale"; // Import Arabic locale

function NotificationsPage() {
  const [notifications, setNotifications] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchNotifications = async () => {
      try {
        const fetchedNotifications = await getNotifications();

        // Format the date and add read status (defaulting to unread)
        const formattedNotifications = fetchedNotifications.map(
          (notification) => ({
            id: notification.notificationID, // Use the correct ID from the API
            title: notification.title,
            description: notification.body,
            date: formatDistanceToNow(new Date(notification.dateAndTime), {
              addSuffix: true,
              locale: ar,
            }),
            read: false, // Initially, all notifications are unread
          })
        );

        setNotifications(formattedNotifications);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching notifications:", error);
        setLoading(false);
        // Optionally, display an error message to the user.
      }
    };

    fetchNotifications();
  }, []);

  const handleNotificationClick = (id) => {
    setNotifications((prevNotifications) =>
      prevNotifications.map((notification) =>
        notification.id === id ? { ...notification, read: true } : notification
      )
    );
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

  // Check if notifications is empty and display a message
  if (notifications.length === 0) {
    return (
      <Container maxWidth="md" sx={{ py: 4 }}>
        <Paper
          elevation={3}
          sx={{ p: 3, borderRadius: 2, textAlign: "center" }}
        >
          <Typography
            variant="h4"
            component="h1"
            gutterBottom
            sx={{ fontWeight: 700 }}
          >
            الإشعارات
          </Typography>
          <Typography variant="body1">لا توجد إشعارات لعرضها.</Typography>
        </Paper>
      </Container>
    );
  }

  return (
    <Container maxWidth="md" sx={{ py: 4 }}>
      <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
        <Typography
          variant="h4"
          component="h1"
          gutterBottom
          sx={{ fontWeight: 700 }}
        >
          الإشعارات
        </Typography>

        <List>
          {notifications.map((notification) => (
            <React.Fragment key={notification.id}>
              <ListItem
                button // Make the list item clickable
                onClick={() => handleNotificationClick(notification.id)}
                sx={{
                  bgcolor: notification.read
                    ? "background.paper"
                    : "action.hover",
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
