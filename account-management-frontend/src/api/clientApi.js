const BASE_URL = "https://localhost:5001/api/client";

async function handleResponse(response) {
    const text = await response.text();

    if (!response.ok) {
        throw new Error(text || response.statusText || "Request failed");
    }

    if (!text) {
        return null;
    }

    return JSON.parse(text);
}

export async function getClients() {
    const response = await fetch(`${BASE_URL}/GetAllClients`, {
        method: "GET",
        headers: {
            Accept: "application/json",
        },
    });

    return handleResponse(response);
}

export async function createClient(client) {
    const response = await fetch(`${BASE_URL}/CreateClient`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify(client),
    });

    return handleResponse(response);
}

export async function updateClient(id, client) {
    const response = await fetch(`${BASE_URL}/UpdateClient/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify(client),
    });

    return handleResponse(response);
}

export async function deleteClient(id) {
    const response = await fetch(`${BASE_URL}/DeleteClient/${id}`, {
        method: "DELETE",
        headers: {
            Accept: "application/json",
        },
    });

    return handleResponse(response);
}