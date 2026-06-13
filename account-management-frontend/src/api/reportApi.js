import { apiRequest } from "./apiClient";

export function getAccountReports() {
    return apiRequest("/api/reports/accounts");
}

export function getAccountTransactionsReport(accountId) {
    return apiRequest(`/api/reports/account-transactions/${accountId}`);
}

export function getClientActiveAccountsReport(clientId) {
    return apiRequest(`/api/reports/client-active-accounts/${clientId}`);
}