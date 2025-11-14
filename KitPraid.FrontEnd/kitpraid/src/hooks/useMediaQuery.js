import { useState, useEffect } from 'react';

/**
 * Custom hook for media queries
 * @param {string} query - Media query string (e.g., '(min-width: 768px)')
 */
export const useMediaQuery = (query) => {
  const [matches, setMatches] = useState(() => {
    if (typeof window !== 'undefined') {
      return window.matchMedia(query).matches;
    }
    return false;
  });

  useEffect(() => {
    if (typeof window === 'undefined') return;

    const mediaQuery = window.matchMedia(query);
    const handler = (event) => setMatches(event.matches);
    
    // Set initial value
    setMatches(mediaQuery.matches);
    
    // Add listener
    mediaQuery.addEventListener('change', handler);
    
    return () => {
      mediaQuery.removeEventListener('change', handler);
    };
  }, [query]);

  return matches;
};

