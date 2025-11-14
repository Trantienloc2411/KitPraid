import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useSearchParams } from "react-router-dom";
import { getColor } from "../theme/colors";
import Tabs from "../components/Tabs";
import Input from "../components/Input";
import Button from "../components/Button";
import Breadcrumb from "../components/Breadcrumb";
import FormElement from "../components/FormElement";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { authService } from "../services/auth";
const Authorization = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  // View states: 'signin' | 'signup' | 'forgetPassword' | 'resetPassword' | 'verifyEmail'
  const [currentView, setCurrentView] = useState("signin");
  const [activeTab, setActiveTab] = useState("signin");

  // Auto-redirect to IdentityServer Razor UI when accessing signin view
  useEffect(() => {
    const viewParam = searchParams.get("view");
    const initialView = viewParam || "signin";
    
    // Set initial view from URL params
    if (viewParam) {
      setCurrentView(initialView);
      setActiveTab(initialView);
    }
    
    // Only redirect if on signin view and not authenticated
    if (initialView === "signin") {
      // Check if user is already authenticated (has token)
      const token = localStorage.getItem("authToken") || sessionStorage.getItem("authToken");
      if (!token) {
        // Not authenticated, redirect to IdentityServer Razor UI login page immediately
        // This ensures login form is always shown on Razor page, not React
        authService.redirectToLogin();
      } else {
        // Already authenticated, redirect to home
        navigate("/", { replace: true });
      }
    }
  }, []); // Only run once on mount

  // Form states
  const [signInForm, setSignInForm] = useState({ email: "", password: "" });
  const [signUpForm, setSignUpForm] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
    agreeTerms: false,
  });
  const [forgetPasswordForm, setForgetPasswordForm] = useState({ email: "" });
  const [resetPasswordForm, setResetPasswordForm] = useState({
    password: "",
    confirmPassword: "",
  });
  const [verifyEmailForm, setVerifyEmailForm] = useState({ code: "" });

  // Password visibility states
  const [showPassword, setShowPassword] = useState({
    signIn: false,
    signUp: false,
    signUpConfirm: false,
    reset: false,
    resetConfirm: false,
  });

  // Eye icon component
  const EyeIcon = ({ visible, onClick }) => (
    <div
      onClick={onClick}
      style={{
        cursor: "pointer",
        display: "flex",
        alignItems: "center",
        color: "#6B7280",
      }}
    >
      {visible ? <FaEyeSlash size={18} /> : <FaEye size={18} />}
    </div>
  );

  // Breadcrumb items based on current view
  const getBreadcrumbItems = () => {
    const base = [
      { label: t("auth.home"), href: "/", onClick: () => navigate("/") },
      {
        label: t("auth.userAccount"),
        href: "/auth",
        onClick: () => {
          setCurrentView(activeTab);
          navigate("/auth");
        },
      },
    ];

    switch (currentView) {
      case "signin":
        return [...base, { label: t("auth.signIn"), href: null }];
      case "signup":
        return [...base, { label: t("auth.signUp"), href: null }];
      case "forgetPassword":
        return [
          ...base,
          {
            label: t("auth.signIn"),
            href: "/auth",
            onClick: () => {
              setCurrentView("signin");
              setActiveTab("signin");
            },
          },
          { label: t("auth.forgetPassword"), href: null },
        ];
      case "resetPassword":
        return [
          ...base,
          {
            label: t("auth.signIn"),
            href: "/auth",
            onClick: () => {
              setCurrentView("signin");
              setActiveTab("signin");
            },
          },
          {
            label: t("auth.forgetPassword"),
            href: "/auth",
            onClick: () => setCurrentView("forgetPassword"),
          },
          { label: t("auth.resetPassword"), href: null },
        ];
      case "verifyEmail":
        return [
          ...base,
          {
            label: t("auth.signUp"),
            href: "/auth",
            onClick: () => {
              setCurrentView("signup");
              setActiveTab("signup");
            },
          },
          { label: t("auth.emailVerification"), href: null },
        ];
      default:
        return base;
    }
  };

  // Handle tab change
  const handleTabChange = (tabId) => {
    setActiveTab(tabId);
    setCurrentView(tabId);
  };

  // Container styles
  const containerStyle = {
    minHeight: "100vh",
    backgroundColor: "#FFFFFF",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: "0rem 1rem",
  };

  const breadcrumbContainerStyle = {
    width: "100%",
    marginBottom: "2rem",
    padding: "0rem 0",
  };

  const cardStyle = {
    backgroundColor: "#FFFFFF",
    borderRadius: "12px",
    padding: "2.5rem",
    width: "100%",
    maxWidth: "500px",
    boxShadow: "0 4px 12px rgba(0, 0, 0, 0.15)",
  };

  const titleStyle = {
    fontSize: "24px",
    fontWeight: "bold",
    color: "#111827",
    textAlign: "center",
    marginBottom: "1.5rem",
  };

  const descStyle = {
    fontSize: "14px",
    color: "#6B7280",
    textAlign: "center",
    marginBottom: "2rem",
    lineHeight: "1.6",
    whiteSpace: "pre-line",
  };

  const formStyle = {
    display: "flex",
    flexDirection: "column",
    gap: "1.5rem",
  };

  const linkStyle = {
    color: getColor("secondary", 500),
    textDecoration: "none",
    cursor: "pointer",
    fontSize: "14px",
  };

  const dividerStyle = {
    display: "flex",
    alignItems: "center",
    margin: "1.5rem 0",
    color: "#9CA3AF",
    fontSize: "14px",
  };

  const dividerLineStyle = {
    flex: 1,
    height: "1px",
    backgroundColor: "#E5E7EB",
  };

  // Render Sign In Form
  const renderSignInForm = () => (
    <div style={formStyle}>
      <Input
        label={t("auth.emailAddress")}
        type="email"
        value={signInForm.email}
        onChange={(e) =>
          setSignInForm({ ...signInForm, email: e.target.value })
        }
        placeholder={t("auth.emailAddress")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
      />
      <div>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            marginBottom: "6px",
          }}
        >
          <label style={{ fontSize: "14px", color: "#111827" }}>
            {t("auth.password")}
          </label>
          <a
            href="#"
            onClick={(e) => {
              e.preventDefault();
              setCurrentView("forgetPassword");
            }}
            style={linkStyle}
          >
            {t("auth.forgetPasswordLink")}
          </a>
        </div>
        <Input
          type={showPassword.signIn ? "text" : "password"}
          value={signInForm.password}
          onChange={(e) =>
            setSignInForm({ ...signInForm, password: e.target.value })
          }
          placeholder={t("auth.password")}
          size="md"
          hasBorder
          hasBackgroundTint={false}
          rightIcon={
            <EyeIcon
              visible={showPassword.signIn}
              onClick={() =>
                setShowPassword({
                  ...showPassword,
                  signIn: !showPassword.signIn,
                })
              }
            />
          }
        />
      </div>
      <Button
        label={t("auth.signInButton")}
        hasArrow
        variant="primary"
        size="lg"
        onClick={() => {
          // Redirect to IdentityServer Razor UI login page
          authService.redirectToLogin();
        }}
        style={{ width: "100%", marginTop: "0.5rem" }}
      />
      <div style={dividerStyle}>
        <div style={dividerLineStyle}></div>
        <span style={{ padding: "0 1rem" }}>or</span>
        <div style={dividerLineStyle}></div>
      </div>
      <div style={{ fontSize: "14px", color: "#6B7280", textAlign: "center" }}>
        {t("auth.dontHaveAccount")}{" "}
        <a
          href="#"
          onClick={(e) => {
            e.preventDefault();
            setActiveTab("signup");
            setCurrentView("signup");
          }}
          style={linkStyle}
        >
          {t("auth.signUp")}
        </a>
      </div>
    </div>
  );

  // Render Sign Up Form
  const renderSignUpForm = () => (
    <div style={formStyle}>
      <Input
        label={t("auth.name")}
        type="text"
        value={signUpForm.name}
        onChange={(e) => setSignUpForm({ ...signUpForm, name: e.target.value })}
        placeholder={t("auth.name")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
      />
      <Input
        label={t("auth.emailAddress")}
        type="email"
        value={signUpForm.email}
        onChange={(e) =>
          setSignUpForm({ ...signUpForm, email: e.target.value })
        }
        placeholder={t("auth.emailAddress")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
      />
      <Input
        label={t("auth.password")}
        type={showPassword.signUp ? "text" : "password"}
        value={signUpForm.password}
        onChange={(e) =>
          setSignUpForm({ ...signUpForm, password: e.target.value })
        }
        placeholder={t("auth.passwordPlaceholder")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
        rightIcon={
          <EyeIcon
            visible={showPassword.signUp}
            onClick={() =>
              setShowPassword({
                ...showPassword,
                signUp: !showPassword.signUp,
              })
            }
          />
        }
      />
      <Input
        label={t("auth.confirmPassword")}
        type={showPassword.signUpConfirm ? "text" : "password"}
        value={signUpForm.confirmPassword}
        onChange={(e) =>
          setSignUpForm({ ...signUpForm, confirmPassword: e.target.value })
        }
        placeholder={t("auth.confirmPassword")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
        rightIcon={
          <EyeIcon
            visible={showPassword.signUpConfirm}
            onClick={() =>
              setShowPassword({
                ...showPassword,
                signUpConfirm: !showPassword.signUpConfirm,
              })
            }
          />
        }
      />
      <div style={{ display: "flex", alignItems: "flex-start", gap: "8px" }}>
        <FormElement.Checkbox
          checked={signUpForm.agreeTerms}
          onChange={(checked) =>
            setSignUpForm({ ...signUpForm, agreeTerms: checked })
          }
          size="md"
        />
        <label
          style={{
            fontSize: "14px",
            color: "#111827",
            lineHeight: "1.5",
            cursor: "pointer",
          }}
          onClick={() =>
            setSignUpForm({
              ...signUpForm,
              agreeTerms: !signUpForm.agreeTerms,
            })
          }
        >
          {t("auth.termsAgree")}{" "}
          <a href="#" style={linkStyle}>
            {t("auth.termsOfCondition")}
          </a>{" "}
          {t("auth.and")}{" "}
          <a href="#" style={linkStyle}>
            {t("auth.privacyPolicy")}
          </a>
          .
        </label>
      </div>
      <Button
        label={t("auth.signUpButton")}
        hasArrow
        variant="primary"
        size="lg"
        onClick={() => {
          // Handle sign up
          console.log("Sign Up:", signUpForm);
          setCurrentView("verifyEmail");
        }}
        style={{ width: "100%", marginTop: "0.5rem" }}
      />
      <div style={dividerStyle}>
        <div style={dividerLineStyle}></div>
        <span style={{ padding: "0 1rem" }}>or</span>
        <div style={dividerLineStyle}></div>
      </div>
      <div style={{ fontSize: "14px", color: "#6B7280", textAlign: "center" }}>
        {t("auth.alreadyHaveAccount")}{" "}
        <a
          href="#"
          onClick={(e) => {
            e.preventDefault();
            setActiveTab("signin");
            setCurrentView("signin");
          }}
          style={linkStyle}
        >
          {t("auth.signIn")}
        </a>
      </div>
    </div>
  );

  // Render Forget Password Form
  const renderForgetPasswordForm = () => (
    <div style={formStyle}>
      <p style={descStyle}>{t("auth.forgetPasswordDesc")}</p>
      <Input
        label={t("auth.emailAddress")}
        type="email"
        value={forgetPasswordForm.email}
        onChange={(e) =>
          setForgetPasswordForm({
            ...forgetPasswordForm,
            email: e.target.value,
          })
        }
        placeholder={t("auth.emailAddress")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
      />
      <Button
        label={t("auth.sendCode")}
        hasArrow
        variant="primary"
        size="lg"
        onClick={() => {
          // Handle send code
          console.log("Send Code:", forgetPasswordForm);
          setCurrentView("resetPassword");
        }}
        style={{ width: "100%", marginTop: "0.5rem" }}
      />
      <div
        style={{
          marginTop: "2rem",
          paddingTop: "2rem",
          borderTop: "1px solid #E5E7EB",
        }}
      >
        <div
          style={{
            fontSize: "14px",
            color: "#6B7280",
            textAlign: "center",
            marginBottom: "0.5rem",
          }}
        >
          {t("auth.alreadyHaveAccount")}{" "}
          <a
            href="#"
            onClick={(e) => {
              e.preventDefault();
              setCurrentView("signin");
              setActiveTab("signin");
            }}
            style={linkStyle}
          >
            {t("auth.signIn")}
          </a>
        </div>
        <div
          style={{
            fontSize: "14px",
            color: "#6B7280",
            textAlign: "center",
          }}
        >
          {t("auth.dontHaveAccount")}{" "}
          <a
            href="#"
            onClick={(e) => {
              e.preventDefault();
              setCurrentView("signup");
              setActiveTab("signup");
            }}
            style={linkStyle}
          >
            {t("auth.signUp")}
          </a>
        </div>
      </div>
      <div
        style={{
          marginTop: "2rem",
          paddingTop: "2rem",
          borderTop: "1px solid #E5E7EB",
          fontSize: "14px",
          color: "#6B7280",
          textAlign: "center",
        }}
      >
        {t("auth.customerServiceDesc")}{" "}
        <a href="#" style={{ ...linkStyle, color: getColor("primary", 500) }}>
          {t("auth.customerService")}
        </a>
        .
      </div>
    </div>
  );

  // Render Reset Password Form
  const renderResetPasswordForm = () => (
    <div style={formStyle}>
      <p style={descStyle}>{t("auth.resetPasswordDesc")}</p>
      <Input
        label={t("auth.password")}
        type={showPassword.reset ? "text" : "password"}
        value={resetPasswordForm.password}
        onChange={(e) =>
          setResetPasswordForm({
            ...resetPasswordForm,
            password: e.target.value,
          })
        }
        placeholder={t("auth.passwordPlaceholder")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
        rightIcon={
          <EyeIcon
            visible={showPassword.reset}
            onClick={() =>
              setShowPassword({
                ...showPassword,
                reset: !showPassword.reset,
              })
            }
          />
        }
      />
      <Input
        label={t("auth.confirmPassword")}
        type={showPassword.resetConfirm ? "text" : "password"}
        value={resetPasswordForm.confirmPassword}
        onChange={(e) =>
          setResetPasswordForm({
            ...resetPasswordForm,
            confirmPassword: e.target.value,
          })
        }
        placeholder={t("auth.confirmPassword")}
        size="md"
        hasBorder
        hasBackgroundTint={false}
        rightIcon={
          <EyeIcon
            visible={showPassword.resetConfirm}
            onClick={() =>
              setShowPassword({
                ...showPassword,
                resetConfirm: !showPassword.resetConfirm,
              })
            }
          />
        }
      />
      <Button
        label={t("auth.resetPasswordButton")}
        hasArrow
        variant="primary"
        size="lg"
        onClick={() => {
          // Handle reset password
          console.log("Reset Password:", resetPasswordForm);
          setCurrentView("signin");
          setActiveTab("signin");
        }}
        style={{ width: "100%", marginTop: "0.5rem" }}
      />
    </div>
  );

  // Render Verify Email Form
  const renderVerifyEmailForm = () => (
    <div style={formStyle}>
      <p style={descStyle}>{t("auth.verifyEmailDesc")}</p>
      <div>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            marginBottom: "6px",
          }}
        >
          <label style={{ fontSize: "14px", color: "#111827" }}>
            {t("auth.verificationCode")}
          </label>
          <a href="#" style={linkStyle}>
            {t("auth.resendCode")}
          </a>
        </div>
        <Input
          type="text"
          value={verifyEmailForm.code}
          onChange={(e) =>
            setVerifyEmailForm({ ...verifyEmailForm, code: e.target.value })
          }
          placeholder={t("auth.verificationCode")}
          size="md"
          hasBorder
          hasBackgroundTint={false}
        />
      </div>
      <Button
        label={t("auth.verifyMe")}
        hasArrow
        variant="primary"
        size="lg"
        onClick={() => {
          // Handle verify email
          console.log("Verify Email:", verifyEmailForm);
          navigate("/");
        }}
        style={{ width: "100%", marginTop: "0.5rem" }}
      />
    </div>
  );

  // Determine which form to render
  const renderForm = () => {
    switch (currentView) {
      case "signin":
        return renderSignInForm();
      case "signup":
        return renderSignUpForm();
      case "forgetPassword":
        return renderForgetPasswordForm();
      case "resetPassword":
        return renderResetPasswordForm();
      case "verifyEmail":
        return renderVerifyEmailForm();
      default:
        return renderSignInForm();
    }
  };

  // Show tabs only for signin/signup views
  const showTabs = currentView === "signin" || currentView === "signup";

  return (
    <div style={containerStyle}>
      <div style={breadcrumbContainerStyle}>
        <Breadcrumb
          items={getBreadcrumbItems()}
          showHome={true}
          size="lg"
          variant="primary"
        />
      </div>

      <div style={cardStyle}>
        {showTabs && (
          <Tabs
            items={[
              { id: "signin", label: t("auth.signIn") },
              { id: "signup", label: t("auth.signUp") },
            ]}
            value={activeTab}
            onChange={handleTabChange}
            kind="underline"
            variant="primary"
            size="md"
            style={{ marginBottom: "2rem" }}
          />
        )}

        {!showTabs && (
          <h1 style={titleStyle}>
            {currentView === "forgetPassword" && t("auth.forgetPassword")}
            {currentView === "resetPassword" && t("auth.resetPassword")}
            {currentView === "verifyEmail" && t("auth.verifyEmail")}
          </h1>
        )}

        {renderForm()}
      </div>
    </div>
  );
};

export default Authorization;
