# Authentication Pages

This directory contains all the authentication-related pages for the KitPraid application, built with React, TypeScript, and ChakraUI v3.

## Pages

### 1. SignIn (`/signin`)
- User login form with email and password
- Form validation with error handling
- Social login options (Google, Facebook) - placeholder for future implementation
- Links to forgot password and sign up pages
- Responsive design with ChakraUI components

### 2. SignUp (`/signup`)
- User registration form with comprehensive validation
- Password strength indicator
- Terms and conditions acceptance
- Social sign up options (Google, Facebook) - placeholder for future implementation
- Links to sign in page

### 3. ForgotPassword (`/forgot-password`)
- Email input form for password reset
- Success state showing confirmation message
- Option to resend reset email
- Links back to sign in page

### 4. ResetPassword (`/reset-password`)
- New password and confirm password form
- Password strength validation
- Success state after password reset
- Links to sign in page

### 5. EmailVerification (`/email-verification`)
- 6-digit verification code input
- Option to resend verification code
- Success state after verification
- Links to sign in page

## Features

- **Breadcrumb Navigation**: All pages include breadcrumb navigation for better UX
- **Form Validation**: Comprehensive client-side validation with error messages
- **Responsive Design**: Mobile-friendly layouts using ChakraUI components
- **Loading States**: Loading indicators for form submissions
- **Error Handling**: User-friendly error messages and validation
- **Accessibility**: Proper ARIA labels and keyboard navigation support

## Components Used

- **ChakraUI v3**: Modern UI components with consistent design
- **React Icons**: FontAwesome icons for visual elements
- **React Router**: Navigation between authentication pages
- **Custom Breadcrumb**: Reusable breadcrumb component

## Usage

### Basic Routing
```tsx
import { Routes, Route } from 'react-router-dom';
import AuthRoutes from './routes/AuthRoutes';

// In your main App component
<Routes>
  <Route path="/auth/*" element={<AuthRoutes />} />
</Routes>
```

### Individual Page Import
```tsx
import { SignIn, SignUp } from '../pages/Auth';

// Use in your components
<SignIn />
<SignUp />
```

## Styling

All pages use ChakraUI's design system with:
- Consistent spacing and typography
- Responsive breakpoints
- Color schemes and variants
- Shadow and border styling
- Hover effects and transitions

## Future Enhancements

- [ ] Google OAuth integration
- [ ] Facebook OAuth integration
- [ ] Two-factor authentication
- [ ] Remember me functionality
- [ ] Password recovery via SMS
- [ ] Account lockout protection
- [ ] Rate limiting for security

## Notes

- All API calls are currently mocked with setTimeout
- Form validation is client-side only
- Social login buttons show "Coming Soon" messages
- Password strength calculation is basic but effective
- All pages include proper TypeScript types
