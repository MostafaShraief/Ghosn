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
import { Typography } from "@mui/material";

function DrawerComponent({ drawerWidth }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);

  const handleRecentChatsClick = () => {
    setRecentChatsOpen(!recentChatsOpen);
  };

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: {
          width: drawerWidth,
          boxSizing: "border-box",
          background: "linear-gradient(180deg, #ffffff, #f8f9fc)",
          borderRight: "none",
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
            { text: "Home", to: "/" },
            { text: "AI Prompt", to: "/ai-prompt" },
            { text: "Add a planting plan", to: "/planting-form" },
            { text: "New Chat", to: "/chat", icon: <AddIcon /> },
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
                primary="Recent Chats"
                primaryTypographyProps={{ fontWeight: 500 }}
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
                      primary={`Chat ${num}`}
                      primaryTypographyProps={{ color: "text.secondary" }}
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
            <Avatar
              sx={{ bgcolor: deepOrange[500], mr: 1.5, width: 32, height: 32 }}
            >
              U
            </Avatar>
            <ListItemText
              primary="Username"
              primaryTypographyProps={{ fontWeight: 500 }}
            />
          </ListItemButton>
        </Box>
      </Box>
    </Drawer>
  );
}

export default DrawerComponent;
