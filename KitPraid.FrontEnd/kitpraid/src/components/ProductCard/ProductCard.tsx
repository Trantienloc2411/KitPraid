import React, { useState } from 'react';
import { FaHeart, FaShoppingCart, FaStar, FaEye } from 'react-icons/fa';
import './ProductCard.css';

interface ProductCardProps {
  id: string;
  name: string;
  price: number;
  originalPrice?: number;
  image: string;
  rating: number;
  reviewCount: number;
  discount?: number;
  isNew?: boolean;
  isSale?: boolean;
  variant?: 'vertical' | 'horizontal';
  onAddToCart?: (id: string) => void;
  onAddToWishlist?: (id: string) => void;
  onQuickView?: (id: string) => void;
  className?: string;
}

const ProductCard: React.FC<ProductCardProps> = ({
  id,
  name,
  price,
  originalPrice,
  image,
  rating,
  reviewCount,
  discount,
  isNew = false,
  isSale = false,
  variant = 'vertical',
  onAddToCart,
  onAddToWishlist,
  onQuickView,
  className = '',
}) => {
  const [isWishlisted, setIsWishlisted] = useState(false);
  const [isHovered, setIsHovered] = useState(false);

  const handleWishlistToggle = () => {
    setIsWishlisted(!isWishlisted);
    onAddToWishlist?.(id);
  };

  const handleAddToCart = () => {
    onAddToCart?.(id);
  };

  const handleQuickView = () => {
    onQuickView?.(id);
  };

  const formatPrice = (price: number) => {
    // For Vietnamese prices, we'll use a more compact format
    if (price >= 1000000) {
      return `${(price / 1000000).toFixed(1)}M ₫`;
    } else if (price >= 1000) {
      return `${(price / 1000).toFixed(0)}K ₫`;
    } else {
      return `${price} ₫`;
    }
  };

  const cardClasses = [
    'product-card',
    `product-card-${variant}`,
    className,
  ].filter(Boolean).join(' ');

  return (
    <div
      className={cardClasses}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      {/* Product Image Container */}
      <div className="product-image-container">
        <img src={image} alt={name} className="product-image" />
        
        {/* Badges */}
        <div className="product-badges">
          {isNew && <span className="badge badge-new">New</span>}
          {isSale && <span className="badge badge-sale">Sale</span>}
          {discount && <span className="badge badge-discount">-{discount}%</span>}
        </div>

        {/* Quick Actions */}
        <div className={`quick-actions ${isHovered ? 'visible' : ''}`}>
          <button
            className="action-btn wishlist-btn"
            onClick={handleWishlistToggle}
            aria-label="Add to wishlist"
          >
            <FaHeart className={isWishlisted ? 'filled' : ''} />
          </button>
          
          <button
            className="action-btn quick-view-btn"
            onClick={handleQuickView}
            aria-label="Quick view"
          >
            <FaEye />
          </button>
        </div>

        {/* Add to Cart Button - Only for vertical variant */}
        {variant === 'vertical' && (
          <button
            className={`add-to-cart-btn ${isHovered ? 'visible' : ''}`}
            onClick={handleAddToCart}
          >
            <FaShoppingCart />
            <span>Add to Cart</span>
          </button>
        )}
      </div>

      {/* Product Info */}
      <div className="product-info">
        <h3 className="product-name">{name}</h3>
        
        {/* Rating */}
        <div className="product-rating">
          <div className="stars">
            {[...Array(5)].map((_, index) => (
              <FaStar
                key={index}
                className={index < Math.floor(rating) ? 'star-filled' : 'star-empty'}
              />
            ))}
          </div>
          <span className="review-count">({reviewCount})</span>
        </div>

        {/* Price */}
        <div className="product-price">
          <span className="current-price" title={formatPrice(price)}>
            {formatPrice(price)}
          </span>
          {originalPrice && (
            <span className="original-price" title={formatPrice(originalPrice)}>
              {formatPrice(originalPrice)}
            </span>
          )}
        </div>

        {/* Add to Cart Button - For horizontal variant */}
        {variant === 'horizontal' && (
          <button
            className="add-to-cart-btn-horizontal"
            onClick={handleAddToCart}
          >
            <FaShoppingCart />
            <span>Add to Cart</span>
          </button>
        )}
      </div>
    </div>
  );
};

export default ProductCard;
