import React, { useMemo, useState } from 'react';
import { getColor } from '../theme/colors';

const Input = ({
    label,
    helperText,
    labelPosition = 'left', // 'left' | 'center' | 'right'
    type = 'text', // 'text' | 'email' | 'password' | 'tel' | 'number' | ...
    variant = 'default', // 'default' | 'success' | 'warning' | 'error'
    size = 'md',
    isDisabled = false,
    hasBorder = true,
    hasBackgroundTint = true, // for success/warning/error states
    leftIcon,
    rightIcon,
    leftIconProps = {},
    rightIconProps = {},
    showPasswordToggle = true,
    value,
    defaultValue,
    onChange,
    placeholder,
    style = {},
    className = '',
    ...rest
}) => {
    const [isFocused, setIsFocused] = useState(false);
    const [isPasswordVisible, setIsPasswordVisible] = useState(false);
    const [internalValue, setInternalValue] = useState(defaultValue ?? '');
    const isControlled = value !== undefined;
    const displayedValue = isControlled ? value : internalValue;

    const sizeStyles = useMemo(() => {
        const mapping = {
            sm: { height: 36, fontSize: 14, radius: 2, paddingX: 12, gap: 8, icon: 16 },
            md: { height: 44, fontSize: 14, radius: 2, paddingX: 14, gap: 10, icon: 18 },
            lg: { height: 50, fontSize: 16, radius: 2, paddingX: 16, gap: 12, icon: 20 },
        };
        return mapping[size] || mapping.md;
    }, [size]);

    const palette = useMemo(() => {
        const map = {
            default: {
                border: '#E5E7EB',
                text: '#111827',
                placeholder: '#9CA3AF',
                background: '#FFFFFF',
                focus: getColor('primary', 500),
            },
            success: {
                border: getColor('success', 500),
                text: '#14532D',
                placeholder: '#16A34A',
                background: hasBackgroundTint ? 'rgba(45,178,36,0.10)' : '#FFFFFF',
                focus: getColor('success', 500),
            },
            warning: {
                border: getColor('warning', 500),
                text: '#92400E',
                placeholder: '#D97706',
                background: hasBackgroundTint ? 'rgba(235,200,12,0.12)' : '#FFFFFF',
                focus: getColor('warning', 500),
            },
            error: {
                border: getColor('danger', 500),
                text: '#7F1D1D',
                placeholder: '#DC2626',
                background: hasBackgroundTint ? 'rgba(238,88,88,0.10)' : '#FFFFFF',
                focus: getColor('danger', 500),
            },
        };
        return map[variant] || map.default;
    }, [variant, hasBackgroundTint]);

    const containerStyles = useMemo(() => {
        return {
            display: 'flex',
            flexDirection: 'column',
            gap: 6,
            width: '100%',
            maxWidth: '100%',
            boxSizing: 'border-box',
            ...style,
        };
    }, [style]);

    const fieldWrapperStyles = useMemo(() => {
        return {
            display: 'flex',
            alignItems: 'center',
            gap: sizeStyles.gap,
            minHeight: sizeStyles.height,
            height: 'auto',
            borderRadius: sizeStyles.radius,
            border: hasBorder ? `2px solid ${isFocused ? palette.focus : palette.border}` : 'none',
            backgroundColor: palette.background,
            padding: `0 ${sizeStyles.paddingX}px`,
            opacity: isDisabled ? 0.6 : 1,
            pointerEvents: isDisabled ? 'none' : 'auto',
            transition: 'border-color 120ms ease, background-color 120ms ease, box-shadow 120ms ease',
            boxShadow: isFocused ? `0 0 0 2px ${palette.focus}22` : 'none',
            width: '100%',
            maxWidth: '100%',
            boxSizing: 'border-box',
        };
    }, [sizeStyles, hasBorder, isFocused, palette, isDisabled]);

    const inputStyles = useMemo(() => {
        return {
            appearance: 'none',
            border: 'none',
            outline: 'none',
            flex: 1,
            minWidth: 0,
            width: '100%',
            maxWidth: '100%',
            background: 'transparent',
            fontSize: `clamp(${sizeStyles.fontSize - 2}px, 2vw, ${sizeStyles.fontSize}px)`,
            color: palette.text,
            boxSizing: 'border-box',
            wordBreak: 'break-word',
            overflowWrap: 'break-word',
            /* Pseudo-elements cannot be set via inline styles.
               If you need placeholder color globally, add CSS rule instead. */
        };
    }, [sizeStyles, palette]);

    const labelStyle = useMemo(() => {
        const justify = labelPosition === 'center' ? 'center' : labelPosition === 'right' ? 'flex-end' : 'flex-start';
        return {
            display: label ? 'flex' : 'none',
            justifyContent: justify,
            color: '#111827',
            fontSize: 12,
            fontWeight: 600,
        };
    }, [label, labelPosition]);

    const LeftIcon = () => (
        <svg width={sizeStyles.icon} height={sizeStyles.icon} viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path d="M4 8l8 5 8-5" stroke={palette.placeholder} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
            <rect x="3" y="5" width="18" height="14" rx="2" stroke={palette.placeholder} strokeWidth="2" />
        </svg>
    );

    const EyeIcon = ({ off }) => (
        <svg width={sizeStyles.icon} height={sizeStyles.icon} viewBox="0 0 24 24" fill="none" aria-hidden="true">
            {off ? (
                <>
                    <path d="M3 3l18 18" stroke={palette.placeholder} strokeWidth="2" />
                    <path d="M10.58 10.58A3 3 0 0012 15a3 3 0 002.42-4.42M21 12s-3.5 6-9 6-9-6-9-6 3.5-6 9-6 9 6 9 6z" stroke={palette.placeholder} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                </>
            ) : (
                <>
                    <circle cx="12" cy="12" r="3" stroke={palette.placeholder} strokeWidth="2" />
                    <path d="M21 12s-3.5 6-9 6-9-6-9-6 3.5-6 9-6 9 6 9 6z" stroke={palette.placeholder} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                </>
            )}
        </svg>
    );

    const statusRightIcon = useMemo(() => {
        if (variant === 'success') {
            return (
                <svg width={sizeStyles.icon} height={sizeStyles.icon} viewBox="0 0 24 24" fill="none" aria-hidden="true">
                    <path d="M20 6L9 17l-5-5" stroke={palette.border} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                </svg>
            );
        }
        if (variant === 'warning') {
            return (
                <svg width={sizeStyles.icon} height={sizeStyles.icon} viewBox="0 0 24 24" fill="none" aria-hidden="true">
                    <circle cx="12" cy="12" r="9" stroke={palette.border} strokeWidth="2" />
                    <path d="M12 7v6m0 4h.01" stroke={palette.border} strokeWidth="2" strokeLinecap="round" />
                </svg>
            );
        }
        if (variant === 'error') {
            return (
                <svg width={sizeStyles.icon} height={sizeStyles.icon} viewBox="0 0 24 24" fill="none" aria-hidden="true">
                    <path d="M12 9v4m0 4h.01M10.29 3.86L1.82 18a2 2 0 001.71 3h16.94a2 2 0 001.71-3L13.71 3.86a2 2 0 00-3.42 0z" stroke={palette.border} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                </svg>
            );
        }
        return null;
    }, [variant, palette.border, sizeStyles.icon]);

    const renderIcon = (icon, extraProps = {}) => {
        if (!icon) return null;
        // If user passed a JSX element, clone it with defaults when not provided
        if (React.isValidElement(icon)) {
            const propsMerged = {
                size: icon.props.size ?? sizeStyles.icon,
                color: icon.props.color ?? palette.placeholder,
                style: { flexShrink: 0, ...(icon.props.style || {}) },
                ...extraProps,
            };
            return React.cloneElement(icon, propsMerged);
        }
        // If user passed a component reference, create element from it
        if (typeof icon === 'function') {
            return React.createElement(icon, {
                size: sizeStyles.icon,
                color: palette.placeholder,
                style: { flexShrink: 0 },
                ...extraProps,
            });
        }
        return null;
    };

    const normalizedLeftIcon = leftIcon !== undefined
        ? renderIcon(leftIcon, leftIconProps)
        : (type === 'email' ? <LeftIcon /> : null);

    const normalizedRightIcon = renderIcon(rightIcon, rightIconProps);
    const isPasswordType = type === 'password';
    const effectiveType = isPasswordType && isPasswordVisible ? 'text' : type;

    return (
        <label style={containerStyles} className={className}>
            <div style={labelStyle}>{label}</div>
            <div style={fieldWrapperStyles}>
                {normalizedLeftIcon}
                <input
                    type={effectiveType}
                    value={displayedValue}
                    onChange={(e) => {
                        if (!isControlled) setInternalValue(e.target.value);
                        if (typeof onChange === 'function') onChange(e);
                    }}
                    placeholder={placeholder}
                    disabled={isDisabled}
                    onFocus={() => setIsFocused(true)}
                    onBlur={() => setIsFocused(false)}
                    style={inputStyles}
                    {...rest}
                />
                {isPasswordType && showPasswordToggle && (
                    <button 
                        type="button" 
                        onClick={() => setIsPasswordVisible(v => !v)} 
                        style={{ 
                            background: 'transparent', 
                            border: 'none', 
                            padding: '8px',
                            margin: '-8px',
                            cursor: 'pointer',
                            minWidth: '44px',
                            minHeight: '44px',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            flexShrink: 0,
                        }}
                        aria-label={isPasswordVisible ? 'Hide password' : 'Show password'}
                    >
                        <EyeIcon off={!isPasswordVisible} />
                    </button>
                )}
                {statusRightIcon}
                {normalizedRightIcon}
            </div>
            {helperText && (
                <div style={{ fontSize: 12, color: palette.placeholder }}>{helperText}</div>
            )}
        </label>
    );
};

export default Input;