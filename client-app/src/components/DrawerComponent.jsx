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
import { Link } from "react-router-dom"; // Import Link from react-router-dom

function DrawerComponent({ drawerWidth }) {
  const [recentChatsOpen, setRecentChatsOpen] = useState(false);

  const handleRecentChatsClick = () => {
    setRecentChatsOpen(!recentChatsOpen);
  };

  return (
    <Drawer
      variant="permanent" //  Make the drawer permanent
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: {
          width: drawerWidth,
          boxSizing: "border-box",
          backgroundColor: "#f8f8ff",
        },
      }}
    >
      <Box sx={{ overflow: "auto" }}>
        {" "}
        {/* Make the drawer content scrollable if needed */}
        <List>
          <ListItem disablePadding>
            <ListItemButton component={Link} to="/">
              <ListItemText primary="AI Prompt" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton component={Link} to="/chat">
              <ListItemText primary="New Chat" />
              <AddIcon />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={handleRecentChatsClick}>
              <ListItemText primary="Recent Chats" />
              {recentChatsOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
          </ListItem>
          {recentChatsOpen && (
            <>
              <ListItem disablePadding>
                <ListItemButton component={Link} to="/chat/1">
                  <ListItemText primary="Chat 1" sx={{ pl: 4 }} />
                </ListItemButton>
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton component={Link} to="/chat/2">
                  <ListItemText primary="Chat 2" sx={{ pl: 4 }} />
                </ListItemButton>
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton component={Link} to="/chat/3">
                  <ListItemText primary="Chat 3" sx={{ pl: 4 }} />
                </ListItemButton>
              </ListItem>
            </>
          )}
        </List>
        <Box sx={{ flexGrow: 1 }} /> {/* Push username to bottom */}
        <ListItem sx={{ pb: 2 }}>
          <ListItemButton
            sx={{ backgroundColor: "#212121", color: "white", borderRadius: 1 }}
          >
            <Avatar
              sx={{ bgcolor: deepOrange[500], mr: 1, width: 24, height: 24 }}
            >
              U
            </Avatar>
            <ListItemText primary="Username" />
            <ExpandLess />
          </ListItemButton>
        </ListItem>
      </Box>
    </Drawer>
  );
}

export default DrawerComponent;
