import { apiRequest, saveToken, removeToken } from "./apiClient";

export async function login(username, password) {
    const data = await apiRequest("/api/auth/Login", {
        method: "POST",
        body: JSON.stringify({ username, password }),
    });

    const token =
        typeof data === "string"
            ? data
            : data?.token || data?.Token || data?.accessToken || data?.AccessToken;

    if (!token) {
        throw new Error("Login succeeded, but token was not returned.");
    }

    saveToken(token);
    return token;
}

export function logout() {
    removeToken();
}