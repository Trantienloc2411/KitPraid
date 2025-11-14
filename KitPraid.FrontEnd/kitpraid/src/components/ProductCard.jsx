import React, { useMemo, useState } from 'react';
import { getColor } from '../theme/colors';

// Default icons for hover overlay
const HeartIcon = ({ size = 20, color = '#111827' }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
  </svg>
);

const CartIcon = ({ size = 20, color = '#111827' }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <circle cx="9" cy="21" r="1" />
    <circle cx="20" cy="21" r="1" />
    <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h10.72a2 2 0 0 0 2-1.61L23 6H6" />
  </svg>
);

const EyeIcon = ({ size = 20, color = '#111827' }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z" />
    <circle cx="12" cy="12" r="3" />
  </svg>
);

const StarIcon = ({ filled = false, size = 16, color = '#FA8232' }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill={filled ? color : 'none'} stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" />
  </svg>
);

/**
 * ProductCard component
 * Props:
 * - imageUrl: string
 * - title: string
 * - price: string | number
 * - hasRating: boolean
 * - ratingStars: number (0-5)
 * - ratingCount: number | string
 * - badges: { label: string, color?: string, position?: 'top-left' | 'top-right' | 'bottom-left' | 'bottom-right' }
 * - direction: 'horizontal' | 'vertical' (default: 'vertical')
 * - hoverIcons: boolean | { heart?: boolean, cart?: boolean, eye?: boolean } | custom icons
 * - onHeartClick, onCartClick, onEyeClick: functions
 * - className, style
 */
const ProductCard = ({
  imageUrl,
  title,
  price,
  hasRating = false,
  ratingStars = 0,
  ratingCount,
  badges = {},
  direction = 'vertical',
  hoverIcons = true,
  onHeartClick,
  onCartClick,
  onEyeClick,
  className = '',
  style = {},
}) => {
  const [isHovered, setIsHovered] = useState(false);

  const cardStyles = useMemo(() => {
    const base = {
      display: 'flex',
      backgroundColor: '#FFFFFF',
      borderRadius: 10,
      overflow: 'hidden',
      boxShadow: isHovered ? '0 8px 24px rgba(0,0,0,0.12)' : '0 2px 8px rgba(0,0,0,0.08)',
      transition: 'box-shadow 200ms ease, transform 200ms ease',
      cursor: 'pointer',
      position: 'relative',
      width: '100%',
      maxWidth: '100%',
    };

    if (direction === 'horizontal') {
      return {
        ...base,
        flexDirection: 'row',
        alignItems: 'center',
      };
    }
    return {
      ...base,
      flexDirection: 'column',
    };
  }, [direction, isHovered]);

  const imageContainerStyles = useMemo(() => {
    const base = {
      position: 'relative',
      backgroundColor: direction === 'vertical' && isHovered ? '#F3F4F6' : '#FFFFFF',
      transition: 'background-color 200ms ease',
      overflow: 'hidden',
    };

    if (direction === 'horizontal') {
      return {
        ...base,
        width: '40%',
        minWidth: 120,
        flexShrink: 0,
      };
    }
    return {
      ...base,
      width: '100%',
      aspectRatio: '4/3',
    };
  }, [direction, isHovered]);

  const imageStyles = {
    width: '100%',
    height: '100%',
    objectFit: 'contain',
    display: 'block',
  };

  const contentStyles = useMemo(() => {
    if (direction === 'horizontal') {
      return {
        flex: 1,
        padding: '16px 20px',
        display: 'flex',
        flexDirection: 'column',
        gap: 8,
      };
    }
    return {
      padding: '16px 20px',
      display: 'flex',
      flexDirection: 'column',
      gap: 8,
    };
  }, [direction]);

  const badgeStyles = useMemo(() => {
    const positions = {
      'top-left': { top: 12, left: 12 },
      'top-right': { top: 12, right: 12 },
      'bottom-left': { bottom: 12, left: 12 },
      'bottom-right': { bottom: 12, right: 12 },
    };
    return {
      position: 'absolute',
      ...positions[badges.position || 'top-left'],
      backgroundColor: badges.color || getColor('warning', 500),
      color: '#FFFFFF',
      padding: '6px 12px',
      borderRadius: 6,
      fontSize: 12,
      fontWeight: 700,
      zIndex: 10,
    };
  }, [badges]);

  const overlayStyles = {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: 'rgba(0,0,0,0.4)',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 12,
    opacity: isHovered ? 1 : 0,
    transition: 'opacity 200ms ease',
    zIndex: 5,
  };

  const iconButtonStyles = {
    width: 44,
    height: 44,
    borderRadius: '50%',
    backgroundColor: '#FFFFFF',
    border: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    cursor: 'pointer',
    transition: 'transform 150ms ease, background-color 150ms ease',
  };

  const renderHoverIcons = () => {
    if (!hoverIcons || direction !== 'vertical') return null;

    const showHeart = hoverIcons === true || (typeof hoverIcons === 'object' && hoverIcons.heart !== false);
    const showCart = hoverIcons === true || (typeof hoverIcons === 'object' && hoverIcons.cart !== false);
    const showEye = hoverIcons === true || (typeof hoverIcons === 'object' && hoverIcons.eye !== false);

    if (typeof hoverIcons === 'object' && hoverIcons.custom) {
      return hoverIcons.custom;
    }

    return (
      <div style={overlayStyles}>
        {showHeart && (
          <button
            type="button"
            onClick={(e) => {
              e.stopPropagation();
              if (onHeartClick) onHeartClick(e);
            }}
            style={iconButtonStyles}
            onMouseEnter={(e) => (e.currentTarget.style.transform = 'scale(1.1)')}
            onMouseLeave={(e) => (e.currentTarget.style.transform = 'scale(1)')}
          >
            <HeartIcon />
          </button>
        )}
        {showCart && (
          <button
            type="button"
            onClick={(e) => {
              e.stopPropagation();
              if (onCartClick) onCartClick(e);
            }}
            style={iconButtonStyles}
            onMouseEnter={(e) => (e.currentTarget.style.transform = 'scale(1.1)')}
            onMouseLeave={(e) => (e.currentTarget.style.transform = 'scale(1)')}
          >
            <CartIcon />
          </button>
        )}
        {showEye && (
          <button
            type="button"
            onClick={(e) => {
              e.stopPropagation();
              if (onEyeClick) onEyeClick(e);
            }}
            style={{ ...iconButtonStyles, backgroundColor: getColor('primary', 500) }}
            onMouseEnter={(e) => (e.currentTarget.style.transform = 'scale(1.1)')}
            onMouseLeave={(e) => (e.currentTarget.style.transform = 'scale(1)')}
          >
            <EyeIcon color="#FFFFFF" />
          </button>
        )}
      </div>
    );
  };

  const renderRating = () => {
    if (!hasRating) return null;
    const stars = Array.from({ length: 5 }, (_, i) => (
      <StarIcon key={i} filled={i < Math.floor(ratingStars)} size={16} />
    ));
    return (
      <div style={{ display: 'flex', alignItems: 'center', gap: 4 }}>
        {stars}
        {ratingCount && (
          <span style={{ fontSize: 14, color: '#6B7280', marginLeft: 4 }}>
            ({ratingCount})
          </span>
        )}
      </div>
    );
  };

  return (
    <div
      className={className}
      style={{ ...cardStyles, ...style }}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div style={imageContainerStyles}>
        {imageUrl && <img src={imageUrl} alt={title || 'Product'} style={imageStyles} />}
        {badges.label && <div style={badgeStyles}>{badges.label}</div>}
        {renderHoverIcons()}
      </div>
      <div style={contentStyles}>
        {hasRating && renderRating()}
        {title && (
          <h3 style={{ fontSize: 16, fontWeight: 600, color: '#111827', margin: 0, lineHeight: 1.4 }}>
            {title}
          </h3>
        )}
        {price && (
          <div style={{ fontSize: 18, fontWeight: 700, color: getColor('secondary', 500) }}>
            ${typeof price === 'number' ? price.toFixed(2) : price}
          </div>
        )}
      </div>
    </div>
  );
};

export default ProductCard;