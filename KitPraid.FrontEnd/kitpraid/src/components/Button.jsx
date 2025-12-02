import React, { useMemo, useState } from "react";
import { getColor, getWeight } from "../theme/colors";

/**
 * Button component with rich customization options.
 * Props:
 * - label: string (text content if children not provided)
 * - isDisabled: boolean
 * - onClick: function
 * - color: string (CSS color value)
 * - size: 'sm' | 'md' | 'lg'
 * - hasArrow: boolean (renders a right arrow icon)
 * - hasCircle: boolean (renders a left outlined circle icon)
 * - hasBackground: boolean (filled background)
 * - hasBorder: boolean (outlined border)
 * - variant: 'primary' | 'secondary' | 'success' | 'warning' | 'danger'
 * - isBold: boolean (font weight +100; disabled forces 200)
 * - className: string (optional extra classes)
 * - style: object (optional extra inline styles)
 */
const Button = ({
  label,
  children,
  isDisabled = false,
  onClick,
  color,
  size = "md",
  hasArrow = false,
  hasCircle = false,
  hasBackground = true,
  hasBorder = false,
  variant = "primary",
  isBold = false,
  leftIcon,
  rightIcon,
  leftIconProps = {},
  rightIconProps = {},
  className = "",
  style = {},
  ...rest
}) => {
  const [isHovered, setIsHovered] = useState(false);
  const sizeStyles = useMemo(() => {
    const mapping = {
      sm: { padding: "6px 12px", fontSize: 12, gap: 6, icon: 12, radius: 1 },
      md: { padding: "10px 16px", fontSize: 14, gap: 8, icon: 14, radius: 2 },
      lg: { padding: "14px 20px", fontSize: 16, gap: 10, icon: 16, radius: 3 },
    };
    return mapping[size] || mapping.md;
  }, [size]);

  const resolvedColor = useMemo(() => {
    const base = color || getColor(variant, isDisabled ? 200 : 500);
    return base;
  }, [variant, color, isDisabled]);

  const renderIcon = (icon, extraProps = {}) => {
    if (!icon) return null;
    if (React.isValidElement(icon)) {
      const merged = {
        size: icon.props.size ?? sizeStyles.icon + 2,
        color:
          icon.props.color ??
          (hasBackground && !(isHovered || isDisabled)
            ? "#FFFFFF"
            : resolvedColor),
        style: { flexShrink: 0, ...(icon.props.style || {}) },
        ...extraProps,
      };
      return React.cloneElement(icon, merged);
    }
    if (typeof icon === "function") {
      return React.createElement(icon, {
        size: sizeStyles.icon + 2,
        color:
          hasBackground && !(isHovered || isDisabled)
            ? "#FFFFFF"
            : resolvedColor,
        style: { flexShrink: 0 },
        ...extraProps,
      });
    }
    return null;
  };

  const computedStyles = useMemo(() => {
    const baseBackground = hasBackground ? resolvedColor : "transparent";
    const hoverBackground = "#F3F4F6"; // gray-100
    const backgroundColor =
      !isDisabled && isHovered ? hoverBackground : baseBackground;
    const textColor =
      !isDisabled && isHovered
        ? resolvedColor
        : hasBackground
          ? "#FFFFFF"
          : resolvedColor;
    const borderStyle = hasBorder
      ? `2px solid ${resolvedColor}`
      : "2px solid transparent";
    const fontWeight = getWeight({ isBold, isDisabled, base: 500 });

    // Parse padding for responsive values
    const paddingParts = sizeStyles.padding.split(" ");
    const paddingY = paddingParts[0] || "10px";
    const paddingX = paddingParts[1] || paddingParts[0] || "16px";
    // const paddingYNum = parseInt(paddingY);
    const paddingXNum = parseInt(paddingX);

    return {
      display: "inline-flex",
      alignItems: "center",
      justifyContent: "center",
      gap: `clamp(${sizeStyles.gap - 2}px, 1vw, ${sizeStyles.gap}px)`,
      padding: `${paddingY} clamp(${Math.max(paddingXNum - 4, 4)}px, 2vw, ${paddingX})`,
      borderRadius: sizeStyles.radius,
      fontWeight,
      letterSpacing: 0.5,
      textTransform: "uppercase",
      lineHeight: 1.2,
      cursor: isDisabled ? "not-allowed" : "pointer",
      opacity: isDisabled ? 0.8 : 1,
      userSelect: "none",
      outline: "none",
      border: borderStyle,
      backgroundColor,
      color: textColor,
      transition:
        "filter 120ms ease, transform 120ms ease, background-color 150ms ease, box-shadow 150ms ease",
      transform: !isDisabled && isHovered ? "translateY(-1px)" : "none",
      boxShadow:
        !isDisabled && isHovered ? "0 4px 10px rgba(0,0,0,0.08)" : "none",
      minWidth: "44px", // Minimum touch target size
      minHeight: "44px",
      width: "fit-content",
      maxWidth: "100%",
      boxSizing: "border-box",
      wordBreak: "break-word",
      overflowWrap: "break-word",
      ...style,
    };
  }, [
    resolvedColor,
    hasBackground,
    hasBorder,
    isDisabled,
    isBold,
    sizeStyles,
    style,
    isHovered,
  ]);

  const IconCircle = () => (
    <svg
      width={sizeStyles.icon + 2}
      height={sizeStyles.icon + 2}
      viewBox="0 0 20 20"
      fill="none"
      aria-hidden="true"
      focusable="false"
      style={{ display: "block" }}
    >
      <circle
        cx="10"
        cy="10"
        r="7.5"
        stroke={hasBackground ? "#FFFFFF" : resolvedColor}
        strokeWidth="2"
      />
    </svg>
  );

  const IconArrow = () => (
    <svg
      width={sizeStyles.icon + 2}
      height={sizeStyles.icon + 2}
      viewBox="0 0 24 24"
      fill="none"
      aria-hidden="true"
      focusable="false"
      style={{ display: "block" }}
    >
      <path
        d="M5 12h12"
        stroke={hasBackground ? "#FFFFFF" : resolvedColor}
        strokeWidth="2"
        strokeLinecap="round"
      />
      <path
        d="M13 6l6 6-6 6"
        stroke={hasBackground ? "#FFFFFF" : resolvedColor}
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );

  const handleClick = (event) => {
    if (isDisabled) return;
    if (typeof onClick === "function") onClick(event);
  };

  return (
    <button
      type="button"
      onClick={handleClick}
      disabled={isDisabled}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      className={className}
      style={computedStyles}
      {...rest}
    >
      {renderIcon(leftIcon, leftIconProps) || (hasCircle && <IconCircle />)}
      <span
        style={{
          fontSize: `clamp(${sizeStyles.fontSize - 1}px, 1.5vw, ${sizeStyles.fontSize}px)`,
          minWidth: 0,
          flexShrink: 1,
          overflow: "hidden",
          textOverflow: "ellipsis",
          whiteSpace: "nowrap",
        }}
      >
        {children ?? label ?? "Label"}
      </span>
      {renderIcon(rightIcon, rightIconProps) || (hasArrow && <IconArrow />)}
    </button>
  );
};

export default Button;
