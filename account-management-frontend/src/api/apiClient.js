const API_BASE_URL = "https://localhost:5001";

export function getToken() {
    return localStorage.getItem("token");
}

export function saveToken(token) {
    localStorage.setItem("token", token);
}

export function removeToken() {
    localStorage.removeItem("token");
}

export async function apiRequest(url, options = {}) {
    const token = getToken();

    const headers = {
        Accept: "application/json",
        ...(options.body ? { "Content-Type": "application/json" } : {}),
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
        ...options.headers,
    };

    const response = await fetch(`${API_BASE_URL}${url}`, {
        ...options,
        headers,
    });

    const text = await response.text();

    if (!response.ok) {
        throw new Error(text || response.statusText || "Request failed");
    }

    if (!text) return null;

    try {
        return JSON.parse(text);
    } catch {
        return text;
    }
}