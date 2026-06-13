import { apiRequest } from "./apiClient";

export function getBankAccounts() {
    return apiRequest("/api/bankaccounts");
}

export function getBankAccountsByClient(clientId) {
    return apiRequest(`/api/bankaccounts/client/${clientId}`);
}

export function createBankAccount(account) {
    return apiRequest("/api/bankaccounts", {
        method: "POST",
        body: JSON.stringify(account),
    });
}

export function updateBankAccount(id, account) {
    return apiRequest(`/api/bankaccounts/${id}`, {
        method: "PUT",
        body: JSON.stringify(account),
    });
}

export function deleteBankAccount(id) {
    return apiRequest(`/api/bankaccounts/${id}`, {
        method: "DELETE",
    });
}