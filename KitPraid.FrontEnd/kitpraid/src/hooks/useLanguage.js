import { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

/**
 * Custom hook for language management
 * @returns {Object} { language, setLanguage, t }
 */
export const useLanguage = () => {
  const { i18n, t } = useTranslation();
  const [language, setLanguageState] = useState(i18n.language);

  useEffect(() => {
    const handleLanguageChange = (lng) => {
      setLanguageState(lng);
      localStorage.setItem('language', lng);
    };

    i18n.on('languageChanged', handleLanguageChange);
    return () => {
      i18n.off('languageChanged', handleLanguageChange);
    };
  }, [i18n]);

  const setLanguage = (lng) => {
    i18n.changeLanguage(lng);
    localStorage.setItem('language', lng);
  };

  return {
    language,
    setLanguage,
    t,
    isVietnamese: language === 'vi',
    isEnglish: language === 'en',
  };
};

