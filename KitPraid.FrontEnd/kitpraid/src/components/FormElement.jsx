import React, { useMemo, useState } from "react";
import { getColor } from "../theme/colors";

// Checkmark icon for checkbox
const CheckIcon = ({ size = 14, color = "#FFFFFF" }) => (
  <svg
    width={size}
    height={size}
    viewBox="0 0 24 24"
    fill="none"
    stroke={color}
    strokeWidth="3"
    strokeLinecap="round"
    strokeLinejoin="round"
  >
    <polyline points="20 6 9 17 4 12" />
  </svg>
);

/**
 * Checkbox component
 * Props:
 * - checked: boolean (controlled)
 * - defaultChecked: boolean (uncontrolled)
 * - onChange: (checked: boolean) => void
 * - disabled: boolean
 * - variant: 'default' | 'error'
 * - size: 'sm' | 'md' | 'lg'
 * - label: string | ReactNode
 * - className, style
 */
export const Checkbox = ({
  checked,
  defaultChecked = false,
  onChange,
  disabled = false,
  variant = "default",
  size = "md",
  label,
  className = "",
  style = {},
  ...rest
}) => {
  const [internalChecked, setInternalChecked] = useState(defaultChecked);
  const [isHovered, setIsHovered] = useState(false);
  const isControlled = checked !== undefined;
  const isChecked = isControlled ? checked : internalChecked;

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { box: 16, icon: 10, radius: 3 },
      md: { box: 20, icon: 12, radius: 4 },
      lg: { box: 24, icon: 14, radius: 5 },
    };
    return map[size] || map.md;
  }, [size]);

  const palette = useMemo(() => {
    if (variant === "error") {
      return {
        border: isHovered ? "#EE5858" : "#EE5858",
        background: isChecked ? "#EE5858" : "#FFFFFF",
        uncheckedBg: "#FFFFFF",
        uncheckedBorder: "#EE5858",
      };
    }
    return {
      border: isHovered
        ? getColor("primary", 500)
        : isChecked
        ? getColor("primary", 500)
        : "#D1D5DB",
      background: isChecked
        ? getColor("primary", 500)
        : isHovered
        ? "#FFFFFF"
        : "#F3F4F6",
      uncheckedBg: isHovered ? "#FFFFFF" : "#F3F4F6",
      uncheckedBorder: isHovered ? getColor("primary", 500) : "#9CA3AF",
    };
  }, [variant, isChecked, isHovered]);

  const handleChange = (e) => {
    if (disabled) return;
    const newChecked = e.target.checked;
    if (!isControlled) setInternalChecked(newChecked);
    if (typeof onChange === "function") onChange(newChecked);
  };

  const checkboxStyle = {
    width: sizeStyles.box,
    height: sizeStyles.box,
    borderRadius: sizeStyles.radius,
    border: `2px solid ${palette.border}`,
    backgroundColor: palette.background,
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    cursor: disabled ? "not-allowed" : "pointer",
    opacity: disabled ? 0.5 : 1,
    transition: "all 150ms ease",
    flexShrink: 0,
    position: "relative",
    ...style,
  };

  return (
    <label
      className={className}
      style={{
        display: "inline-flex",
        alignItems: "center",
        gap: 8,
        cursor: disabled ? "not-allowed" : "pointer",
        userSelect: "none",
      }}
      onMouseEnter={() => !disabled && setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <input
        type="checkbox"
        checked={isChecked}
        onChange={handleChange}
        disabled={disabled}
        style={{ position: "absolute", opacity: 0, pointerEvents: "none" }}
        {...rest}
      />
      <div style={checkboxStyle}>
        {isChecked && <CheckIcon size={sizeStyles.icon} />}
      </div>
      {label && (
        <span style={{ fontSize: 14, color: disabled ? "#9CA3AF" : "#111827" }}>
          {label}
        </span>
      )}
    </label>
  );
};

/**
 * Radio component
 * Props:
 * - checked: boolean (controlled)
 * - defaultChecked: boolean (uncontrolled)
 * - onChange: (checked: boolean) => void
 * - disabled: boolean
 * - variant: 'default' | 'error'
 * - size: 'sm' | 'md' | 'lg'
 * - label: string | ReactNode
 * - name: string (for radio groups)
 * - value: string | number
 * - className, style
 */
export const Radio = ({
  checked,
  defaultChecked = false,
  onChange,
  disabled = false,
  variant = "default",
  size = "md",
  label,
  name,
  value,
  className = "",
  style = {},
  ...rest
}) => {
  const [internalChecked, setInternalChecked] = useState(defaultChecked);
  const [isHovered, setIsHovered] = useState(false);
  const isControlled = checked !== undefined;
  const isChecked = isControlled ? checked : internalChecked;

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { outer: 16, inner: 6, border: 2 },
      md: { outer: 20, inner: 8, border: 2 },
      lg: { outer: 24, inner: 10, border: 2 },
    };
    return map[size] || map.md;
  }, [size]);

  const palette = useMemo(() => {
    if (variant === "error") {
      return {
        border: isHovered ? "#EE5858" : "#EE5858",
        background: isChecked ? "#EE5858" : "#FFFFFF",
        uncheckedBg: "#FFFFFF",
        uncheckedBorder: "#EE5858",
      };
    }
    return {
      border: isHovered
        ? getColor("primary", 500)
        : isChecked
        ? getColor("primary", 500)
        : "#D1D5DB",
      background: isChecked
        ? getColor("primary", 500)
        : isHovered
        ? "#FFFFFF"
        : "#F3F4F6",
      uncheckedBg: isHovered ? "#FFFFFF" : "#F3F4F6",
      uncheckedBorder: isHovered ? getColor("primary", 500) : "#9CA3AF",
    };
  }, [variant, isChecked, isHovered]);

  const handleChange = (e) => {
    if (disabled) return;
    const newChecked = e.target.checked;
    if (!isControlled) setInternalChecked(newChecked);
    if (typeof onChange === "function") onChange(newChecked, value);
  };

  const radioStyle = {
    width: sizeStyles.outer,
    height: sizeStyles.outer,
    borderRadius: "50%",
    border: `${sizeStyles.border}px solid ${palette.border}`,
    backgroundColor: palette.background,
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    cursor: disabled ? "not-allowed" : "pointer",
    opacity: disabled ? 0.5 : 1,
    transition: "all 150ms ease",
    flexShrink: 0,
    position: "relative",
    ...style,
  };

  const innerCircleStyle = {
    width: sizeStyles.inner,
    height: sizeStyles.inner,
    borderRadius: "50%",
    backgroundColor: isChecked ? "#FFFFFF" : "transparent",
    transition: "all 150ms ease",
  };

  return (
    <label
      className={className}
      style={{
        display: "inline-flex",
        alignItems: "center",
        gap: 8,
        cursor: disabled ? "not-allowed" : "pointer",
        userSelect: "none",
      }}
      onMouseEnter={() => !disabled && setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <input
        type="radio"
        checked={isChecked}
        onChange={handleChange}
        disabled={disabled}
        name={name}
        value={value}
        style={{ position: "absolute", opacity: 0, pointerEvents: "none" }}
        {...rest}
      />
      <div style={radioStyle}>
        <div style={innerCircleStyle} />
      </div>
      {label && (
        <span style={{ fontSize: 14, color: disabled ? "#9CA3AF" : "#111827" }}>
          {label}
        </span>
      )}
    </label>
  );
};

/**
 * Toggle Switch component
 * Props:
 * - checked: boolean (controlled)
 * - defaultChecked: boolean (uncontrolled)
 * - onChange: (checked: boolean) => void
 * - disabled: boolean
 * - variant: 'default' | 'error'
 * - size: 'sm' | 'md' | 'lg'
 * - label: string | ReactNode
 * - className, style
 */
export const Toggle = ({
  checked,
  defaultChecked = false,
  onChange,
  disabled = false,
  variant = "default",
  size = "md",
  label,
  className = "",
  style = {},
  ...rest
}) => {
  const [internalChecked, setInternalChecked] = useState(defaultChecked);
  const [isHovered, setIsHovered] = useState(false);
  const isControlled = checked !== undefined;
  const isChecked = isControlled ? checked : internalChecked;

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { width: 40, height: 20, thumb: 14, gap: 3 },
      md: { width: 48, height: 24, thumb: 18, gap: 3 },
      lg: { width: 56, height: 28, thumb: 22, gap: 3 },
    };
    return map[size] || map.md;
  }, [size]);

  const palette = useMemo(() => {
    if (variant === "error") {
      return {
        track: isChecked ? "#EE5858" : isHovered ? "#FFFFFF" : "#F3F4F6",
        border: isHovered ? "#EE5858" : "transparent",
        thumb: isChecked ? "#FFFFFF" : isHovered ? "#EE5858" : "#9CA3AF",
      };
    }
    return {
      track: isChecked
        ? getColor("primary", 500)
        : isHovered
        ? "#FFFFFF"
        : "#F3F4F6",
      border: isHovered ? getColor("primary", 500) : "transparent",
      thumb: isChecked
        ? "#FFFFFF"
        : isHovered
        ? getColor("primary", 500)
        : "#9CA3AF",
    };
  }, [variant, isChecked, isHovered]);

  const handleChange = (e) => {
    if (disabled) return;
    const newChecked = e.target.checked;
    if (!isControlled) setInternalChecked(newChecked);
    if (typeof onChange === "function") onChange(newChecked);
  };

  const trackStyle = {
    width: sizeStyles.width,
    height: sizeStyles.height,
    borderRadius: sizeStyles.height / 2,
    backgroundColor: palette.track,
    border: `2px solid ${palette.border}`,
    position: "relative",
    cursor: disabled ? "not-allowed" : "pointer",
    opacity: disabled ? 0.5 : 1,
    transition: "all 150ms ease",
    flexShrink: 0,
    ...style,
  };

  const thumbStyle = {
    position: "absolute",
    top: `calc((100% - ${sizeStyles.thumb}px) / 2)`,
    left: isChecked
      ? `calc(100% - ${sizeStyles.thumb}px - ${sizeStyles.gap}px)`
      : sizeStyles.gap,
    width: sizeStyles.thumb,
    height: sizeStyles.thumb,
    borderRadius: "50%",
    backgroundColor: palette.thumb,
    transition: "all 150ms ease",
    boxShadow: "0 2px 4px rgba(0,0,0,0.2)",
  };

  return (
    <label
      className={className}
      style={{
        display: "inline-flex",
        alignItems: "center",
        gap: 8,
        cursor: disabled ? "not-allowed" : "pointer",
        userSelect: "none",
      }}
      onMouseEnter={() => !disabled && setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <input
        type="checkbox"
        checked={isChecked}
        onChange={handleChange}
        disabled={disabled}
        style={{ position: "absolute", opacity: 0, pointerEvents: "none" }}
        {...rest}
      />
      <div style={trackStyle}>
        <div style={thumbStyle} />
      </div>
      {label && (
        <span style={{ fontSize: 14, color: disabled ? "#9CA3AF" : "#111827" }}>
          {label}
        </span>
      )}
    </label>
  );
};

// Default export with all components
const FormElement = {
  Checkbox,
  Radio,
  Toggle,
};

export default FormElement;
