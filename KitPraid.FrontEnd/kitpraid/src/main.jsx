import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import Router from "./router/index.jsx";
import { QueryProvider } from "./lib/react-query.jsx";
import "./i18n/config.js";
import "./App.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <QueryProvider>
      <Router />
    </QueryProvider>
  </StrictMode>
);
