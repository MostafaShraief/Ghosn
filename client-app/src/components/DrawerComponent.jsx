import React, { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import ListItemIcon from "@mui/material/ListItemIcon"; // إضافة ListItemIcon
import AddIcon from "@mui/icons-material/Add";
import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";
import NotificationsIcon from "@mui/icons-material/Notifications"; // أيقونة الإشعارات
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import { deepOrange } from "@mui/material/colors";
import Divider from "@mui/material/Divider";
import { Typography } from "@mui/material";

function DrawerComponent({ drawerWidth }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsLoggedIn(true);
    }
  }, []);

  const handleRecentChatsClick = () => {
    setRecentChatsOpen(!recentChatsOpen);
  };

  // إذا كان المسار الحالي هو /login، لا تعرض السايد بار
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
      <Typography
        variant="h4"
        p={2}
        sx={{ fontWeight: 600, color: "primary.main" }}
        align="center"
      >
        Ghosn
      </Typography>
      <Divider />
      <Box sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
        <List sx={{ px: 1 }}>
          {[
            { text: "الصفحة الرئيسية", to: "/", icon: null },
            { text: "الموجه الذكي", to: "/ai-prompt", icon: null },
            { text: "إضافة خطة زراعة", to: "/planting-form", icon: null },
            { text: "محادثة جديدة", to: "/chat", icon: <AddIcon /> },
            { text: "الإشعارات", to: "/notifications", icon: <NotificationsIcon /> }, // إضافة عنصر الإشعارات
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

          <ListItem disablePadding>
            <ListItemButton
              onClick={handleRecentChatsClick}
              sx={{ borderRadius: 2 }}
            >
              <ListItemText
                primary="الدردشات الأخيرة"
                primaryTypographyProps={{ fontWeight: 500 }}
                sx={{ textAlign: "right" }}
              />
              {recentChatsOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
          </ListItem>

          {recentChatsOpen && (
            <List disablePadding sx={{ pl: 2 }}>
              {[1, 2, 3].map((num) => (
                <ListItem key={num} disablePadding>
                  <ListItemButton
                    onClick={() => navigate(`/chat/${num}`)}
                    sx={{
                      borderRadius: 2,
                      py: 0.5,
                      "&:hover": { bgcolor: "rgba(25, 118, 210, 0.08)" },
                    }}
                  >
                    <ListItemText
                      primary={`الدردشة ${num}`}
                      primaryTypographyProps={{ color: "text.secondary" }}
                      sx={{ textAlign: "right" }}
                    />
                  </ListItemButton>
                </ListItem>
              ))}
            </List>
          )}
        </List>

        <Box sx={{ mt: "auto", p: 2 }}>
          <Divider sx={{ mb: 2 }} />
          <ListItemButton
            onClick={() => (isLoggedIn ? navigate("/profile") : navigate("/login"))}
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
              primary={isLoggedIn ? "اسم المستخدم" : "تسجيل الدخول"}
              primaryTypographyProps={{ fontWeight: 500 }}
              sx={{ textAlign: "right", pr: 2 }}
            />
          </ListItemButton>
        </Box>
      </Box>
    </Drawer>
  );
}

export default DrawerComponent;