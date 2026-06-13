import { useEffect, useState } from "react";
import {
    getTransactions,
    createTransaction,
    deleteTransaction,
} from "../api/transactionApi";
import { getBankAccounts } from "../api/bankAccountApi";

const emptyForm = {
    bankAccountId: "",
    action: "1",
    amount: "",
};

function getValue(obj, camel, pascal) {
    return obj?.[camel] ?? obj?.[pascal] ?? "";
}

function getActionText(action) {
    const value = Number(action);

    if (value === 1) return "Deposit";
    if (value === 2) return "Withdraw";

    return action;
}

function formatDate(dateValue) {
    if (!dateValue) return "";
    return String(dateValue).replace("T", " ").split(".")[0];
}

function Transactions() {
    const [transactions, setTransactions] = useState([]);
    const [accounts, setAccounts] = useState([]);

    const [form, setForm] = useState(emptyForm);
    const [selectedId, setSelectedId] = useState(null);

    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const transactionsData = await getTransactions();
            const accountsData = await getBankAccounts();

            setTransactions(Array.isArray(transactionsData) ? transactionsData : []);
            setAccounts(Array.isArray(accountsData) ? accountsData : []);
        } catch (err) {
            setError(err.message || "Could not load transactions.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadData();
        }, 0);

        return () => clearTimeout(timer);
    }, []);

    const clearForm = () => {
        setForm(emptyForm);
        setSelectedId(null);
        setError("");
        setSuccess("");
    };

    const handleChange = (e) => {
        const { name, value } = e.target;

        setForm((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const selectTransaction = (transaction) => {
        const id = getValue(transaction, "id", "Id");

        setSelectedId(id);

        setForm({
            bankAccountId: getValue(
                transaction,
                "bankAccountId",
                "BankAccountId"
            ),
            action: String(getValue(transaction, "action", "Action")),
            amount: getValue(transaction, "amount", "Amount"),
        });

        setError("");
        setSuccess("");
    };

    const validateForm = () => {
        if (!form.bankAccountId) return "Select a bank account.";
        if (!form.action) return "Select transaction action.";
        if (!form.amount) return "Amount is required.";
        if (Number(form.amount) <= 0) return "Amount must be greater than 0.";

        return "";
    };

    const handleCreate = async () => {
        const validationError = validateForm();

        if (validationError) {
            setError(validationError);
            return;
        }

        try {
            setSaving(true);
            setError("");
            setSuccess("");

            const payload = {
                bankAccountId: Number(form.bankAccountId),
                action: Number(form.action),
                amount: Number(form.amount),
            };

            await createTransaction(payload);

            setSuccess("Transaction created successfully.");
            clearForm();
            await loadData();
        } catch (err) {
            setError(err.message || "Could not create transaction.");
        } finally {
            setSaving(false);
        }
    };

    const handleDelete = async () => {
        if (!selectedId) {
            setError("Select a transaction first.");
            return;
        }

        if (!window.confirm("Delete this transaction?")) return;

        try {
            setSaving(true);
            setError("");
            setSuccess("");

            await deleteTransaction(selectedId);

            setSuccess("Transaction deleted successfully.");
            clearForm();
            await loadData();
        } catch (err) {
            setError(err.message || "Could not delete transaction.");
        } finally {
            setSaving(false);
        }
    };

    const getAccountLabel = (bankAccountId) => {
        const account = accounts.find(
            (a) => String(getValue(a, "id", "Id")) === String(bankAccountId)
        );

        if (!account) return bankAccountId;

        return `${getValue(account, "code", "Code")} - ${getValue(
            account,
            "name",
            "Name"
        )}`;
    };

    return (
        <div className="page-card">
            <div className="section-header">
                <div>
                    <p className="top-label">Banking Module</p>
                    <h2>Transactions</h2>
                </div>

                <button className="secondary-btn" type="button" onClick={loadData}>
                    Refresh
                </button>
            </div>

            {error && <p className="error-text">{error}</p>}
            {success && <p className="success-text">{success}</p>}

            <div className="form-panel">
                <h3>{selectedId ? "Selected Transaction" : "New Transaction"}</h3>

                <div className="form-grid">
                    <div className="input-group">
                        <label>Bank Account</label>
                        <select
                            name="bankAccountId"
                            value={form.bankAccountId}
                            onChange={handleChange}
                        >
                            <option value="">Select account</option>

                            {accounts.map((account) => {
                                const id = getValue(account, "id", "Id");

                                return (
                                    <option key={id} value={id}>
                                        {getValue(account, "code", "Code")} -{" "}
                                        {getValue(account, "name", "Name")} | Balance:{" "}
                                        {getValue(account, "balance", "Balance")}
                                    </option>
                                );
                            })}
                        </select>
                    </div>

                    <div className="input-group">
                        <label>Action</label>
                        <select name="action" value={form.action} onChange={handleChange}>
                            <option value="1">Deposit</option>
                            <option value="2">Withdraw</option>
                        </select>
                    </div>

                    <div className="input-group">
                        <label>Amount</label>
                        <input
                            name="amount"
                            type="number"
                            value={form.amount}
                            onChange={handleChange}
                            placeholder="100"
                        />
                    </div>
                </div>

                <div className="form-actions">
                    <button
                        className="primary-btn"
                        type="button"
                        onClick={handleCreate}
                        disabled={saving}
                    >
                        {saving ? "Saving..." : "Create Transaction"}
                    </button>

                    {selectedId && (
                        <button
                            className="danger-btn"
                            type="button"
                            onClick={handleDelete}
                            disabled={saving}
                        >
                            Delete
                        </button>
                    )}

                    <button
                        className="secondary-btn"
                        type="button"
                        onClick={clearForm}
                        disabled={saving}
                    >
                        Clear
                    </button>
                </div>
            </div>

            {loading && <p className="loading-text">Loading transactions...</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Bank Account</th>
                            <th>Action</th>
                            <th>Amount</th>
                            <th>Date</th>
                        </tr>
                    </thead>

                    <tbody>
                        {transactions.map((transaction) => {
                            const id = getValue(transaction, "id", "Id");
                            const bankAccountId = getValue(
                                transaction,
                                "bankAccountId",
                                "BankAccountId"
                            );
                            const action = getValue(transaction, "action", "Action");
                            const dateCreated = getValue(
                                transaction,
                                "dateCreated",
                                "DateCreated"
                            );

                            return (
                                <tr
                                    key={id}
                                    onClick={() => selectTransaction(transaction)}
                                    className={selectedId === id ? "selected-row" : ""}
                                >
                                    <td>{id}</td>
                                    <td>{getAccountLabel(bankAccountId)}</td>
                                    <td>
                                        <span
                                            className={
                                                Number(action) === 1
                                                    ? "status active"
                                                    : "status inactive"
                                            }
                                        >
                                            {getActionText(action)}
                                        </span>
                                    </td>
                                    <td>{getValue(transaction, "amount", "Amount")}</td>
                                    <td>{formatDate(dateCreated)}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>

            {!loading && transactions.length === 0 && !error && (
                <p className="loading-text">No transactions found.</p>
            )}
        </div>
    );
}

export default Transactions;