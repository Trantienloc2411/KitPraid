// Simple color helpers
const clamp = (value, min, max) => Math.min(max, Math.max(min, value));

const hexToRgb = (hex) => {
  const normalized = hex.replace('#', '').trim();
  const bigint = parseInt(normalized.length === 3
    ? normalized.split('').map((c) => c + c).join('')
    : normalized, 16);
  const r = (bigint >> 16) & 255;
  const g = (bigint >> 8) & 255;
  const b = bigint & 255;
  return { r, g, b };
};

const rgbToHex = ({ r, g, b }) => {
  const toHex = (n) => clamp(Math.round(n), 0, 255).toString(16).padStart(2, '0');
  return `#${toHex(r)}${toHex(g)}${toHex(b)}`.toUpperCase();
};

// Lighten color by percentage (0-1)
const lighten = (hex, amount) => {
  const { r, g, b } = hexToRgb(hex);
  const nr = r + (255 - r) * amount;
  const ng = g + (255 - g) * amount;
  const nb = b + (255 - b) * amount;
  return rgbToHex({ r: nr, g: ng, b: nb });
};

export const colors = {
  primary: {
    500: '#FA8232',
    200: lighten('#FA8232', 0.6),
  },
  secondary: {
    500: '#2DA5F3',
    200: lighten('#2DA5F3', 0.6),
  },
  success: {
    500: '#2DB224',
    200: lighten('#2DB224', 0.6),
  },
  warning: {
    500: '#EBC80C',
    200: lighten('#EBC80C', 0.6),
  },
  danger: {
    500: '#EE5858',
    200: lighten('#EE5858', 0.6),
  },
  disabled: {
    500: '#999999',
    200: '#CCCCCC',
    100: '#E6E6E6',
    50: '#F2F2F2',
  },
};

export const getColor = (variant = 'primary', shade = 500) => {
  const group = colors[variant] || colors.primary;
  return group[shade] || group[500];
};

export const getWeight = ({ isBold, isDisabled, base = 500 }) => {
  if (isDisabled) return 200;
  if (isBold) return clamp(base + 100, 100, 900); // e.g., 600 when base 500
  return base; // 500 default
};

export default colors;


