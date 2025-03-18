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

const drawerWidth = 260;

const theme = createTheme({
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

function RootLayout() {
  return (
    <Box
      sx={{
        display: "flex",
        minHeight: "100vh",
        bgcolor: "background.default",
      }}
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
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<RootLayout />}>
            <Route index element={<HomePage />} />
            <Route path="chat" element={<ChatView />} />
            <Route path="ai-prompt" element={<AIPromptPage />} />
            <Route path="planting-form" element={<PlantingFormPage />} />
            <Route
              path="planting-output"
              element={<PlantingOutputPage />}
            />{" "}
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  </StrictMode>
);
