import { apiRequest } from "./apiClient";

export function getCurrencies() {
    return apiRequest("/api/currency/GetAllCurrencies");
}

export function createCurrency(currency) {
    return apiRequest("/api/currency/CreateCurrency", {
        method: "POST",
        body: JSON.stringify(currency),
    });
}

export function updateCurrency(id, currency) {
    return apiRequest(`/api/currency/UpdateCurrency/${id}`, {
        method: "PUT",
        body: JSON.stringify(currency),
    });
}

export function deleteCurrency(id) {
    return apiRequest(`/api/currency/DeleteCurrency/${id}`, {
        method: "DELETE",
    });
}