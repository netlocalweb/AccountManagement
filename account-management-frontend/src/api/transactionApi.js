import { apiRequest } from "./apiClient";

export function getTransactions() {
    return apiRequest("/api/banktransactions");
}

export function getTransactionById(id) {
    return apiRequest(`/api/banktransactions/${id}`);
}

export function getTransactionsByAccount(bankAccountId) {
    return apiRequest(`/api/banktransactions/bankaccount/${bankAccountId}`);
}

export function createTransaction(transaction) {
    return apiRequest("/api/banktransactions", {
        method: "POST",
        body: JSON.stringify(transaction),
    });
}

export function updateTransaction(id, transaction) {
    return apiRequest(`/api/banktransactions/${id}`, {
        method: "PUT",
        body: JSON.stringify(transaction),
    });
}

export function deleteTransaction(id) {
    return apiRequest(`/api/banktransactions/${id}`, {
        method: "DELETE",
    });
}