import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const Login = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const generatePKCE = async () => {
    // Generate code_verifier
    const array = new Uint8Array(32);
    crypto.getRandomValues(array);
    const codeVerifier = btoa(String.fromCharCode.apply(null, array))
      .replace(/\+/g, "-")
      .replace(/\//g, "_")
      .replace(/=/g, "");

    // Generate code_challenge
    const encoder = new TextEncoder();
    const data = encoder.encode(codeVerifier);
    const hashBuffer = await crypto.subtle.digest("SHA-256", data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const codeChallenge = btoa(String.fromCharCode.apply(null, hashArray))
      .replace(/\+/g, "-")
      .replace(/\//g, "_")
      .replace(/=/g, "");

    return { codeVerifier, codeChallenge };
  };

  const generateNonce = () => {
    const array = new Uint8Array(16);
    crypto.getRandomValues(array);
    return btoa(String.fromCharCode.apply(null, array))
      .replace(/\+/g, "-")
      .replace(/\//g, "_")
      .replace(/=/g, "");
  };

  const handleLogin = async () => {
    try {
      setLoading(true);
      console.log("🔓 Initiating OAuth login...");

      // Generate PKCE and nonce
      const { codeVerifier, codeChallenge } = await generatePKCE();
      const nonce = generateNonce();
      const state = generateNonce(); // Additional state parameter

      console.log("🔐 Generated Security Parameters:", {
        codeVerifier: `${codeVerifier.substring(0, 20)}...`,
        codeChallenge: `${codeChallenge.substring(0, 20)}...`,
        nonce: `${nonce.substring(0, 20)}...`,
        state: `${state.substring(0, 20)}...`,
        timestamp: new Date().toISOString(),
      });

      // Store in session storage for callback
      sessionStorage.setItem("pkce_code_verifier", codeVerifier);
      sessionStorage.setItem("pkce_code_challenge", codeChallenge);
      sessionStorage.setItem("oauth_nonce", nonce);
      sessionStorage.setItem("oauth_state", state);

      // Build OAuth authorize URL
      const callbackUri = `${window.location.origin}/oauth-callback`;
      const authorizeUrl = new URL("https://localhost:7070/connect/authorize");
      authorizeUrl.searchParams.append("client_id", "external-app");
      authorizeUrl.searchParams.append("redirect_uri", callbackUri);
      authorizeUrl.searchParams.append("response_type", "code");
      authorizeUrl.searchParams.append(
        "scope",
        "openid profile email api1 offline_access"
      );
      authorizeUrl.searchParams.append("code_challenge", codeChallenge);
      authorizeUrl.searchParams.append("code_challenge_method", "S256");
      authorizeUrl.searchParams.append("nonce", nonce);
      authorizeUrl.searchParams.append("state", state);

      console.log("🚀 Redirecting to OAuth provider:", {
        url: authorizeUrl.toString().substring(0, 100) + "...",
        timestamp: new Date().toISOString(),
      });

      // Redirect to IdentityServer login
      window.location.href = authorizeUrl.toString();
    } catch (err) {
      console.error("🚨 OAuth Initiation Error:", {
        message: err.message,
        stack: err.stack,
        timestamp: new Date().toISOString(),
      });
      alert(`Login failed: ${err.message}`);
      setLoading(false);
    }
  };

  return (
    <div style={{ textAlign: "center", padding: "40px" }}>
      <h1>🔐 KitPraid Login</h1>
      <p>Click below to log in with IdentityServer</p>
      <button
        onClick={handleLogin}
        disabled={loading}
        style={{
          padding: "10px 20px",
          fontSize: "16px",
          cursor: loading ? "not-allowed" : "pointer",
          opacity: loading ? 0.6 : 1,
        }}
      >
        {loading ? "Logging in..." : "Login with IdentityServer"}
      </button>
      <p style={{ marginTop: "20px", color: "#666" }}>
        💡 Open browser console (F12) to see detailed OAuth logs
      </p>
    </div>
  );
};

export default Login;
