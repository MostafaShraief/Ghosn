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
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import { deepOrange } from "@mui/material/colors";
import ghosnImage from "@/assets/ghosn.png";
import {
  Agriculture,
  House,
  MonetizationOn,
  Support,
} from "@mui/icons-material";
import LogoutIcon from "@mui/icons-material/Logout";

function DrawerComponent({ drawerWidth, userType = "client" }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userName, setUserName] = useState("");
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const firstName = localStorage.getItem("firstName");
    const lastName = localStorage.getItem("lastName");

    if (firstName && lastName) {
      setIsLoggedIn(true);
      setUserName(`${firstName} ${lastName}`);
    } else {
      setIsLoggedIn(false);
      setUserName("");
    }
  }, [location]);

  const handleLogout = () => {
    localStorage.removeItem("clientID");
    localStorage.removeItem("firstName");
    localStorage.removeItem("lastName");
    navigate("/app/login");
  };

  if (location.pathname === "/app/login") {
    return null;
  }

  const clientItems = [
    { text: "الصفحة الرئيسية", to: "/app", icon: <House /> },
    { text: "إضافة خطة زراعة", to: "/app/planting-form", icon: <AddIcon /> },
    {
      text: "الإشعارات",
      to: "/app/notifications",
      icon: <NotificationsIcon />,
    },
    { text: "خطط الزراعية السابقة", to: "/app/plans", icon: <Agriculture /> },
    { text: "الجوائز", to: "/app/awards", icon: <EmojiEventsIcon /> },
  ];

  const donorItems = [
    { text: "الصفحة الرئيسية", to: "/donor", icon: <House /> },
    {
      text: "إنشاء جائزة",
      to: "/donor/create-prize",
      icon: <MonetizationOn />,
    },
    { text: "الدعم المباشر", to: "/donor/direct-support", icon: <Support /> },
  ];

  const menuItems = userType === "donor" ? donorItems : clientItems;

  return (
    <Drawer
      variant="permanent"
      anchor="left"
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
        <List sx={{ px: 1, textAlignLast: "start" }}>
          {menuItems.map((item) => (
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
          {isLoggedIn ? (
            <ListItemButton
              onClick={handleLogout}
              sx={{
                color: "primary.main",
                borderRadius: 2,
                p: 1,
                "&:hover": { opacity: 0.9 },
                borderColor: "primary.main",
              }}
            >
              <Avatar sx={{ bgcolor: deepOrange[500], width: 32, height: 32 }}>
                {userName.charAt(0).toUpperCase()}
              </Avatar>
              <ListItemText
                primary={userName}
                primaryTypographyProps={{ fontWeight: 500 }}
                sx={{ textAlign: "right", pr: 2 }}
              />
              <ListItemIcon>
                <LogoutIcon />
              </ListItemIcon>
            </ListItemButton>
          ) : (
            <ListItemButton
              onClick={() => navigate("/app/login")}
              sx={{
                color: "primary.main",
                border: "black 1px solid",
                borderRadius: 2,
                p: 1,
                "&:hover": { opacity: 0.9 },
                borderColor: "primary.main",
              }}
            >
              <ListItemText
                primary={"تسجيل الدخول"}
                primaryTypographyProps={{ fontWeight: 500 }}
                sx={{ textAlign: "right", pr: 2, textAlignLast: "center" }}
              />
            </ListItemButton>
          )}
        </Box>
      </Box>
    </Drawer>
  );
}

export default DrawerComponent;
