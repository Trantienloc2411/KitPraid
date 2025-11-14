import React, { useEffect, useMemo, useRef, useState } from 'react';
import { getColor } from '../theme/colors';

const ArrowIcon = ({ open, color = '#9CA3AF', size = 18 }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" aria-hidden="true" style={{ display: 'block' }}>
    {open ? (
      <path d="M6 15l6-6 6 6" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
    ) : (
      <path d="M6 9l6 6 6-6" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
    )}
  </svg>
);

const CheckIcon = ({ color = '#111827', size = 18 }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" aria-hidden="true" style={{ display: 'block' }}>
    <path d="M20 6l-11 11-5-5" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
  </svg>
);

/**
 * Dropdown component
 * Props:
 * - options: Array<{ label: string, value: string | number, disabled?: boolean }>
 * - value: selected value (controlled)
 * - defaultValue: default selected value (uncontrolled)
 * - onChange: (value) => void
 * - placeholder: string
 * - size: 'sm' | 'md' | 'lg'
 * - variant: 'default' | 'success' | 'warning' | 'error'
 * - disabled: boolean
 * - style, className
 */
const Dropdown = ({
  options = [],
  value,
  defaultValue,
  onChange,
  placeholder = 'Select...',
  size = 'md',
  variant = 'default',
  disabled = false,
  style = {},
  className = '',
  listMaxHeight = 240,
}) => {
  const [open, setOpen] = useState(false);
  const [hoverIdx, setHoverIdx] = useState(-1);
  const [internalValue, setInternalValue] = useState(defaultValue);
  const isControlled = value !== undefined;
  const selectedValue = isControlled ? value : internalValue;

  const ref = useRef(null);
  const listRef = useRef(null);

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { height: 38, fontSize: 14, radius: 8, paddingX: 14, icon: 18 },
      md: { height: 48, fontSize: 16, radius: 10, paddingX: 16, icon: 18 },
      lg: { height: 56, fontSize: 18, radius: 12, paddingX: 18, icon: 20 },
    };
    return map[size] || map.md;
  }, [size]);

  const palette = useMemo(() => {
    const base = {
      border: '#E5E7EB',
      text: '#111827',
      placeholder: '#9CA3AF',
      background: '#FFFFFF',
      focus: getColor('primary', 500),
      hover: '#F3F4F6',
      shadow: '0 12px 30px rgba(0,0,0,0.18)',
    };
    if (variant === 'success') return { ...base, border: getColor('success', 500) };
    if (variant === 'warning') return { ...base, border: getColor('warning', 500) };
    if (variant === 'error') return { ...base, border: getColor('danger', 500) };
    return base;
  }, [variant]);

  const normalizedOptions = useMemo(() => {
    return (options || []).map((opt) => {
      if (opt && typeof opt === 'object' && ('label' in opt || 'value' in opt)) {
        return { label: String(opt.label ?? opt.value), value: opt.value, disabled: !!opt.disabled };
      }
      return { label: String(opt), value: opt };
    });
  }, [options]);

  const selectedOption = useMemo(() => normalizedOptions.find(o => o.value === selectedValue), [normalizedOptions, selectedValue]);

  const toggleOpen = () => {
    if (disabled) return;
    setOpen(o => !o);
  };

  const close = () => setOpen(false);

  useEffect(() => {
    const onClickOutside = (e) => {
      if (!ref.current) return;
      if (!ref.current.contains(e.target)) close();
    };
    if (open) document.addEventListener('mousedown', onClickOutside);
    return () => document.removeEventListener('mousedown', onClickOutside);
  }, [open]);

  const handleSelect = (opt) => {
    if (opt.disabled) return;
    if (!isControlled) setInternalValue(opt.value);
    if (typeof onChange === 'function') onChange(opt.value);
    close();
  };

  // Keyboard interactions
  const onKeyDown = (e) => {
    if (disabled) return;
    const enabledOptions = normalizedOptions.filter(o => !o.disabled);
    const currentIndex = enabledOptions.findIndex(o => o.value === selectedValue);
    switch (e.key) {
      case 'Enter':
      case ' ': // Space
        e.preventDefault();
        if (!open) { setOpen(true); return; }
        if (hoverIdx >= 0 && hoverIdx < normalizedOptions.length) handleSelect(normalizedOptions[hoverIdx]);
        break;
      case 'Escape':
        close();
        break;
      case 'ArrowDown': {
        e.preventDefault();
        const idx = open ? Math.min(normalizedOptions.length - 1, (hoverIdx < 0 ? currentIndex : hoverIdx) + 1) : -1;
        setOpen(true);
        setHoverIdx(idx);
        scrollIntoView(idx);
        break;
      }
      case 'ArrowUp': {
        e.preventDefault();
        const idx = open ? Math.max(0, (hoverIdx < 0 ? (currentIndex < 0 ? 0 : currentIndex) : hoverIdx) - 1) : -1;
        setOpen(true);
        setHoverIdx(idx);
        scrollIntoView(idx);
        break;
      }
      default:
        break;
    }
  };

  const scrollIntoView = (idx) => {
    if (!listRef.current || idx < 0) return;
    const el = listRef.current.children[idx];
    if (el && el.scrollIntoView) el.scrollIntoView({ block: 'nearest' });
  };

  const containerStyles = {
    position: 'relative',
    width: '100%',
    maxWidth: '100%',
  };

  const triggerStyles = {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    gap: 10,
    width: '100%',
    minHeight: sizeStyles.height,
    borderRadius: sizeStyles.radius,
    border: `2px solid ${open ? palette.focus : palette.border}`,
    backgroundColor: '#FFFFFF',
    padding: `0 ${sizeStyles.paddingX}px`,
    fontSize: sizeStyles.fontSize,
    color: selectedOption ? palette.text : palette.placeholder,
    cursor: disabled ? 'not-allowed' : 'pointer',
    opacity: disabled ? 0.6 : 1,
    boxShadow: open ? '0 0 0 2px ' + palette.focus + '22' : 'none',
    transition: 'border-color 150ms ease, box-shadow 150ms ease',
  };

  const listStyles = {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 'calc(100% + 8px)',
    zIndex: 1000,
    background: '#FFFFFF',
    border: '1px solid #E5E7EB',
    borderRadius: 3,
    boxShadow: '0 24px 40px rgba(0,0,0,0.25)',
    overflow: 'auto',
    maxHeight: listMaxHeight,
  };

  return (
    <div ref={ref} className={className} style={{ ...containerStyles, ...style }}>
      <button
        type="button"
        aria-haspopup="listbox"
        aria-expanded={open}
        onClick={toggleOpen}
        onKeyDown={onKeyDown}
        disabled={disabled}
        style={triggerStyles}
      >
        <span style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
          {selectedOption ? selectedOption.label : placeholder}
        </span>
        <ArrowIcon open={open} color={palette.placeholder} size={sizeStyles.icon} />
      </button>

      {open && (
        <ul ref={listRef} role="listbox" tabIndex={-1} style={listStyles}>
          {normalizedOptions.map((opt, idx) => {
            const selected = selectedValue === opt.value;
            const hovered = hoverIdx === idx;
            const itemStyle = {
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              gap: 10,
              padding: '14px 16px',
              backgroundColor: hovered ? '#F3F4F6' : 'transparent',
              color: opt.disabled ? '#9CA3AF' : '#111827',
              cursor: opt.disabled ? 'not-allowed' : 'pointer',
            };
            const dividerStyle = { height: 1, background: '#F3F4F6' };
            return (
              <React.Fragment key={String(opt.value)}>
                <li
                  role="option"
                  aria-selected={selected}
                  onMouseEnter={() => setHoverIdx(idx)}
                  onMouseLeave={() => setHoverIdx(-1)}
                  onClick={() => handleSelect(opt)}
                  style={itemStyle}
                >
                  <span style={{ fontSize: sizeStyles.fontSize }}>{opt.label}</span>
                  {selected && <CheckIcon color={getColor('success', 500)} size={18} />}
                </li>
                {idx !== normalizedOptions.length - 1 && <div style={dividerStyle} />}
              </React.Fragment>
            );
          })}
        </ul>
      )}
    </div>
  );
};

export default Dropdown;


