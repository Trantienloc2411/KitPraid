import React from 'react';
import Button from '../ui/Button';
import './HeroSection.css';

interface HeroSectionProps {
  title: string;
  subtitle: string;
  description: string;
  primaryButtonText: string;
  secondaryButtonText?: string;
  backgroundImage: string;
  onPrimaryClick?: () => void;
  onSecondaryClick?: () => void;
}

const HeroSection: React.FC<HeroSectionProps> = ({
  title,
  subtitle,
  description,
  primaryButtonText,
  secondaryButtonText,
  backgroundImage,
  onPrimaryClick,
  onSecondaryClick,
}) => {
  return (
    <section className="hero-section">
      <div 
        className="hero-background"
        style={{ backgroundImage: `url(${backgroundImage})` }}
      >
        <div className="hero-overlay"></div>
      </div>
      
      <div className="hero-content">
        <div className="hero-text">
          <span className="hero-subtitle">{subtitle}</span>
          <h1 className="hero-title">{title}</h1>
          <p className="hero-description">{description}</p>
          
          <div className="hero-buttons">
            <Button
              variant="primary"
              size="lg"
              onClick={onPrimaryClick}
              className="hero-primary-btn"
            >
              {primaryButtonText}
            </Button>
            
            {secondaryButtonText && (
              <Button
                variant="outline"
                size="lg"
                onClick={onSecondaryClick}
                className="hero-secondary-btn"
              >
                {secondaryButtonText}
              </Button>
            )}
          </div>
        </div>
        
        <div className="hero-visual">
          <div className="hero-stats">
            <div className="stat-item">
              <span className="stat-number">50K+</span>
              <span className="stat-label">Happy Customers</span>
            </div>
            <div className="stat-item">
              <span className="stat-number">100+</span>
              <span className="stat-label">Brands</span>
            </div>
            <div className="stat-item">
              <span className="stat-number">24/7</span>
              <span className="stat-label">Support</span>
            </div>
          </div>
        </div>
      </div>
      
      {/* Floating Elements */}
      <div className="floating-elements">
        <div className="floating-card floating-card-1">
          <div className="card-icon">🚀</div>
          <div className="card-content">
            <h4>Fast Delivery</h4>
            <p>Free shipping on orders over $50</p>
          </div>
        </div>
        
        <div className="floating-card floating-card-2">
          <div className="card-icon">🛡️</div>
          <div className="card-content">
            <h4>Secure Payment</h4>
            <p>100% secure payment</p>
          </div>
        </div>
      </div>
    </section>
  );
};

export default HeroSection;
