import React from 'react';
import './Switch.css';

interface SwitchProps {
  checked: boolean;
  onChange: (checked: boolean) => void;
  disabled?: boolean;
  size?: 'sm' | 'md' | 'lg';
  label?: string;
  labelPosition?: 'left' | 'right';
  className?: string;
}

const Switch: React.FC<SwitchProps> = ({
  checked,
  onChange,
  disabled = false,
  size = 'md',
  label,
  labelPosition = 'right',
  className = '',
}) => {
  const handleToggle = () => {
    if (!disabled) {
      onChange(!checked);
    }
  };

  const switchClasses = [
    'switch',
    `switch-${size}`,
    checked ? 'switch-checked' : '',
    disabled ? 'switch-disabled' : '',
    className,
  ].filter(Boolean).join(' ');

  const labelClasses = [
    'switch-label',
    `switch-label-${labelPosition}`,
    disabled ? 'switch-label-disabled' : '',
  ].filter(Boolean).join(' ');

  return (
    <div className="switch-container">
      {label && labelPosition === 'left' && (
        <label className={labelClasses}>{label}</label>
      )}
      
      <button
        className={switchClasses}
        onClick={handleToggle}
        disabled={disabled}
        type="button"
        role="switch"
        aria-checked={checked}
        aria-label={label || 'Toggle switch'}
      >
        <div className="switch-track">
          <div className="switch-thumb" />
        </div>
      </button>

      {label && labelPosition === 'right' && (
        <label className={labelClasses}>{label}</label>
      )}
    </div>
  );
};

export default Switch;
