import React, { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import ChatView from "@/components/ChatView";
import DrawerComponent from "@/components/DrawerComponent";
import Box from "@mui/material/Box";
import { CssBaseline } from "@mui/material";
import HomePage from "@/pages/HomePage";
import AIPromptPage from "@/pages/AIPromptPage";
import PlantingFormPage from "@/pages/PlantingFormPage";
import PlantingOutputPage from "@/pages/PlantingOutputPage";
import Login from "@/pages/Login";
import NotificationsPage from "@/pages/NotificationsPage";
import PlansPage from "@/pages/PlansPage";
import LandingPage from "@/pages/LandingPage";
import rtlPlugin from "stylis-plugin-rtl";
import { prefixer } from "stylis";
import { CacheProvider } from "@emotion/react";
import createCache from "@emotion/cache";

const drawerWidth = 260;

const theme = createTheme({
  direction: "rtl",
  palette: {
    primary: {
      main: "#43a047",
    },
    secondary: {
      main: "#00c853",
    },
    background: { default: "#f5f7fa" },
  },
  typography: {
    fontFamily:
      '"Noto Sans Arabic", sans-serif, "Inter", "Roboto", "Helvetica", "Arial"',
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          textTransform: "none",
        },
      },
    },
  },
});

const cacheRtl = createCache({
  key: "muirtl",
  stylisPlugins: [prefixer, rtlPlugin],
});

function RootLayout() {
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

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <CacheProvider value={cacheRtl}>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <Routes>
            {/* Route for the standalone Landing Page */}
            <Route path="/" element={<LandingPage />} />

            {/* Routes for the main application (using RootLayout) */}
            <Route path="/app" element={<RootLayout />}>
              <Route path="home" element={<HomePage />} />
              <Route path="chat" element={<ChatView />} />
              <Route path="ai-prompt" element={<AIPromptPage />} />
              <Route path="planting-form" element={<PlantingFormPage />} />
              <Route path="notifications" element={<NotificationsPage />} />
              <Route path="plans" element={<PlansPage />} />
              <Route
                path="planting-output"
                element={<PlantingOutputPage />}
              />{" "}
              <Route path="login" element={<Login />} />
            </Route>
          </Routes>
        </BrowserRouter>
      </ThemeProvider>
    </CacheProvider>
  </StrictMode>
);
