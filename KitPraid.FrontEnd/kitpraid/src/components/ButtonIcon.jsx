import React, { useMemo } from 'react';
import { getColor } from '../theme/colors';

const ButtonIcon = ({
    icon,
    size = 'md',
    variant = 'primary',
    isDisabled = false,
    onClick,
    hasBackground = true,
    hasBorder = false,
    color,
    shape = 'circle',
    style = {},
    className = '',
    ...rest
}) => {
    const sizeStyles = useMemo(() => {
        const mapping = {
            sm: { padding: '6px', iconSize: 16, radius: 1 },
            md: { padding: '10px', iconSize: 20, radius: 2 },
            lg: { padding: '14px', iconSize: 24, radius: 3 },
        };
        return mapping[size] || mapping.md;
    }, [size]);

    const resolvedColor = useMemo(() => {
        const base = color || getColor(variant, isDisabled ? 200 : 500);
        return base;
    }, [variant, color, isDisabled]);

    const computedStyles = useMemo(() => {
        const backgroundColor = hasBackground ? resolvedColor : 'transparent';
        const textColor = hasBackground ? '#FFFFFF' : resolvedColor;
        const borderStyle = hasBorder ? `2px solid ${resolvedColor}` : '2px solid transparent';

        const paddingValue = parseInt(sizeStyles.padding);
        const totalSize = sizeStyles.iconSize + paddingValue * 2;
        const minTouchSize = 44; // Minimum touch target size for accessibility
        const finalSize = Math.max(totalSize, minTouchSize);

        return {
            display: 'inline-flex',
            alignItems: 'center',
            justifyContent: 'center',
            padding: sizeStyles.padding,
            width: `${finalSize}px`,
            height: `${finalSize}px`,
            minWidth: `${finalSize}px`,
            minHeight: `${finalSize}px`,
            maxWidth: '100%',
            maxHeight: '100%',
            borderRadius: shape === 'circle' ? '50%' : `${sizeStyles.radius}px`,
            backgroundColor,
            color: textColor,
            border: borderStyle,
            cursor: isDisabled ? 'not-allowed' : 'pointer',
            opacity: isDisabled ? 0.8 : 1,
            userSelect: 'none',
            outline: 'none',
            transition: 'filter 120ms ease, transform 120ms ease',
            boxSizing: 'border-box',
            flexShrink: 0,
            ...style,
        };
    }, [resolvedColor, hasBackground, hasBorder, isDisabled, sizeStyles, shape, style]);

    const handleClick = (event) => {
        if (isDisabled) return;
        if (typeof onClick === 'function') onClick(event);
    };

    return (
        <button
            type="button"
            onClick={handleClick}
            disabled={isDisabled}
            className={className}
            style={computedStyles}
            {...rest}
        >
            {icon}
        </button>
    );
};

export default ButtonIcon;