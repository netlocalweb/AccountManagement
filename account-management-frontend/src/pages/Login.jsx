import { useState } from "react";
import { login, logout } from "../api/authApi";
import { getToken } from "../api/apiClient";

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState(getToken() ? "You are logged in." : "");
    const [error, setError] = useState("");

    const handleLogin = async () => {
        try {
            setError("");
            setMessage("");

            await login(username, password);

            setMessage("Login successful. Token saved.");
            setUsername("");
            setPassword("");
        } catch (err) {
            setError(err.message || "Login failed.");
        }
    };

    const handleLogout = () => {
        logout();
        setMessage("Logged out.");
        setError("");
    };

    return (
        <div className="page-card">
            <p className="top-label">Authentication</p>
            <h2>Login</h2>

            {error && <p className="error-text">{error}</p>}
            {message && <p className="success-text">{message}</p>}

            <div className="form-panel">
                <div className="form-grid">
                    <div className="input-group">
                        <label>Username</label>
                        <input
                            placeholder="Enter username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                    </div>

                    <div className="input-group">
                        <label>Password</label>
                        <input
                            type="password"
                            placeholder="Enter password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                </div>

                <div className="form-actions">
                    <button className="primary-btn" type="button" onClick={handleLogin}>
                        Login
                    </button>

                    <button className="secondary-btn" type="button" onClick={handleLogout}>
                        Logout
                    </button>
                </div>
            </div>
        </div>
    );
}

export default Login;