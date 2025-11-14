import React, { useEffect, useMemo, useRef, useState } from 'react';
import { getColor } from '../theme/colors';

/**
 * Tabs component
 * Props:
 * - items: Array<{ id: string, label: string, disabled?: boolean }>
 * - value: string (controlled)
 * - defaultValue: string (uncontrolled)
 * - onChange: (id: string) => void
 * - size: 'sm' | 'md' | 'lg'
 * - variant: 'primary' | 'secondary' | 'success' | 'warning' | 'danger'
 * - kind: 'underline' | 'filled' (active style)
 * - className, style
 */
const Tabs = ({
  items = [],
  value,
  defaultValue,
  onChange,
  size = 'md',
  variant = 'primary',
  kind = 'underline',
  className = '',
  style = {},
}) => {
  const isControlled = value !== undefined;
  const [internal, setInternal] = useState(
    defaultValue ?? (items[0] ? items[0].id : undefined)
  );
  const active = isControlled ? value : internal;

  const palette = useMemo(() => {
    return {
      primary: getColor(variant, 500),
      border: '#E5E7EB',
      hover: '#F3F4F6',
      text: '#111827',
      textMuted: '#6B7280',
      fillActive: 'rgba(250,130,50,0.12)', // light primary
    };
  }, [variant]);

  const sizeStyles = useMemo(() => {
    const map = {
      sm: { fontSize: 14, h: 36, px: 12, radius: 3 },
      md: { fontSize: 16, h: 44, px: 14, radius: 3 },
      lg: { fontSize: 18, h: 52, px: 16, radius: 3 },
    };
    return map[size] || map.md;
  }, [size]);

  const onSelect = (id) => {
    if (!isControlled) setInternal(id);
    if (typeof onChange === 'function') onChange(id);
  };

  const onKeyDown = (e) => {
    if (!items.length) return;
    const idx = items.findIndex((i) => i.id === active);
    if (e.key === 'ArrowRight') {
      e.preventDefault();
      const next = items[(idx + 1) % items.length];
      if (next && !next.disabled) onSelect(next.id);
    } else if (e.key === 'ArrowLeft') {
      e.preventDefault();
      const prev = items[(idx - 1 + items.length) % items.length];
      if (prev && !prev.disabled) onSelect(prev.id);
    }
  };

  const container = {
    display: 'flex',
    gap: 10,
    width: '100%',
    flexWrap: 'wrap',
    ...style,
  };

  return (
    <div role="tablist" aria-orientation="horizontal" className={className} style={container} onKeyDown={onKeyDown}>
      {items.map((item) => {
        const selected = item.id === active;
        const common = {
          fontSize: sizeStyles.fontSize,
          height: sizeStyles.h,
          padding: `0 ${sizeStyles.px}px`,
          borderRadius: sizeStyles.radius,
          border: '1px solid ' + palette.border,
          background: '#FFFFFF',

          color: selected ? palette.text : palette.textMuted,
          display: 'inline-flex',
          alignItems: 'center',
          justifyContent: 'center',
          cursor: item.disabled ? 'not-allowed' : 'pointer',
          opacity: item.disabled ? 0.5 : 1,
          transition: 'background-color 150ms ease, color 150ms ease, box-shadow 150ms ease',
          userSelect: 'none',
        };

        const hoverStyle = {
          background: palette.hover,
        };

        const activeStyles =
          kind === 'underline'
            ? {
                boxShadow: selected ? `inset 0 -3px 0 0 ${palette.primary}` : 'none',
              }
            : {
                background: selected ? palette.fillActive : common.background,
                borderColor: selected ? palette.primary : palette.border,
              };

        return (
          <button
            key={item.id}
            role="tab"
            aria-selected={selected}
            disabled={item.disabled}
            onClick={() => !item.disabled && onSelect(item.id)}
            onMouseEnter={(e) => {
              if (item.disabled) return;
              e.currentTarget.style.backgroundColor = palette.hover;
            }}
            onMouseLeave={(e) => {
              if (item.disabled) return;
              e.currentTarget.style.backgroundColor = selected && kind === 'filled' ? palette.fillActive : '#FFFFFF';
            }}
            style={{ ...common, ...activeStyles }}
          >
            {item.label}
          </button>
        );
      })}
    </div>
  );
};

export default Tabs;


