import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { getColor } from "../theme/colors";
import Link from "./Link";
import Tag from "./Tag";

// Google Play Icon
const GooglePlayIcon = () => (
  <svg
    width="24"
    height="24"
    viewBox="0 0 24 24"
    fill="none"
    style={{ display: "block" }}
  >
    <path
      d="M3 20.5V3.5C3 2.91 3.34 2.39 3.84 2.15L13.69 12L3.84 21.85C3.34 21.6 3 21.09 3 20.5Z"
      fill="currentColor"
    />
    <path
      d="M16.81 15.12L6.05 21.34L14.54 12.85L16.81 15.12Z"
      fill="currentColor"
    />
    <path
      d="M14.54 11.15L6.05 2.66L16.81 8.88L14.54 11.15Z"
      fill="currentColor"
    />
    <path
      d="M20.16 10.81C20.41 10.93 20.59 11.18 20.59 11.45V12.55C20.59 12.82 20.41 13.07 20.16 13.19L16.81 15.12L14.54 12.85L20.16 10.81Z"
      fill="currentColor"
    />
  </svg>
);

// Apple Icon
const AppleIcon = () => (
  <svg
    width="24"
    height="24"
    viewBox="0 0 24 24"
    fill="none"
    style={{ display: "block" }}
  >
    <path
      d="M17.05 20.28C16.07 21.28 14.87 21.1 13.86 20.63C12.84 20.16 11.96 20.14 10.79 20.63C9.35 21.25 8.49 21.07 7.56 20.28C2.3 14.79 3.24 7.31 9.29 7.02C10.37 7.1 11.15 7.7 11.95 7.74C12.9 7.64 13.8 7.01 14.92 6.97C16.2 6.89 17.31 7.45 18.01 8.4C15.41 9.88 16.01 13.15 18.57 13.9C18.13 15.18 17.47 16.44 16.55 17.5C15.88 18.3 15.2 19.1 14.25 19.95L17.05 20.28ZM12.03 7.01C11.9 4.85 13.63 3.1 15.68 2.9C15.92 5.4 12.9 7.26 12.03 7.01Z"
      fill="currentColor"
    />
  </svg>
);

const Footer = () => {
  const { t } = useTranslation();
  const [selectedTags, setSelectedTags] = useState([t("footer.graphicsCard")]);

  const categories = [
    t("footer.computerLaptop"),
    t("footer.smartphone"),
    t("footer.headphone"),
    t("footer.accessories"),
    t("footer.cameraPhoto"),
    t("footer.tvHomes"),
  ];

  const quickLinks = [
    t("footer.shopProduct"),
    t("footer.shoppingCart"),
    t("footer.wishlist"),
    t("footer.compare"),
    t("footer.trackOrder"),
    t("footer.customerHelp"),
    t("footer.aboutUs"),
  ];

  const tags = [
    t("footer.game"),
    t("footer.iphone"),
    t("footer.tv"),
    t("footer.asusLaptops"),
    t("footer.macbook"),
    t("footer.ssd"),
    t("footer.graphicsCard"),
    t("footer.powerBank"),
    t("footer.smartTV"),
    t("footer.speaker"),
    t("footer.tablet"),
    t("footer.microwave"),
    t("footer.samsung"),
  ];

  const containerStyle = {
    backgroundColor: "#222222",
    color: "#FFFFFF",
    padding: "clamp(2rem, 6vw, 4rem) clamp(1rem, 5vw, 8rem)",
    width: "100%",
    boxSizing: "border-box",
  };

  const gridStyle = {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(200px, 1fr))",
    gap: "clamp(2rem, 5vw, 3rem)",
    maxWidth: "1400px",
    margin: "0 auto",
  };

  const columnStyle = {
    display: "flex",
    flexDirection: "column",
    gap: "1rem",
  };

  const titleStyle = {
    fontSize: "clamp(14px, 2vw, 16px)",
    fontWeight: "bold",
    textTransform: "uppercase",
    marginBottom: "0.5rem",
    color: "#FFFFFF",
  };

  const textStyle = {
    fontSize: "clamp(13px, 1.8vw, 15px)",
    color: "#CCCCCC",
    lineHeight: 1.6,
  };

  const linkStyle = {
    fontSize: "clamp(13px, 1.8vw, 15px)",
    color: "#CCCCCC",
    textDecoration: "none",
    transition: "color 200ms ease",
    cursor: "pointer",
  };

  const logoStyle = {
    fontSize: "clamp(20px, 3vw, 28px)",
    fontWeight: "bold",
    color: getColor("primary", 500),
    marginBottom: "1rem",
  };

  const appButtonStyle = {
    display: "flex",
    alignItems: "center",
    gap: "12px",
    padding: "12px 16px",
    backgroundColor: "#333333",
    borderRadius: "8px",
    border: "1px solid #444444",
    cursor: "pointer",
    transition: "all 200ms ease",
    marginBottom: "12px",
    textDecoration: "none",
    color: "#FFFFFF",
  };

  const tagContainerStyle = {
    display: "flex",
    flexWrap: "wrap",
    gap: "8px",
  };

  return (
    <footer style={containerStyle}>
      <div style={gridStyle}>
        {/* Company Information */}
        <div style={columnStyle}>
          <div style={logoStyle}>KITPRAID</div>
          <div style={textStyle}>{t("footer.customerSupport")}</div>
          <div style={textStyle}>{t("footer.phone")}</div>
          <div style={textStyle}>{t("footer.address")}</div>
          <div style={textStyle}>{t("footer.email")}</div>
        </div>

        {/* Top Category */}
        <div style={columnStyle}>
          <div style={titleStyle}>{t("footer.topCategory")}</div>
          {categories.map((category, index) => (
            <a
              key={index}
              href="#"
              style={{
                ...linkStyle,
                ...(category === t("footer.accessories") && {
                  color: getColor("primary", 500),
                  borderBottom: `2px solid ${getColor("primary", 500)}`,
                  paddingBottom: "2px",
                  display: "inline-block",
                }),
              }}
              onMouseEnter={(e) => {
                if (category !== t("footer.accessories")) {
                  e.currentTarget.style.color = getColor("primary", 500);
                }
              }}
              onMouseLeave={(e) => {
                if (category !== t("footer.accessories")) {
                  e.currentTarget.style.color = "#CCCCCC";
                }
              }}
            >
              {category}
            </a>
          ))}
          <Link
            href="#"
            variant="primary"
            hasArrow
            style={{ marginTop: "0.5rem", color: getColor("primary", 500) }}
          >
            {t("footer.browseAllProduct")}
          </Link>
        </div>

        {/* Quick Links */}
        <div style={columnStyle}>
          <div style={titleStyle}>{t("footer.quickLinks")}</div>
          {quickLinks.map((link, index) => (
            <a
              key={index}
              href="#"
              style={linkStyle}
              onMouseEnter={(e) =>
                (e.currentTarget.style.color = getColor("primary", 500))
              }
              onMouseLeave={(e) => (e.currentTarget.style.color = "#CCCCCC")}
            >
              {link}
            </a>
          ))}
        </div>

        {/* Download App */}
        <div style={columnStyle}>
          <div style={titleStyle}>{t("footer.downloadApp")}</div>
          <a
            href="#"
            style={appButtonStyle}
            onMouseEnter={(e) => {
              e.currentTarget.style.backgroundColor = "#3A3A3A";
              e.currentTarget.style.borderColor = "#555555";
            }}
            onMouseLeave={(e) => {
              e.currentTarget.style.backgroundColor = "#333333";
              e.currentTarget.style.borderColor = "#444444";
            }}
          >
            <GooglePlayIcon />
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "flex-start",
              }}
            >
              <span style={{ fontSize: "11px", color: "#999999" }}>
                {t("footer.getItNow")}
              </span>
              <span style={{ fontSize: "14px", fontWeight: "bold" }}>
                {t("footer.googlePlay")}
              </span>
            </div>
          </a>
          <a
            href="#"
            style={appButtonStyle}
            onMouseEnter={(e) => {
              e.currentTarget.style.backgroundColor = "#3A3A3A";
              e.currentTarget.style.borderColor = "#555555";
            }}
            onMouseLeave={(e) => {
              e.currentTarget.style.backgroundColor = "#333333";
              e.currentTarget.style.borderColor = "#444444";
            }}
          >
            <AppleIcon />
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "flex-start",
              }}
            >
              <span style={{ fontSize: "11px", color: "#999999" }}>
                {t("footer.getItNow")}
              </span>
              <span style={{ fontSize: "14px", fontWeight: "bold" }}>
                {t("footer.appStore")}
              </span>
            </div>
          </a>
        </div>

        {/* Popular Tags */}
        <div style={columnStyle}>
          <div style={titleStyle}>{t("footer.popularTag")}</div>
          <div style={tagContainerStyle}>
            {tags.map((tag, index) => {
              const isSelected = selectedTags.includes(tag);
              return (
                <Tag
                  key={index}
                  label={tag}
                  size="sm"
                  hasBackground={isSelected}
                  hasBorder
                  tone="dark"
                  onClick={() => {
                    setSelectedTags((prev) => {
                      if (prev.includes(tag)) {
                        return prev.filter((t) => t !== tag);
                      } else {
                        return [...prev, tag];
                      }
                    });
                  }}
                  style={{
                    cursor: "pointer",
                    backgroundColor: isSelected ? "#333333" : "transparent",
                    borderColor: isSelected ? "#FFFFFF" : "#444444",
                    color: isSelected ? "#FFFFFF" : "#CCCCCC",
                    transition: "all 200ms ease",
                  }}
                  onMouseEnter={(e) => {
                    if (!isSelected) {
                      e.currentTarget.style.borderColor = "#666666";
                      e.currentTarget.style.backgroundColor = "#2A2A2A";
                    }
                  }}
                  onMouseLeave={(e) => {
                    if (!isSelected) {
                      e.currentTarget.style.borderColor = "#444444";
                      e.currentTarget.style.backgroundColor = "transparent";
                    }
                  }}
                />
              );
            })}
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
