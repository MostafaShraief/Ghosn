import React from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import { ThemeProvider } from "@mui/material/styles";
import { CacheProvider } from "@emotion/react";
import { CssBaseline, Box } from "@mui/material";
import { theme, cacheRtl } from "@/theme";
import DrawerComponent from "@/components/DrawerComponent";
import LandingPage from "@/pages/LandingPage";
import ChatView from "@/components/ChatView";
import HomePage from "@/pages/HomePage";
import AIPromptPage from "@/pages/AIPromptPage";
import PlantingFormPage from "@/pages/PlantingFormPage";
import PlantingOutputPage from "@/pages/PlantingOutputPage";
import NotificationsPage from "@/pages/NotificationsPage";
import PlansPage from "@/pages/PlansPage";
import LoginForm from "@/pages/Login";

export const drawerWidth = 260;

function AppLayout() {
  return (
    <Box
      sx={{
        display: "flex",
        minHeight: "100vh",
        bgcolor: "background.default",
      }}
      dir="rtl"
    >
      <CssBaseline />
      <DrawerComponent drawerWidth={drawerWidth} />
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 4,
          width: `calc(100% - ${drawerWidth}px)`,
          maxWidth: "1440px",
          mx: "auto",
        }}
      >
        <Outlet />
      </Box>
    </Box>
  );
}

const AppRoutes = () => (
  <Routes>
    <Route path="/" element={<LandingPage />} />
    <Route path="/app" element={<AppLayout />}>
      <Route index element={<HomePage />} />
      <Route path="chat" element={<ChatView />} />
      <Route path="ai-prompt" element={<AIPromptPage />} />
      <Route path="planting-form" element={<PlantingFormPage />} />
      <Route path="planting-output" element={<PlantingOutputPage />} />
      <Route path="notifications" element={<NotificationsPage />} />
      <Route path="plans" element={<PlansPage />} />
      <Route path="login" element={<LoginForm />} />
    </Route>
  </Routes>
);

createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <CacheProvider value={cacheRtl}>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <AppRoutes />
        </BrowserRouter>
      </ThemeProvider>
    </CacheProvider>
  </React.StrictMode>
);
