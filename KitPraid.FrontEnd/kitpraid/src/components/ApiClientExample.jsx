import { useEffect, useState } from 'react';
import { apiGet, isAuthenticated, getCurrentUser, logout } from '../services/apiClient';

/**
 * Example component showing how to use the API client
 * This demonstrates:
 * - Checking if user is authenticated
 * - Getting current user info
 * - Making authenticated API calls
 * - Handling errors
 * - Logout functionality
 */
export const ApiClientExample = () => {
  const [user, setUser] = useState(null);
  const [userData, setUserData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Check authentication status
    if (isAuthenticated()) {
      console.log('✅ User is authenticated');
      const currentUser = getCurrentUser();
      setUser(currentUser);
    } else {
      console.log('❌ User is not authenticated');
    }
  }, []);

  const handleFetchUser = async () => {
    try {
      setLoading(true);
      setError(null);

      // Example: Fetch user data from API
      // Replace with your actual user ID
      const userId = user?.sub || '935cf5a9-042c-4b66-92d9-32ebbbbffcd9';
      
      console.log('📡 Fetching user data for ID:', userId);
      const data = await apiGet(`/api/auth/user/${userId}`);
      
      setUserData(data);
      console.log('👤 User data:', data);
    } catch (err) {
      console.error('Error fetching user:', err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    logout();
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
      <h2>API Client Example</h2>

      {/* Authentication Status */}
      <section style={{ marginBottom: '20px', padding: '10px', border: '1px solid #ccc' }}>
        <h3>Authentication Status</h3>
        {user ? (
          <div>
            <p style={{ color: 'green' }}>✅ Authenticated</p>
            <table>
              <tbody>
                <tr>
                  <td><strong>User ID (sub):</strong></td>
                  <td><code>{user.sub}</code></td>
                </tr>
                <tr>
                  <td><strong>Email:</strong></td>
                  <td>{user.email || 'N/A'}</td>
                </tr>
                <tr>
                  <td><strong>Name:</strong></td>
                  <td>{user.name || 'N/A'}</td>
                </tr>
              </tbody>
            </table>
          </div>
        ) : (
          <p style={{ color: 'red' }}>
            ❌ Not authenticated. <a href="/login">Login here</a>
          </p>
        )}
      </section>

      {/* API Call Example */}
      {user && (
        <section style={{ marginBottom: '20px', padding: '10px', border: '1px solid #ccc' }}>
          <h3>API Call Example</h3>
          <p>Click below to fetch user data from the API:</p>
          <button
            onClick={handleFetchUser}
            disabled={loading}
            style={{
              padding: '8px 16px',
              cursor: loading ? 'not-allowed' : 'pointer',
              opacity: loading ? 0.6 : 1,
              marginBottom: '10px',
            }}
          >
            {loading ? 'Loading...' : 'Fetch User Data'}
          </button>

          {error && (
            <div style={{ color: 'red', marginTop: '10px' }}>
              <strong>❌ Error:</strong> {error}
            </div>
          )}

          {userData && (
            <div style={{ marginTop: '10px', padding: '10px', backgroundColor: '#f0f0f0' }}>
              <strong>Response from API:</strong>
              <pre style={{ whiteSpace: 'pre-wrap', wordWrap: 'break-word' }}>
                {JSON.stringify(userData, null, 2)}
              </pre>
            </div>
          )}
        </section>
      )}

      {/* Console Logging Tips */}
      <section style={{ marginBottom: '20px', padding: '10px', border: '1px solid #999', backgroundColor: '#f9f9f9' }}>
        <h3>💡 Console Logging Tips</h3>
        <ul>
          <li>Open DevTools: Press <code>F12</code> or Right-click → Inspect</li>
          <li>Go to "Console" tab</li>
          <li>You'll see all API calls, responses, and errors logged automatically</li>
          <li>Look for logs like:
            <ul>
              <li><code>📡 API Call: ...</code> - When request is made</li>
              <li><code>📊 API Response: ...</code> - When response is received</li>
              <li><code>✅ API Success: ...</code> - On successful response</li>
              <li><code>❌ API Error: ...</code> - On error</li>
              <li><code>🔑 Token expired or invalid</code> - When token is invalid</li>
            </ul>
          </li>
        </ul>
      </section>

      {/* Logout */}
      {user && (
        <section style={{ padding: '10px', border: '1px solid #999', backgroundColor: '#fff3cd' }}>
          <button
            onClick={handleLogout}
            style={{
              padding: '8px 16px',
              backgroundColor: '#dc3545',
              color: 'white',
              border: 'none',
              borderRadius: '4px',
              cursor: 'pointer',
            }}
          >
            🚪 Logout
          </button>
        </section>
      )}
    </div>
  );
};

export default ApiClientExample;
