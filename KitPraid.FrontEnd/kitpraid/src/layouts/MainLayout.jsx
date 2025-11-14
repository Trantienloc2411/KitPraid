import React from "react";
import Navigation from "../components/Navigation";
import Footer from "../components/Footer";
import defaultCategories from "../data/categories";

/**
 * Main Layout Component
 * Wraps pages with Navigation and Footer
 *
 * @param {React.ReactNode} children - Page content
 * @param {boolean} showNavigation - Show/hide navigation (default: true)
 * @param {boolean} showFooter - Show/hide footer (default: true)
 * @param {string} className - Additional CSS classes
 * @param {object} style - Additional inline styles
 */
const MainLayout = ({
  children,
  showNavigation = true,
  showFooter = true,
  className = "",
  style = {},
  navigationCategories,
}) => {
  const categoriesForNavigation =
    Array.isArray(navigationCategories) && navigationCategories.length > 0
      ? navigationCategories
      : defaultCategories;
  return (
    <div
      className={`main-layout ${className}`}
      style={{
        display: "flex",
        flexDirection: "column",
        minHeight: "100vh",
        width: "100%",
        ...style,
      }}
    >
      {/* Navigation */}
      {showNavigation && <Navigation categoriesData={categoriesForNavigation} />}

      {/* Main content area */}
      <main
        style={{
          flex: 1,
          width: "100%",
          display: "flex",
          flexDirection: "column",
        }}
      >
        {children}
      </main>

      {/* Footer */}
      {showFooter && <Footer />}
    </div>
  );
};

export default MainLayout;
