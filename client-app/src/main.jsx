import React, { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import App from "./App.jsx";
import ChatView from "@/components/ChatView.jsx";
import AIPrompt from "@/components/AIPrompt.jsx";
import DrawerComponent from "@/components/DrawerComponent.jsx";
import Box from "@mui/material/Box";
import { CssBaseline } from "@mui/material";

const drawerWidth = 240;

function RootLayout() {
  return (
    <Box sx={{ display: "flex", width: "100vw", height: "100vh" }}>
      <CssBaseline />
      <DrawerComponent drawerWidth={drawerWidth} />
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 3,
          width: "100%",
          height: "100%",
        }}
      >
        <Outlet />
      </Box>
    </Box>
  );
}

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<RootLayout />}>
          <Route index element={<App />} />
          <Route path="chat" element={<ChatView />} />
          <Route path="ai-prompt" element={<AIPrompt />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>
);
