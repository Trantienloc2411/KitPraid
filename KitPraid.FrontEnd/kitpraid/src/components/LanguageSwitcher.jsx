import React, { useState, useEffect, useRef, useMemo } from 'react';
import { useLanguage } from '../hooks/useLanguage';

// Flag SVG Components
const VietnamFlag = ({ size = 20 }) => (
  <svg width={size} height={Math.round(size * 0.67)} viewBox="0 0 30 20" style={{ display: 'block', flexShrink: 0, borderRadius: '2px', overflow: 'hidden' }}>
    <rect width="30" height="20" fill="#DA020E"/>
    <path d="M15 5 L17 9 L21 9 L17.5 12 L18.5 16 L15 13.5 L11.5 16 L12.5 12 L9 9 L13 9 Z" fill="#FFFE00"/>
  </svg>
);

const UKFlag = ({ size = 20 }) => (
  <svg width={size} height={Math.round(size * 0.67)} viewBox="0 0 30 20" style={{ display: 'block', flexShrink: 0, borderRadius: '2px', overflow: 'hidden' }}>
    <rect width="30" height="20" fill="#012169"/>
    <path d="M0 0 L30 20 M30 0 L0 20" stroke="#FFF" strokeWidth="2.5"/>
    <path d="M0 10 L30 10 M15 0 L15 20" stroke="#FFF" strokeWidth="3"/>
    <path d="M0 0 L30 20 M30 0 L0 20" stroke="#C8102E" strokeWidth="1.5"/>
    <path d="M0 10 L30 10 M15 0 L15 20" stroke="#C8102E" strokeWidth="2"/>
  </svg>
);

const ArrowIcon = ({ open, color = '#FFFFFF', size = 14 }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" aria-hidden="true" style={{ display: 'block', flexShrink: 0 }}>
    {open ? (
      <path d="M6 15l6-6 6 6" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
    ) : (
      <path d="M6 9l6 6 6-6" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
    )}
  </svg>
);

const LanguageSwitcher = ({
  size = 'sm',
  className = '',
  style = {},
}) => {
  const { language, setLanguage } = useLanguage();
  const [open, setOpen] = useState(false);
  const ref = useRef(null);

  const languages = useMemo(() => [
    { 
      value: 'vi', 
      label: 'Tiếng Việt',
      flag: <VietnamFlag size={20} />
    },
    { 
      value: 'en', 
      label: 'English',
      flag: <UKFlag size={20} />
    },
  ], []);

  const currentLanguage = useMemo(() => 
    languages.find(lang => lang.value === language) || languages[0],
    [language, languages]
  );

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { height: 36, fontSize: 14, paddingX: 10, gap: 6, icon: 14 },
      md: { height: 40, fontSize: 15, paddingX: 12, gap: 8, icon: 16 },
      lg: { height: 44, fontSize: 16, paddingX: 14, gap: 10, icon: 18 },
    };
    return map[size] || map.sm;
  }, [size]);

  const handleLanguageChange = (langValue) => {
    setLanguage(langValue);
    setOpen(false);
  };

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (ref.current && !ref.current.contains(event.target)) {
        setOpen(false);
      }
    };

    if (open) {
      document.addEventListener('mousedown', handleClickOutside);
      return () => document.removeEventListener('mousedown', handleClickOutside);
    }
  }, [open]);

  const buttonStyle = {
    display: 'flex',
    alignItems: 'center',
    gap: sizeStyles.gap,
    padding: `0 ${sizeStyles.paddingX}px`,
    height: `${sizeStyles.height}px`,
    backgroundColor: 'transparent',
    border: '1px solid rgba(255, 255, 255, 0.3)',
    borderRadius: '6px',
    color: '#FFFFFF',
    fontSize: `${sizeStyles.fontSize}px`,
    cursor: 'pointer',
    transition: 'all 200ms ease',
    whiteSpace: 'nowrap',
    ...style,
  };

  const listStyle = {
    position: 'absolute',
    top: `calc(100% + 8px)`,
    right: 0,
    backgroundColor: '#FFFFFF',
    border: '1px solid #E5E7EB',
    borderRadius: '8px',
    boxShadow: '0 4px 12px rgba(0, 0, 0, 0.15)',
    minWidth: '140px',
    zIndex: 1000,
    overflow: 'hidden',
  };

  const optionStyle = {
    display: 'flex',
    alignItems: 'center',
    gap: '8px',
    padding: '10px 14px',
    cursor: 'pointer',
    transition: 'background-color 150ms ease',
    fontSize: '14px',
    color: '#111827',
  };

  return (
    <div 
      ref={ref}
      className={`language-switcher ${className}`}
      style={{ position: 'relative', display: 'inline-block' }}
    >
      <button
        type="button"
        onClick={() => setOpen(!open)}
        style={buttonStyle}
        aria-label="Change language"
        aria-expanded={open}
      >
        {currentLanguage.flag}
        <span>{currentLanguage.label}</span>
        <ArrowIcon open={open} color="#FFFFFF" size={sizeStyles.icon} />
      </button>

      {open && (
        <div style={listStyle}>
          {languages.map((lang) => (
            <div
              key={lang.value}
              onClick={() => handleLanguageChange(lang.value)}
              style={{
                ...optionStyle,
                backgroundColor: lang.value === language ? '#F3F4F6' : 'transparent',
              }}
              onMouseEnter={(e) => {
                if (lang.value !== language) {
                  e.currentTarget.style.backgroundColor = '#F9FAFB';
                }
              }}
              onMouseLeave={(e) => {
                if (lang.value !== language) {
                  e.currentTarget.style.backgroundColor = 'transparent';
                }
              }}
            >
              {lang.flag}
              <span>{lang.label}</span>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default LanguageSwitcher;

