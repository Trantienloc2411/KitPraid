import { useMemo, useState } from 'react';
import { getColor } from '../theme/colors';

const Link = ({
    label,
    children,
    variant = 'primary',
    color,
    isDisabled = false,
    onClick,
    size = 'md',
    style = {},
    hasArrow = false,
    hasBorder = false,
    borderStyle = 'solid', // 'solid' | 'dotted'
    hasUnderline = false,
    className = '',
    href,
    ...rest
}) => {
    const [isHovered, setIsHovered] = useState(false);

    const sizeStyles = useMemo(() => {
        const mapping = {
            sm: { fontSize: 12, padding: '6px 12px', gap: 6 },
            md: { fontSize: 14, padding: '10px 16px', gap: 8 },
            lg: { fontSize: 16, padding: '14px 20px', gap: 10 },
        };
        return mapping[size] || mapping.md;
    }, [size]);
    
    const resolvedColor = useMemo(() => {
        const base = color || getColor(variant, isDisabled ? 200 : 500);
        return base;
    }, [variant, color, isDisabled]);
    
    const computedStyles = useMemo(() => {
        const borderColor = hasBorder ? resolvedColor : 'transparent';
        const border = hasBorder ? `1px ${borderStyle} ${borderColor}` : 'none';
        
        // Parse padding for responsive values
        const paddingParts = sizeStyles.padding.split(' ');
        const paddingY = paddingParts[0] || '10px';
        const paddingX = paddingParts[1] || paddingParts[0] || '16px';
        const paddingXNum = parseInt(paddingX);
        
        return {
            display: 'inline-flex',
            alignItems: 'center',
            fontSize: `clamp(${sizeStyles.fontSize - 1}px, 1.5vw, ${sizeStyles.fontSize}px)`,
            padding: `${paddingY} clamp(${Math.max(paddingXNum - 4, 4)}px, 2vw, ${paddingX})`,
            color: resolvedColor,
            textDecoration: 'none',
            border,
            backgroundColor: 'transparent',
            cursor: isDisabled ? 'not-allowed' : 'pointer',
            opacity: isDisabled ? 0.5 : (isHovered ? 0.9 : 1),
            transition: 'opacity 200ms ease',
            minWidth: '44px', // Minimum touch target size
            minHeight: '44px',
            width: 'fit-content',
            maxWidth: '100%',
            boxSizing: 'border-box',
            wordBreak: 'break-word',
            overflowWrap: 'break-word',
            ...style,
        };
    }, [resolvedColor, hasBorder, borderStyle, sizeStyles, isDisabled, isHovered, style]);

    const handleClick = (event) => {
        if (isDisabled) {
            event.preventDefault();
            return;
        }
        if (typeof onClick === 'function') onClick(event);
    };

    const handleMouseEnter = () => {
        if (!isDisabled) setIsHovered(true);
    };

    const handleMouseLeave = () => {
        setIsHovered(false);
    };

    const IconArrow = () => (
        <svg
            width={sizeStyles.fontSize}
            height={sizeStyles.fontSize}
            viewBox="0 0 24 24"
            fill="none"
            aria-hidden="true"
            focusable="false"
            style={{ display: 'block', flexShrink: 0 }}
        >
            <path 
                d="M5 12h12M13 6l6 6-6 6" 
                stroke={resolvedColor} 
                strokeWidth="2" 
                strokeLinecap="round" 
                strokeLinejoin="round"
            />
        </svg>
    );

    const linkProps = {
        href: isDisabled ? undefined : (href || '#'),
        onClick: handleClick,
        onMouseEnter: handleMouseEnter,
        onMouseLeave: handleMouseLeave,
        className: className,
        style: computedStyles,
        ...(isDisabled && { 'aria-disabled': true, tabIndex: -1 }),
        ...rest,
    };

    const innerContentStyle = useMemo(() => {
        const shouldShowUnderline = isHovered || hasUnderline;
        return {
            display: 'inline-flex',
            alignItems: 'center',
            gap: `clamp(${sizeStyles.gap - 2}px, 1vw, ${sizeStyles.gap}px)`,
            borderBottom: shouldShowUnderline ? `3px solid ${resolvedColor}` : 'none',
            paddingBottom: shouldShowUnderline ? '6px' : '0',
            transition: 'border-bottom 200ms ease, padding-bottom 200ms ease',
            minWidth: 0,
            flexShrink: 1,
            overflow: 'hidden',
            textOverflow: 'ellipsis',
            whiteSpace: 'nowrap',
        };
    }, [isHovered, hasUnderline, resolvedColor, sizeStyles.gap]);

    return (
        <a {...linkProps}>
            <span style={innerContentStyle}>
                {children || label}
                {hasArrow && <IconArrow />}
            </span>
        </a>
    );
};

export default Link;