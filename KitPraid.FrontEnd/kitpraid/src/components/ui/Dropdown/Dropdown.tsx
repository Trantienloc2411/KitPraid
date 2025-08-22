import React, { useState, useRef, useEffect } from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';
import './Dropdown.css';

interface DropdownOption {
  value: string;
  label: string;
  icon?: React.ReactNode;
}

interface DropdownProps {
  options: DropdownOption[];
  value?: string;
  placeholder?: string;
  onChange?: (value: string) => void;
  disabled?: boolean;
  size?: 'sm' | 'md' | 'lg';
  fullWidth?: boolean;
  className?: string;
}

const Dropdown: React.FC<DropdownProps> = ({
  options,
  value,
  placeholder = 'Select an option',
  onChange,
  disabled = false,
  size = 'md',
  fullWidth = false,
  className = '',
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState<DropdownOption | null>(
    value ? options.find(opt => opt.value === value) || null : null
  );
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  useEffect(() => {
    if (value) {
      const option = options.find(opt => opt.value === value);
      setSelectedOption(option || null);
    }
  }, [value, options]);

  const handleToggle = () => {
    if (!disabled) {
      setIsOpen(!isOpen);
    }
  };

  const handleSelect = (option: DropdownOption) => {
    setSelectedOption(option);
    setIsOpen(false);
    onChange?.(option.value);
  };

  const dropdownClasses = [
    'dropdown',
    `dropdown-${size}`,
    fullWidth ? 'dropdown-full' : '',
    disabled ? 'dropdown-disabled' : '',
    className,
  ].filter(Boolean).join(' ');

  const triggerClasses = [
    'dropdown-trigger',
    `dropdown-trigger-${size}`,
    isOpen ? 'dropdown-trigger-open' : '',
  ].filter(Boolean).join(' ');

  return (
    <div className={dropdownClasses} ref={dropdownRef}>
      <button
        className={triggerClasses}
        onClick={handleToggle}
        disabled={disabled}
        type="button"
      >
        <div className="dropdown-selected">
          {selectedOption ? (
            <>
              {selectedOption.icon && (
                <span className="dropdown-icon">{selectedOption.icon}</span>
              )}
              <span className="dropdown-label">{selectedOption.label}</span>
            </>
          ) : (
            <span className="dropdown-placeholder">{placeholder}</span>
          )}
        </div>
        <span className="dropdown-arrow">
          {isOpen ? <FaChevronUp /> : <FaChevronDown />}
        </span>
      </button>

      {isOpen && (
        <div className="dropdown-menu">
          {options.map((option) => (
            <button
              key={option.value}
              className={`dropdown-option ${
                selectedOption?.value === option.value ? 'dropdown-option-selected' : ''
              }`}
              onClick={() => handleSelect(option)}
              type="button"
            >
              {option.icon && (
                <span className="dropdown-option-icon">{option.icon}</span>
              )}
              <span className="dropdown-option-label">{option.label}</span>
            </button>
          ))}
        </div>
      )}
    </div>
  );
};

export default Dropdown;
