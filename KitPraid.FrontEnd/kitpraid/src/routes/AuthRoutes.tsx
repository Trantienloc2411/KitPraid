
import { Routes, Route } from 'react-router-dom';
import {
  AuthTabs,
  ForgotPassword,
  ResetPassword,
  EmailVerification,
} from '../pages/Auth';

const AuthRoutes = () => {
  return (
    <Routes>
      <Route index element={<AuthTabs initialTab={0} />} />
      <Route path="signin" element={<AuthTabs initialTab={0} />} />
      <Route path="signup" element={<AuthTabs initialTab={1} />} />
      {/* Login flow */}
      <Route path="login/forgot-password" element={<ForgotPassword />} />
      <Route path="login/reset-password" element={<ResetPassword />} />
      {/* Signup flow */}
      <Route path="signup/email-verification" element={<EmailVerification />} />
    </Routes>
  );
};

export default AuthRoutes;
