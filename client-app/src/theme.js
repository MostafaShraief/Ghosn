// client-app/src/theme.js
import { createTheme } from "@mui/material/styles";
import createCache from "@emotion/cache";
import { prefixer } from "stylis";
import rtlPlugin from "stylis-plugin-rtl";

export const theme = createTheme({
  direction: "rtl",
  palette: {
    primary: { main: "#43a047" },
    secondary: { main: "#00c853" },
    background: { default: "#f5f7fa" },

    gold: {
      main: "#FFD700",
    },
    silver: {
      main: "#C0C0C0",
    },
    bronze: {
      main: "#CD7F32",
    },
  },
  typography: {
    fontFamily:
      '"Noto Sans Arabic", sans-serif, "Inter", "Roboto", "Helvetica", "Arial"',
  },
  components: {
    MuiButton: {
      styleOverrides: { root: { borderRadius: 8, textTransform: "none" } },
    },
  },
});

export const cacheRtl = createCache({
  key: "muirtl",
  stylisPlugins: [prefixer, rtlPlugin],
});
