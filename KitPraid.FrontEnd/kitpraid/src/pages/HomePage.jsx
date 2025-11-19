import { useEffect, useState } from "react";

const HomePage = ({ onSubscribe, className = "", style = {} }) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [user, setUser] = useState(null);

  const userId = "832850a0-0918-43b4-8d9e-188904813372"; // requested user

  const fetchUser = async () => {
    setLoading(true);
    setError(null);
    try {
      const token = localStorage.getItem("authToken");
      if (!token) {
        throw new Error(
          "No authToken found in localStorage. Please login first."
        );
      }

      console.log("📡 HomePage: Requesting user", userId);

      const res = await fetch(
        `https://localhost:5001/api/auth/user/${userId}`,
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      console.log(`📊 HomePage: Response status ${res.status}`);

      if (res.status === 401) {
        const text = await res.text();
        console.error("🔒 Unauthorized (401):", text);
        throw new Error("Unauthorized. Token may be invalid or expired.");
      }

      if (!res.ok) {
        const text = await res.text();
        console.error("❌ API error:", res.status, text);
        throw new Error(`API Error: ${res.status} ${res.statusText}`);
      }

      const data = await res.json();
      console.log("✅ HomePage: User data:", data);
      setUser(data);
    } catch (err) {
      console.error("🚨 HomePage fetch error:", err);
      setError(err.message || String(err));
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    // Try fetching on mount; user can retry via button
    fetchUser();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div
      className={`homepage ${className}`}
      style={{
        width: "100%",
        ...style,
      }}
    >
      <div
        style={{
          width: "100%",
          minHeight: "calc(100vh - 200px)",
          padding: 24,
        }}
      >
        <h1>Home</h1>

        <div style={{ marginBottom: 16 }}>
          <button onClick={fetchUser} disabled={loading}>
            {loading ? "Loading..." : "Fetch User"}
          </button>
        </div>

        {error && (
          <div style={{ color: "red", marginBottom: 12 }}>
            <strong>Error:</strong> {error}
          </div>
        )}

        {user ? (
          <div style={{ background: "#f6f8fa", padding: 12, borderRadius: 6 }}>
            <h3>User Data</h3>
            <pre style={{ whiteSpace: "pre-wrap", wordBreak: "break-word" }}>
              {JSON.stringify(user, null, 2)}
            </pre>
          </div>
        ) : (
          !loading && <div>No user data yet.</div>
        )}
      </div>
    </div>
  );
};

export default HomePage;
