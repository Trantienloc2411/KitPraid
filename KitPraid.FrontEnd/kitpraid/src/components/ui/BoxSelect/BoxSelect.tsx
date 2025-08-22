import React, { useState } from 'react';
import './BoxSelect.css';

interface BoxSelectOption {
  value: string;
  label: string;
  description?: string;
  icon?: React.ReactNode;
}

interface BoxSelectProps {
  options: BoxSelectOption[];
  value?: string;
  onChange?: (value: string) => void;
  multiple?: boolean;
  disabled?: boolean;
  size?: 'sm' | 'md' | 'lg';
  fullWidth?: boolean;
  className?: string;
}

const BoxSelect: React.FC<BoxSelectProps> = ({
  options,
  value,
  onChange,
  multiple = false,
  disabled = false,
  size = 'md',
  fullWidth = false,
  className = '',
}) => {
  const [selectedValues, setSelectedValues] = useState<string[]>(
    value ? (multiple ? [value] : [value]) : []
  );

  const handleSelect = (optionValue: string) => {
    if (disabled) return;

    if (multiple) {
      const newValues = selectedValues.includes(optionValue)
        ? selectedValues.filter(v => v !== optionValue)
        : [...selectedValues, optionValue];
      
      setSelectedValues(newValues);
      onChange?.(newValues[0] || '');
    } else {
      setSelectedValues([optionValue]);
      onChange?.(optionValue);
    }
  };

  const isSelected = (optionValue: string) => selectedValues.includes(optionValue);

  const boxSelectClasses = [
    'box-select',
    `box-select-${size}`,
    fullWidth ? 'box-select-full' : '',
    disabled ? 'box-select-disabled' : '',
    className,
  ].filter(Boolean).join(' ');

  return (
    <div className={boxSelectClasses}>
      {options.map((option) => (
        <button
          key={option.value}
          className={`box-select-option ${
            isSelected(option.value) ? 'box-select-option-selected' : ''
          }`}
          onClick={() => handleSelect(option.value)}
          disabled={disabled}
          type="button"
        >
          {option.icon && (
            <div className="box-select-option-icon">
              {option.icon}
            </div>
          )}
          
          <div className="box-select-option-content">
            <div className="box-select-option-label">
              {option.label}
            </div>
            {option.description && (
              <div className="box-select-option-description">
                {option.description}
              </div>
            )}
          </div>

          {multiple && (
            <div className="box-select-checkbox">
              <div className={`checkbox ${isSelected(option.value) ? 'checked' : ''}`}>
                {isSelected(option.value) && (
                  <svg viewBox="0 0 16 16" fill="currentColor">
                    <path d="M13.854 3.646a.5.5 0 0 1 0 .708l-7 7a.5.5 0 0 1-.708 0l-3.5-3.5a.5.5 0 1 1 .708-.708L6.5 10.293l6.646-6.647a.5.5 0 0 1 .708 0z"/>
                  </svg>
                )}
              </div>
            </div>
          )}
        </button>
      ))}
    </div>
  );
};

export default BoxSelect;
