import { useEffect, useState } from "react";
import { getClients } from "../api/clientApi";
import { getBankAccounts } from "../api/bankAccountApi";
import { getTransactions } from "../api/transactionApi";
import { getAccountReports } from "../api/reportApi";

function getValue(obj, camel, pascal) {
    return obj?.[camel] ?? obj?.[pascal] ?? 0;
}

function DashboardPage({ onNavigate }) {
    const [stats, setStats] = useState({
        customers: 0,
        accounts: 0,
        transactions: 0,
        totalBalance: 0,
    });

    const [recentAccounts, setRecentAccounts] = useState([]);
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const loadDashboard = async () => {
        try {
            setLoading(true);
            setError("");

            const clientsData = await getClients();
            const accountsData = await getBankAccounts();
            const transactionsData = await getTransactions();
            const reportsData = await getAccountReports();

            const clients = Array.isArray(clientsData) ? clientsData : [];
            const accounts = Array.isArray(accountsData) ? accountsData : [];
            const transactions = Array.isArray(transactionsData)
                ? transactionsData
                : [];
            const reports = Array.isArray(reportsData) ? reportsData : [];

            const totalBalance = reports.reduce((sum, item) => {
                const value =
                    Number(getValue(item, "currentBalance", "CurrentBalance")) || 0;
                return sum + value;
            }, 0);

            setStats({
                customers: clients.length,
                accounts: accounts.length,
                transactions: transactions.length,
                totalBalance,
            });

            setRecentAccounts(accounts.slice(0, 5));
        } catch (err) {
            setError(err.message || "Could not load dashboard data.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadDashboard();
        }, 0);

        return () => clearTimeout(timer);
    }, []);

    return (
        <div className="dashboard-page">
            <section className="hero-bank-card">
                <div>
                    <p className="top-label">Welcome back</p>
                    <h2>Bank Account Management</h2>
                    <p>
                        Manage customers, currencies, bank accounts, transactions and
                        reports from one dashboard.
                    </p>
                </div>

                <div className="hero-actions">
                    <button
                        className="primary-btn"
                        type="button"
                        onClick={() => onNavigate("clients")}
                    >
                        Add Customer
                    </button>

                    <button
                        className="secondary-btn"
                        type="button"
                        onClick={loadDashboard}
                    >
                        Refresh
                    </button>
                </div>
            </section>

            {error && <p className="error-text">{error}</p>}
            {loading && <p className="loading-text">Loading dashboard...</p>}

            <section className="stats-grid">
                <div className="stat-card">
                    <div className="stat-icon">👥</div>
                    <p>Total Customers</p>
                    <h3>{stats.customers}</h3>
                </div>

                <div className="stat-card">
                    <div className="stat-icon">💳</div>
                    <p>Bank Accounts</p>
                    <h3>{stats.accounts}</h3>
                </div>

                <div className="stat-card">
                    <div className="stat-icon">🔁</div>
                    <p>Transactions</p>
                    <h3>{stats.transactions}</h3>
                </div>

                <div className="stat-card">
                    <div className="stat-icon">💰</div>
                    <p>Total Balance</p>
                    <h3>{stats.totalBalance.toFixed(2)}</h3>
                </div>
            </section>

            <section className="dashboard-grid">
                <div className="page-card">
                    <div className="section-header">
                        <div>
                            <p className="top-label">Quick Access</p>
                            <h2>Modules</h2>
                        </div>
                    </div>

                    <div className="quick-grid">
                        <button onClick={() => onNavigate("clients")}>
                            <strong>Customers</strong>
                            <span>Create and manage customers</span>
                        </button>

                        <button onClick={() => onNavigate("currencies")}>
                            <strong>Currencies</strong>
                            <span>Manage master currency data</span>
                        </button>

                        <button onClick={() => onNavigate("accounts")}>
                            <strong>Bank Accounts</strong>
                            <span>Open and edit accounts</span>
                        </button>

                        <button onClick={() => onNavigate("transactions")}>
                            <strong>Transactions</strong>
                            <span>Deposit and withdraw money</span>
                        </button>

                        <button onClick={() => onNavigate("reports")}>
                            <strong>Reports</strong>
                            <span>View balances and history</span>
                        </button>
                    </div>
                </div>

                <div className="page-card">
                    <div className="section-header">
                        <div>
                            <p className="top-label">Latest</p>
                            <h2>Recent Accounts</h2>
                        </div>
                    </div>

                    <div className="mini-list">
                        {recentAccounts.map((account) => {
                            const id = getValue(account, "id", "Id");

                            return (
                                <div className="mini-list-item" key={id}>
                                    <div>
                                        <strong>{getValue(account, "code", "Code")}</strong>
                                        <p>{getValue(account, "name", "Name")}</p>
                                    </div>

                                    <span>
                                        {Number(getValue(account, "balance", "Balance") || 0).toFixed(
                                            2
                                        )}
                                    </span>
                                </div>
                            );
                        })}

                        {!loading && recentAccounts.length === 0 && (
                            <p className="loading-text">No accounts yet.</p>
                        )}
                    </div>
                </div>
            </section>
        </div>
    );
}

export default DashboardPage;