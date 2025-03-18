import React, { useState } from "react";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import AddIcon from "@mui/icons-material/Add";
import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import { deepOrange } from "@mui/material/colors";
import { Link } from "react-router-dom";
import Divider from "@mui/material/Divider";
import ghosnImage from "@/assets/ghosn.png";

function DrawerComponent({ drawerWidth }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);

  const handleRecentChatsClick = () => {
    setRecentChatsOpen(!recentChatsOpen);
  };

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
            { text: "الصفحة الرئيسية", to: "/" },
            { text: "الموجه الذكي", to: "/ai-prompt" },
            { text: "إضافة خطة زراعة", to: "/planting-form" },
            { text: "محادثة جديدة", to: "/chat", icon: <AddIcon /> },
          ].map((item) => (
            <ListItem key={item.text} disablePadding>
              <ListItemButton
                component={Link}
                to={item.to}
                sx={{
                  borderRadius: 2,
                  mb: 0.5,
                  "&:hover": { bgcolor: "rgba(25, 118, 210, 0.08)" },
                }}
              >
                <ListItemText
                  primary={item.text}
                  primaryTypographyProps={{ fontWeight: 500 }}
                  sx={{ textAlign: "right" }}
                />
                {item.icon}
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
                    component={Link}
                    to={`/chat/${num}`}
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
              primary="اسم المستخدم"
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
