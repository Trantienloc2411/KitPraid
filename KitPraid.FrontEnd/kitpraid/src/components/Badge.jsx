import { useMemo } from 'react';
import { getColor } from '../theme/colors';

const Badge = ({
    label,
    children,
    variant = 'primary',
    color,
    size = 'md',
    isDisabled = false,
    maxWidth = 160,
    truncate = true,
    rounded = 2,
    uppercase = true,
    style = {},
    className = '',
    ...rest
}) => {
    const sizeStyles = useMemo(() => {
        const mapping = {
            sm: { fontSize: 12, paddingY: 4, paddingX: 8, radius: 4 },
            md: { fontSize: 13, paddingY: 6, paddingX: 12, radius: 6 },
            lg: { fontSize: 14, paddingY: 8, paddingX: 14, radius: 8 },
        };
        return mapping[size] || mapping.md;
    }, [size]);

    const resolvedColor = useMemo(() => {
        const base = color || getColor(variant, isDisabled ? 200 : 500);
        return base;
    }, [variant, color, isDisabled]);

    const computedStyles = useMemo(() => {
        const maxWidthValue = typeof maxWidth === 'number' 
            ? `min(${maxWidth}px, 100%)` 
            : maxWidth === 'none' 
                ? 'none' 
                : `min(${maxWidth}, 100%)`;
        
        return {
            display: 'inline-flex',
            alignItems: 'center',
            justifyContent: 'center',
            gap: 6,
            backgroundColor: resolvedColor,
            color: '#FFFFFF',
            borderRadius: rounded ?? sizeStyles.radius,
            padding: `${sizeStyles.paddingY}px clamp(${Math.max(sizeStyles.paddingX - 4, 4)}px, 2vw, ${sizeStyles.paddingX}px)`,
            fontSize: `clamp(${sizeStyles.fontSize - 1}px, 1.5vw, ${sizeStyles.fontSize}px)`,
            fontWeight: 700,
            textTransform: uppercase ? 'uppercase' : 'none',
            lineHeight: 1.2,
            maxWidth: maxWidthValue,
            width: 'fit-content',
            minWidth: 0,
            overflow: 'hidden',
            whiteSpace: truncate ? 'nowrap' : 'normal',
            textOverflow: truncate ? 'ellipsis' : 'clip',
            userSelect: 'none',
            boxSizing: 'border-box',
            wordBreak: truncate ? 'normal' : 'break-word',
            ...style,
        };
    }, [resolvedColor, sizeStyles, rounded, uppercase, maxWidth, truncate, style]);

    return (
        <span style={computedStyles} className={className} {...rest}>
            {children || label}
        </span>
    );
};

export default Badge;