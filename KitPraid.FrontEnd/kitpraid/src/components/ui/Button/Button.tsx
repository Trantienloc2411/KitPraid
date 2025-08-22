import React from 'react';
import './Button.css';

interface ButtonProps {
  variant?: 'primary' | 'secondary' | 'outline' | 'ghost' | 'danger' | 'success';
  size?: 'sm' | 'md' | 'lg';
  children: React.ReactNode;
  onClick?: () => void;
  disabled?: boolean;
  fullWidth?: boolean;
  className?: string;
  loading?: boolean;
  icon?: React.ReactNode;
  iconPosition?: 'left' | 'right';
}

const Button: React.FC<ButtonProps> = ({
  variant = 'primary',
  size = 'md',
  children,
  onClick,
  disabled = false,
  fullWidth = false,
  className = '',
  loading = false,
  icon,
  iconPosition = 'left',
}) => {
  const buttonClasses = [
    'btn',
    `btn-${variant}`,
    `btn-${size}`,
    fullWidth ? 'btn-full' : '',
    loading ? 'loading' : '',
    className,
  ].filter(Boolean).join(' ');

  const renderIcon = () => {
    if (!icon) return null;
    return <span className={`btn-icon ${iconPosition === 'right' ? 'icon-right' : ''}`}>{icon}</span>;
  };

  return (
    <button
      className={buttonClasses}
      onClick={onClick}
      disabled={disabled || loading}
    >
      {icon && iconPosition === 'left' && renderIcon()}
      {loading ? (
        <span className="loading-spinner"></span>
      ) : (
        children
      )}
      {icon && iconPosition === 'right' && renderIcon()}
    </button>
  );
};

export default Button;
