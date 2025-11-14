# Installation Guide

## Required Dependencies

Install the following packages:

```bash
npm install axios react-router-dom @tanstack/react-query i18next react-i18next
```

## What Changed

### ✅ Using Libraries Instead of Manual Code:

1. **Axios** - Replaced manual fetch API
   - Better error handling
   - Request/response interceptors
   - Automatic JSON parsing

2. **React Query (@tanstack/react-query)** - Replaced custom useApi hooks
   - Built-in caching
   - Automatic refetching
   - Loading/error states
   - DevTools support

3. **React Router DOM** - For routing
   - Client-side routing
   - Navigation components

4. **i18next & react-i18next** - For internationalization
   - Multi-language support (Vietnamese & English)
   - Language switching
   - Translation management

### 🗑️ Removed Files:
- `src/hooks/useApi.js` - Replaced by React Query
- `src/config/README.md` - Not needed

### 📁 Kept Custom Hooks:
- `useLocalStorage` - Useful utility
- `useDebounce` - Useful utility  
- `useMediaQuery` - Useful utility
- `useLanguage` - Language management hook

## Usage Examples

### API with Axios:
```jsx
import api, { endpoints } from './config/api';

// GET
const response = await api.get(endpoints.products.list);
const products = response.data;

// POST
const response = await api.post(endpoints.auth.login, { email, password });
```

### Data Fetching with React Query:
```jsx
import { useQuery, useMutation } from '@tanstack/react-query';
import api, { endpoints } from './config/api';

// Query
const { data, isLoading, error } = useQuery({
  queryKey: ['products'],
  queryFn: () => api.get(endpoints.products.list).then(res => res.data)
});

// Mutation
const mutation = useMutation({
  mutationFn: (data) => api.post(endpoints.auth.login, data),
  onSuccess: (data) => {
    console.log('Success:', data);
  }
});
```

### i18n (Internationalization):
```jsx
import { useTranslation } from 'react-i18next';
import { useLanguage } from './hooks/useLanguage';
import LanguageSwitcher from './components/LanguageSwitcher';

// Using translations
function MyComponent() {
  const { t } = useTranslation();
  return <h1>{t('common.welcome')}</h1>;
}

// Language switcher
function Header() {
  const { language, setLanguage } = useLanguage();
  return <LanguageSwitcher />;
}
```

