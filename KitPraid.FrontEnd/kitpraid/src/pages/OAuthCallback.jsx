import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

export const OAuthCallback = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const handleCallback = async () => {
      try {
        const code = searchParams.get("code");
        const state = searchParams.get("state");
        const sessionState = searchParams.get("session_state");

        console.log("🔐 OAuth Callback Received:", {
          code: code ? `${code.substring(0, 20)}...` : "missing",
          state,
          sessionState,
          timestamp: new Date().toISOString(),
        });

        if (!code) {
          throw new Error("Authorization code not found in redirect URL");
        }

        // Get stored PKCE parameters from session/local storage
        const codeVerifier = sessionStorage.getItem("pkce_code_verifier");
        if (!codeVerifier) {
          throw new Error(
            "PKCE code_verifier not found. Session may have expired."
          );
        }

        console.log("📝 Exchanging authorization code for token...");

        // Exchange code for token (call your backend OR IdentityServer directly)
        const tokenResponse = await fetch(
          "https://localhost:7070/connect/token",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/x-www-form-urlencoded",
            },
            body: new URLSearchParams({
              grant_type: "authorization_code",
              client_id: "external-app",
              client_secret: "external-secret", // ⚠️ SECURITY: Move this to backend in production
              code,
              redirect_uri: `${window.location.origin}/oauth-callback`,
              code_verifier: codeVerifier,
            }),
          }
        );

        if (!tokenResponse.ok) {
          const errorData = await tokenResponse.json();
          console.error("❌ Token exchange failed:", {
            status: tokenResponse.status,
            error: errorData.error,
            error_description: errorData.error_description,
          });
          throw new Error(
            `Token exchange failed: ${errorData.error} - ${errorData.error_description}`
          );
        }

        const tokenData = await tokenResponse.json();

        console.log("✅ Token received successfully:", {
          access_token: `${tokenData.access_token.substring(0, 20)}...`,
          token_type: tokenData.token_type,
          expires_in: tokenData.expires_in,
          refresh_token: tokenData.refresh_token ? "present" : "not provided",
          scope: tokenData.scope,
        });

        // Store tokens
        localStorage.setItem("authToken", tokenData.access_token);
        if (tokenData.refresh_token) {
          localStorage.setItem("refresh_token", tokenData.refresh_token);
        }
        if (tokenData.id_token) {
          localStorage.setItem("id_token", tokenData.id_token);
        }

        // Decode and log token claims
        try {
          const claims = JSON.parse(atob(tokenData.access_token.split(".")[1]));
          console.log("👤 Token Claims:", {
            sub: claims.sub,
            email: claims.email,
            name: claims.name,
            scope: claims.scope,
            aud: claims.aud,
            iss: claims.iss,
            exp: new Date(claims.exp * 1000).toISOString(),
          });
        } catch (e) {
          console.warn("Could not decode token claims:", e.message);
        }

        // Clean up session storage
        sessionStorage.removeItem("pkce_code_verifier");
        sessionStorage.removeItem("pkce_code_challenge");

        console.log("🎉 Login successful! Redirecting to home...");

        // Redirect to home or dashboard
        setLoading(false);
        setTimeout(() => {
          navigate("/");
        }, 1000);
      } catch (err) {
        console.error("🚨 OAuth Callback Error:", {
          message: err.message,
          stack: err.stack,
          timestamp: new Date().toISOString(),
        });
        setError(err.message);
        setLoading(false);
      }
    };

    handleCallback();
  }, [searchParams, navigate]);

  if (loading) {
    return (
      <div style={{ textAlign: "center", padding: "40px" }}>
        <h2>Processing login...</h2>
        <p>Please wait while we authenticate you.</p>
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ textAlign: "center", padding: "40px", color: "red" }}>
        <h2>❌ Authentication Failed</h2>
        <p>{error}</p>
        <button onClick={() => navigate("/login")}>Back to Login</button>
      </div>
    );
  }

  return (
    <div style={{ textAlign: "center", padding: "40px" }}>
      <h2>✅ Login Successful!</h2>
      <p>Redirecting...</p>
    </div>
  );
};

export default OAuthCallback;
