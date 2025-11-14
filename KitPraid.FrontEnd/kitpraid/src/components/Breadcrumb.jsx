import React, { useMemo } from "react";
import { getColor } from "../theme/colors";

// Default Home icon (outline)
const HomeIcon = ({ size = 16, color = "#6B7280" }) => (
  <svg
    width={size}
    height={size}
    viewBox="0 0 24 24"
    fill="none"
    aria-hidden="true"
    style={{ display: "block" }}
  >
    <path
      d="M3 10.5l9-7 9 7"
      stroke={color}
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    />
    <path
      d="M5 10v9a1 1 0 001 1h4v-6h4v6h4a1 1 0 001-1v-9"
      stroke={color}
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    />
  </svg>
);

/**
 * Breadcrumb component
 * Props:
 * - items: Array<{ label: string, href?: string, onClick?: (e)=>void }>
 * - separator: string | ReactNode (default ›)
 * - size: 'sm' | 'md' | 'lg'
 * - variant: 'default' | 'primary' (active color)
 * - showHome: boolean | ReactNode (show a leading home icon / custom node)
 * - ariaLabel: string (default 'Breadcrumb')
 * - className, style
 */
const Breadcrumb = ({
  items = [],
  separator = "›",
  size = "md",
  variant = "default",
  showHome = false,
  ariaLabel = "Breadcrumb",
  className = "",
  style = {},
}) => {
  const sizeStyles = useMemo(() => {
    const map = {
      sm: { fontSize: 12, gap: 8, icon: 14, py: 8, px: 12 },
      md: { fontSize: 14, gap: 10, icon: 16, py: 10, px: 16 },
      lg: { fontSize: 16, gap: 12, icon: 18, py: 12, px: 20 },
    };
    return map[size] || map.md;
  }, [size]);

  const palette = useMemo(() => {
    return {
      link: variant === "primary" ? getColor("secondary", 500) : "#0EA5E9",
      text: "#6B7280",
      active: "#111827",
      bg: "#F3F4F6",
      separator: "#9CA3AF",
    };
  }, [variant]);

  const container = {
    display: "flex",
    alignItems: "center",
    gap: sizeStyles.gap,
    padding: `${sizeStyles.py}px ${sizeStyles.px}px`,
    background: palette.bg,
    borderRadius: 3,
    width: "100%",
    overflowX: "auto",
    whiteSpace: "nowrap",
    ...style,
  };

  const linkStyle = {
    color: palette.link,
    textDecoration: "none",
    cursor: "pointer",
  };

  const textStyle = {
    color: palette.text,
  };

  const activeStyle = {
    color: palette.active,
    fontWeight: 600,
  };

  const renderSeparator = (i) => (
    <span key={`sep-${i}`} aria-hidden style={{ color: palette.separator }}>
      {typeof separator === "string" ? separator : separator}
    </span>
  );

  const homeNode =
    showHome === true ? (
      <HomeIcon size={sizeStyles.icon} color={palette.text} />
    ) : showHome ? (
      showHome
    ) : null;

  const normalizedItems = Array.isArray(items) ? items : [];

  return (
    <nav aria-label={ariaLabel} className={className} style={container}>
      {homeNode && <span style={textStyle}>{homeNode}</span>}

      <ol
        style={{
          display: "flex",
          alignItems: "center",
          gap: sizeStyles.gap,
          margin: 0,
          padding: 0,
          listStyle: "none",
        }}
      >
        {normalizedItems.map((item, i) => {
          const isLast = i === normalizedItems.length - 1;
          const content = isLast ? (
            <span style={activeStyle}>{item.label}</span>
          ) : item.href || item.onClick ? (
            <a href={item.href || "#"} onClick={item.onClick} style={linkStyle}>
              {item.label}
            </a>
          ) : (
            <span style={textStyle}>{item.label}</span>
          );

          return (
            <li
              key={`crumb-${i}`}
              style={{
                display: "inline-flex",
                alignItems: "center",
                gap: sizeStyles.gap,
              }}
            >
              {content}
              {!isLast && renderSeparator(i)}
            </li>
          );
        })}
      </ol>
    </nav>
  );
};

export default Breadcrumb;
