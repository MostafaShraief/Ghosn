// client-app/src/components/DrawerComponent.jsx
import React, { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import ListItemIcon from "@mui/material/ListItemIcon";
import AddIcon from "@mui/icons-material/Add";
import NotificationsIcon from "@mui/icons-material/Notifications";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import { deepOrange } from "@mui/material/colors";
import Divider from "@mui/material/Divider";
import ghosnImage from "@/assets/ghosn.png"; //  Replace with the correct path
import { Agriculture, House } from "@mui/icons-material";
import LogoutIcon from "@mui/icons-material/Logout"; // Import Logout icon

function DrawerComponent({ drawerWidth }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    // Check for login status and get user data
    const loggedIn = localStorage.getItem("isLoggedIn") === "true";
    setIsLoggedIn(loggedIn);

    if (loggedIn) {
      const firstName = localStorage.getItem("firstName");
      const lastName = localStorage.getItem("lastName");
      // Combine first and last name (or use just one if preferred)
      setUserName(`${firstName} ${lastName}`);
    }
  }, []);

  const handleRecentChatsClick = () => {
    setRecentChatsOpen(!recentChatsOpen);
  };

  const handleLogout = () => {
    // Clear user data from localStorage
    localStorage.removeItem("clientID");
    localStorage.removeItem("firstName");
    localStorage.removeItem("lastName");
    localStorage.removeItem("isLoggedIn");

    // Redirect to login page
    navigate("/login");
  };

  if (location.pathname === "/login") {
    return null;
  }

  return (
    <Drawer
      variant="permanent"
      dir="rtl"
      anchor="right"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: {
          width: drawerWidth,
          boxSizing: "border-box",
          background: "linear-gradient(180deg, #ffffff, #f8f9fc)",
          borderLeft: "none",
          boxShadow: "2px 0 8px rgba(0,0,0,0.08)",
        },
      }}
    >
      <Box
        component="img"
        src={ghosnImage}
        alt="Ghosn"
        sx={{
          maxWidth: "150px",
          margin: "0 auto",
          display: "block",
          p: 1,
        }}
      />
      <Box sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
        <List sx={{ px: 1 }}>
          {[
            { text: "الصفحة الرئيسية", to: "/", icon: <House /> },
            {
              text: "إضافة خطة زراعة",
              to: "/planting-form",
              icon: <AddIcon />,
            },
            {
              text: "الإشعارات",
              to: "/notifications",
              icon: <NotificationsIcon />,
            },
            {
              text: "خطط الزراعية السابقة",
              to: "/notifications",
              icon: <Agriculture />,
            },
          ].map((item) => (
            <ListItem key={item.text} disablePadding>
              <ListItemButton
                onClick={() => navigate(item.to)}
                sx={{
                  borderRadius: 2,
                  mb: 0.5,
                  "&:hover": { bgcolor: "rgba(25, 118, 210, 0.08)" },
                }}
              >
                {item.icon && (
                  <ListItemIcon sx={{ minWidth: "40px" }}>
                    {item.icon}
                  </ListItemIcon>
                )}
                <ListItemText
                  primary={item.text}
                  primaryTypographyProps={{ fontWeight: 500 }}
                  sx={{ textAlign: "right" }}
                />
              </ListItemButton>
            </ListItem>
          ))}
        </List>

        <Box sx={{ mt: "auto", p: 2 }}>
          <Divider sx={{ mb: 2 }} />
          {isLoggedIn ? ( // Conditionally render based on login status
            <ListItemButton
              onClick={handleLogout} // Call handleLogout on click
              sx={{
                color: "primary.main",
                borderRadius: 2,
                p: 1,
                "&:hover": { opacity: 0.9 },
                borderColor: "primary.main",
              }}
            >
              <Avatar sx={{ bgcolor: deepOrange[500], width: 32, height: 32 }}>
                {userName.charAt(0).toUpperCase()}{" "}
                {/* Display first letter of name */}
              </Avatar>
              <ListItemText
                primary={userName} // Display the user's name
                primaryTypographyProps={{ fontWeight: 500 }}
                sx={{ textAlign: "right", pr: 2 }}
              />
              <ListItemIcon>
                <LogoutIcon /> {/* Add a logout icon */}
              </ListItemIcon>
            </ListItemButton>
          ) : (
            <ListItemButton
              onClick={() => navigate("/login")}
              sx={{
                color: "primary.main",
                borderRadius: 2,
                p: 1,
                "&:hover": { opacity: 0.9 },
                borderColor: "primary.main",
              }}
            >
              <Avatar sx={{ bgcolor: deepOrange[500], width: 32, height: 32 }}>
                U
              </Avatar>
              <ListItemText
                primary={"تسجيل الدخول"}
                primaryTypographyProps={{ fontWeight: 500 }}
                sx={{ textAlign: "right", pr: 2 }}
              />
            </ListItemButton>
          )}
        </Box>
      </Box>
    </Drawer>
  );
}

export default DrawerComponent;
