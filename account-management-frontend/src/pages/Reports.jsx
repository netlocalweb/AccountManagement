import { useEffect, useState } from "react";
import {
    getAccountReports,
    getAccountTransactionsReport,
    getClientActiveAccountsReport,
} from "../api/reportApi";
import { getBankAccounts } from "../api/bankAccountApi";
import { getClients } from "../api/clientApi";

function getValue(obj, camel, pascal) {
    return obj?.[camel] ?? obj?.[pascal] ?? "";
}

function formatDate(dateValue) {
    if (!dateValue) return "";
    return String(dateValue).replace("T", " ").split(".")[0];
}

function Reports() {
    const [accountReports, setAccountReports] = useState([]);
    const [accountTransactions, setAccountTransactions] = useState([]);
    const [clientAccounts, setClientAccounts] = useState([]);

    const [accounts, setAccounts] = useState([]);
    const [clients, setClients] = useState([]);

    const [selectedAccountId, setSelectedAccountId] = useState("");
    const [selectedClientId, setSelectedClientId] = useState("");

    const [loading, setLoading] = useState(false);
    const [loadingDetails, setLoadingDetails] = useState(false);

    const [error, setError] = useState("");

    const loadInitialData = async () => {
        try {
            setLoading(true);
            setError("");

            const reportsData = await getAccountReports();
            const accountsData = await getBankAccounts();
            const clientsData = await getClients();

            setAccountReports(Array.isArray(reportsData) ? reportsData : []);
            setAccounts(Array.isArray(accountsData) ? accountsData : []);
            setClients(Array.isArray(clientsData) ? clientsData : []);
        } catch (err) {
            setError(err.message || "Could not load reports.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadInitialData();
        }, 0);

        return () => clearTimeout(timer);
    }, []);

    const loadTransactionsForAccount = async () => {
        if (!selectedAccountId) {
            setError("Select a bank account first.");
            return;
        }

        try {
            setLoadingDetails(true);
            setError("");

            const data = await getAccountTransactionsReport(selectedAccountId);
            setAccountTransactions(Array.isArray(data) ? data : []);
        } catch (err) {
            setError(err.message || "Could not load account transactions.");
        } finally {
            setLoadingDetails(false);
        }
    };

    const loadActiveAccountsForClient = async () => {
        if (!selectedClientId) {
            setError("Select a customer first.");
            return;
        }

        try {
            setLoadingDetails(true);
            setError("");

            const data = await getClientActiveAccountsReport(selectedClientId);
            setClientAccounts(Array.isArray(data) ? data : []);
        } catch (err) {
            setError(err.message || "Could not load client active accounts.");
        } finally {
            setLoadingDetails(false);
        }
    };

    return (
        <div className="page-card">
            <div className="section-header">
                <div>
                    <p className="top-label">Reports Module</p>
                    <h2>Reports</h2>
                </div>

                <button
                    className="secondary-btn"
                    type="button"
                    onClick={loadInitialData}
                >
                    Refresh
                </button>
            </div>

            {error && <p className="error-text">{error}</p>}
            {loading && <p className="loading-text">Loading reports...</p>}

            <div className="form-panel">
                <h3>Account Balance Report</h3>

                <div className="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>Client Code</th>
                                <th>Client Name</th>
                                <th>Account Code</th>
                                <th>Account Name</th>
                                <th>Currency</th>
                                <th>Current Balance</th>
                            </tr>
                        </thead>

                        <tbody>
                            {accountReports.map((item, index) => (
                                <tr key={index}>
                                    <td>{getValue(item, "clientCode", "ClientCode")}</td>
                                    <td>{getValue(item, "clientName", "ClientName")}</td>
                                    <td>{getValue(item, "accountCode", "AccountCode")}</td>
                                    <td>{getValue(item, "accountName", "AccountName")}</td>
                                    <td>{getValue(item, "currency", "Currency")}</td>
                                    <td>{getValue(item, "currentBalance", "CurrentBalance")}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>

                {!loading && accountReports.length === 0 && (
                    <p className="loading-text">No account report data found.</p>
                )}
            </div>

            <div className="form-panel">
                <h3>Transactions by Account</h3>

                <div className="form-grid">
                    <div className="input-group">
                        <label>Bank Account</label>
                        <select
                            value={selectedAccountId}
                            onChange={(e) => setSelectedAccountId(e.target.value)}
                        >
                            <option value="">Select account</option>

                            {accounts.map((account) => {
                                const id = getValue(account, "id", "Id");

                                return (
                                    <option key={id} value={id}>
                                        {getValue(account, "code", "Code")} -{" "}
                                        {getValue(account, "name", "Name")}
                                    </option>
                                );
                            })}
                        </select>
                    </div>
                </div>

                <div className="form-actions">
                    <button
                        className="primary-btn"
                        type="button"
                        onClick={loadTransactionsForAccount}
                    >
                        Load Transactions
                    </button>
                </div>

                {loadingDetails && (
                    <p className="loading-text">Loading details...</p>
                )}

                <div className="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>Action</th>
                                <th>Amount</th>
                                <th>Date</th>
                            </tr>
                        </thead>

                        <tbody>
                            {accountTransactions.map((item, index) => (
                                <tr key={index}>
                                    <td>{getValue(item, "action", "Action")}</td>
                                    <td>{getValue(item, "amount", "Amount")}</td>
                                    <td>{formatDate(getValue(item, "date", "Date"))}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>

            <div className="form-panel">
                <h3>Active Accounts by Customer</h3>

                <div className="form-grid">
                    <div className="input-group">
                        <label>Customer</label>
                        <select
                            value={selectedClientId}
                            onChange={(e) => setSelectedClientId(e.target.value)}
                        >
                            <option value="">Select customer</option>

                            {clients.map((client) => {
                                const id = getValue(client, "id", "Id");

                                return (
                                    <option key={id} value={id}>
                                        {getValue(client, "firstName", "FirstName")}{" "}
                                        {getValue(client, "lastName", "LastName")}
                                    </option>
                                );
                            })}
                        </select>
                    </div>
                </div>

                <div className="form-actions">
                    <button
                        className="primary-btn"
                        type="button"
                        onClick={loadActiveAccountsForClient}
                    >
                        Load Active Accounts
                    </button>
                </div>

                <div className="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>Account Code</th>
                                <th>Account Name</th>
                                <th>Currency</th>
                                <th>Current Balance</th>
                            </tr>
                        </thead>

                        <tbody>
                            {clientAccounts.map((item, index) => (
                                <tr key={index}>
                                    <td>{getValue(item, "accountCode", "AccountCode")}</td>
                                    <td>{getValue(item, "accountName", "AccountName")}</td>
                                    <td>{getValue(item, "currency", "Currency")}</td>
                                    <td>{getValue(item, "currentBalance", "CurrentBalance")}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

export default Reports;