import { useMemo } from 'react';
import { getColor } from '../theme/colors';

const Tag = ({
    label,
    hasBackground, // filled background
    hasBorder = true,
    variant = 'primary',
    size = 'md',
    isDisabled = false,
    color,
    tone = 'light', // 'light' text or 'dark' text theme for outline mode
    style = {},
    className = '',
    ...rest
}) => {
    const sizeStyles = useMemo(() => {
        const mapping = {
            sm: { fontSize: 12, paddingY: 6, paddingX: 12, radius: 3 },
            md: { fontSize: 14, paddingY: 8, paddingX: 16, radius: 3 },
            lg: { fontSize: 16, paddingY: 10, paddingX: 20, radius: 3 },
        };
        return mapping[size] || mapping.md;
    }, [size]);

    const resolvedColor = useMemo(() => {
        const base = color || getColor(variant, isDisabled ? 200 : 500);
        return base;
    }, [variant, color, isDisabled]);

    const computed = useMemo(() => {
        const filled = !!hasBackground;
        const borderColor = hasBorder ? (filled ? resolvedColor : (tone === 'dark' ? '#FFFFFF' : resolvedColor)) : 'transparent';
        const backgroundColor = filled ? resolvedColor : 'transparent';
        const textColor = filled ? '#FFFFFF' : (tone === 'dark' ? '#FFFFFF' : resolvedColor);

        return {
            display: 'inline-flex',
            alignItems: 'center',
            justifyContent: 'center',
            gap: 6,
            padding: `${sizeStyles.paddingY}px ${sizeStyles.paddingX}px`,
            fontSize: sizeStyles.fontSize,
            fontWeight: 700,
            borderRadius: sizeStyles.radius,
            backgroundColor,
            backgroundClip: 'padding-box',
            color: textColor,
            border: hasBorder ? `2px solid ${borderColor}` : '2px solid transparent',
            opacity: isDisabled ? 0.6 : 1,
            userSelect: 'none',
            lineHeight: 1.2,
            boxSizing: 'border-box',
            ...style,
        };
    }, [hasBackground, hasBorder, resolvedColor, sizeStyles, tone, isDisabled, style]);

    return (
        <span style={computed} className={className} {...rest}>
            {label}
        </span>
    );
};

export default Tag;