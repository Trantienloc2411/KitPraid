import React, { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { authService } from '../services/auth';

/**
 * Callback Page
 * Handles the OAuth 2.0 Authorization Code callback from IdentityServer
 * 
 * Flow:
 * 1. IdentityServer redirects here with authorization code and state
 * 2. Extract code and state from URL parameters
 * 3. Exchange authorization code for access token using PKCE verifier
 * 4. Store tokens and redirect to home page
 */
const Callback = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [status, setStatus] = useState('processing'); // 'processing' | 'success' | 'error'
  const [error, setError] = useState(null);

  useEffect(() => {
    const handleCallback = async () => {
      try {
        // Extract authorization code and state from URL
        const code = searchParams.get('code');
        const state = searchParams.get('state');
        const errorParam = searchParams.get('error');
        const errorDescription = searchParams.get('error_description');

        // Check for OAuth errors
        if (errorParam) {
          throw new Error(errorDescription || `OAuth error: ${errorParam}`);
        }

        // Validate required parameters
        if (!code) {
          throw new Error('Authorization code not found in callback URL');
        }

        if (!state) {
          throw new Error('State parameter not found in callback URL');
        }

        // Exchange authorization code for tokens
        setStatus('processing');
        const tokenResponse = await authService.exchangeCodeForTokens(code, state);

        if (tokenResponse.access_token) {
          setStatus('success');
          
          // Redirect to home page after a short delay
          setTimeout(() => {
            navigate('/', { replace: true });
          }, 1500);
        } else {
          throw new Error('No access token received from IdentityServer');
        }
      } catch (err) {
        console.error('Callback error:', err);
        setStatus('error');
        setError(err.message || 'An error occurred during authentication');
        
        // Redirect to login page after error
        setTimeout(() => {
          navigate('/auth', { replace: true });
        }, 3000);
      }
    };

    handleCallback();
  }, [searchParams, navigate]);

  const containerStyle = {
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    padding: '2rem',
    backgroundColor: '#FFFFFF',
  };

  const cardStyle = {
    backgroundColor: '#FFFFFF',
    borderRadius: '12px',
    padding: '2.5rem',
    maxWidth: '500px',
    width: '100%',
    boxShadow: '0 4px 12px rgba(0, 0, 0, 0.15)',
    textAlign: 'center',
  };

  const titleStyle = {
    fontSize: '24px',
    fontWeight: 'bold',
    color: '#111827',
    marginBottom: '1rem',
  };

  const messageStyle = {
    fontSize: '16px',
    color: '#6B7280',
    marginBottom: '1.5rem',
  };

  const errorStyle = {
    fontSize: '14px',
    color: '#DC2626',
    marginTop: '1rem',
    padding: '1rem',
    backgroundColor: '#FEF2F2',
    borderRadius: '8px',
  };

  const spinnerStyle = {
    border: '4px solid #F3F4F6',
    borderTop: '4px solid #3B82F6',
    borderRadius: '50%',
    width: '40px',
    height: '40px',
    animation: 'spin 1s linear infinite',
    margin: '0 auto 1rem',
  };

  return (
    <div style={containerStyle}>
      <div style={cardStyle}>
        {status === 'processing' && (
          <>
            <div style={spinnerStyle}></div>
            <h1 style={titleStyle}>Processing Authentication...</h1>
            <p style={messageStyle}>
              Please wait while we complete your login.
            </p>
          </>
        )}

        {status === 'success' && (
          <>
            <div style={{ fontSize: '48px', marginBottom: '1rem' }}>✅</div>
            <h1 style={titleStyle}>Login Successful!</h1>
            <p style={messageStyle}>
              You have been successfully authenticated. Redirecting...
            </p>
          </>
        )}

        {status === 'error' && (
          <>
            <div style={{ fontSize: '48px', marginBottom: '1rem' }}>❌</div>
            <h1 style={titleStyle}>Authentication Failed</h1>
            <p style={messageStyle}>
              There was an error during the authentication process.
            </p>
            {error && (
              <div style={errorStyle}>
                <strong>Error:</strong> {error}
              </div>
            )}
            <p style={{ ...messageStyle, fontSize: '14px', marginTop: '1rem' }}>
              Redirecting to login page...
            </p>
          </>
        )}
      </div>

      <style>{`
        @keyframes spin {
          0% { transform: rotate(0deg); }
          100% { transform: rotate(360deg); }
        }
      `}</style>
    </div>
  );
};

export default Callback;

