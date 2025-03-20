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
  Badge,
  Avatar,
  useTheme, // Import useTheme
  Snackbar, // Import Snackbar
  Alert, // Import Alert
} from "@mui/material";
import {
  Notifications as NotificationsIcon,
  CheckCircleOutline as CheckCircleIcon, // For read notifications
  ErrorOutline as ErrorIcon, // For error notifications (optional)
  InfoOutlined as InfoIcon, // for info notifications
  FiberManualRecord as UnreadDotIcon,
} from "@mui/icons-material";
import { getNotifications } from "../services/api";
import { formatDistanceToNow } from "date-fns";
import { ar } from "date-fns/locale";
import { styled } from "@mui/material/styles"; // Import styled

// Styled component for the notification item
const StyledListItem = styled(ListItem)(({ theme, read }) => ({
  backgroundColor: read
    ? theme.palette.background.selected
    : theme.palette.action.default, // Use a more subtle color
  borderRadius: theme.shape.borderRadius,
  marginBottom: theme.spacing(1),
  transition: "background-color 0.2s ease-in-out", // Smooth transition
  "&:hover": {
    backgroundColor: read
      ? theme.palette.action.hover
      : theme.palette.action.selected, // maintain hover effect
    cursor: "pointer", //show a pointer on hover
  },
}));

//Styled component for the Unread Icon
const StyledBadge = styled(Badge)(({ theme }) => ({
  "& .MuiBadge-badge": {
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.primary.contrastText,
    boxShadow: `0 0 0 2px ${theme.palette.background.paper}`,
    "&::after": {
      position: "absolute",
      top: 0,
      left: 0,
      width: "100%",
      height: "100%",
      borderRadius: "50%",
      animation: "ripple 1.2s infinite ease-in-out",
      border: "1px solid currentColor",
      content: '""',
    },
  },
  "@keyframes ripple": {
    "0%": {
      transform: "scale(.8)",
      opacity: 1,
    },
    "100%": {
      transform: "scale(2.4)",
      opacity: 0,
    },
  },
}));

function NotificationsPage() {
  const [notifications, setNotifications] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null); // State for handling errors
  const [snackbarOpen, setSnackbarOpen] = useState(false); //for showing error in snackbar

  const theme = useTheme(); // Get the current theme
  useEffect(() => {
    const fetchNotifications = async () => {
      try {
        const fetchedNotifications = await getNotifications();

        // Sort notifications by dateAndTime in descending order (newest first)
        const sortedNotifications = fetchedNotifications.sort(
          (a, b) => new Date(b.dateAndTime) - new Date(a.dateAndTime)
        );

        // Improved notification type handling and date formatting
        const formattedNotifications = sortedNotifications.map(
          (notification) => {
            let icon;
            switch (notification.type) {
              case "success":
                icon = <CheckCircleIcon color="success" />;
                break;
              case "error":
                icon = <ErrorIcon color="error" />;
                break;
              case "info":
                icon = <InfoIcon color="info" />;
                break;
              default:
                icon = <NotificationsIcon />;
            }

            return {
              id: notification.notificationID,
              title: notification.title,
              description: notification.body,
              date: formatDistanceToNow(new Date(notification.dateAndTime), {
                addSuffix: true,
                locale: ar,
              }),
              read: notification.read || false,
              icon: icon,
            };
          }
        );

        setNotifications(formattedNotifications);
      } catch (err) {
        setError(err.message || "Failed to fetch notifications.");
        setSnackbarOpen(true);
      } finally {
        setLoading(false);
      }
    };
    fetchNotifications();

    return () => {};
  }, []);

  const handleNotificationClick = (id) => {
    setNotifications((prevNotifications) =>
      prevNotifications.map((notification) =>
        notification.id === id ? { ...notification, read: true } : notification
      )
    );
    //  Here, you should *also* make an API call to *persistently* mark
    //  the notification as read on the server.  This is *crucial*
    //  for a real-world application.  Without this, the notification
    //  will revert to "unread" on a page refresh.  Example:
    //
    //  markNotificationAsRead(id).catch(err => {
    //      console.error("Failed to mark notification as read:", err);
    //      //  Optionally, revert the local state change if the API call fails:
    //      setNotifications((prevNotifications) =>
    //          prevNotifications.map((notification) =>
    //              notification.id === id ? { ...notification, read: false } : notification
    //           )
    //        );
    //  });
  };

  const handleSnackbarClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setSnackbarOpen(false);
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
  return (
    <Container>
      <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
        <Box sx={{ display: "flex", alignItems: "center", mb: 3 }}>
          <NotificationsIcon
            sx={{ fontSize: 40, mr: 2, color: theme.palette.primary.main }}
          />
          <Typography variant="h4" component="h1" sx={{ fontWeight: 700 }}>
            الإشعارات
          </Typography>
        </Box>

        {notifications.length === 0 ? (
          <Typography variant="body1" align="center" sx={{ mt: 2 }}>
            لا توجد إشعارات لعرضها.
          </Typography>
        ) : (
          <List>
            {notifications.map((notification) => (
              <React.Fragment key={notification.id}>
                <StyledListItem
                  button
                  onClick={() => handleNotificationClick(notification.id)}
                  read={notification.read} // Pass read status to the styled component
                >
                  <ListItemIcon>
                    {notification.read ? (
                      notification.icon // Show the type-specific icon, even when read
                    ) : (
                      <StyledBadge
                        overlap="circular"
                        anchorOrigin={{
                          vertical: "bottom",
                          horizontal: "right",
                        }}
                        variant="dot"
                      >
                        {notification.icon}
                      </StyledBadge>
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
                        <Typography variant="body2" color="text.secondary">
                          {notification.description}
                        </Typography>
                        <Typography variant="caption" color="text.secondary">
                          {notification.date}
                        </Typography>
                      </>
                    }
                  />
                </StyledListItem>
                <Divider />
              </React.Fragment>
            ))}
          </List>
        )}
      </Paper>

      {/* Snackbar for displaying errors */}
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          onClose={handleSnackbarClose}
          severity="error"
          sx={{ width: "100%" }}
        >
          {error}
        </Alert>
      </Snackbar>
    </Container>
  );
}

export default NotificationsPage;
