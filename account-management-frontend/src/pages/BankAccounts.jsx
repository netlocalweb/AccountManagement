import { useEffect, useState } from "react";
import {
    getBankAccounts,
    createBankAccount,
    updateBankAccount,
    deleteBankAccount,
} from "../api/bankAccountApi";
import { getClients } from "../api/clientApi";
import { getCurrencies } from "../api/currencyApi";

const emptyForm = {
    code: "",
    name: "",
    currencyId: "",
    balance: "",
    clientId: "",
    isActive: true,
};

function getValue(obj, camel, pascal) {
    return obj?.[camel] ?? obj?.[pascal] ?? "";
}

function BankAccounts() {
    const [accounts, setAccounts] = useState([]);
    const [clients, setClients] = useState([]);
    const [currencies, setCurrencies] = useState([]);

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

            const accountsData = await getBankAccounts();
            const clientsData = await getClients();
            const currenciesData = await getCurrencies();

            setAccounts(Array.isArray(accountsData) ? accountsData : []);
            setClients(Array.isArray(clientsData) ? clientsData : []);
            setCurrencies(Array.isArray(currenciesData) ? currenciesData : []);
        } catch (err) {
            setError(err.message || "Could not load bank accounts.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(loadData, 0);
        return () => clearTimeout(timer);
    }, []);

    const clearForm = () => {
        setForm(emptyForm);
        setSelectedId(null);
        setError("");
        setSuccess("");
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setForm((prev) => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value,
        }));
    };

    const selectAccount = (account) => {
        const id = getValue(account, "id", "Id");

        setSelectedId(id);

        setForm({
            code: getValue(account, "code", "Code"),
            name: getValue(account, "name", "Name"),
            currencyId: getValue(account, "currencyId", "CurrencyId"),
            balance: getValue(account, "balance", "Balance"),
            clientId: getValue(account, "clientId", "ClientId"),
            isActive: Boolean(getValue(account, "isActive", "IsActive")),
        });

        setError("");
        setSuccess("");
    };

    const validateForm = () => {
        if (!form.code.trim()) return "Account code is required.";
        if (!form.name.trim()) return "Account name is required.";
        if (!form.currencyId) return "Currency is required.";
        if (!form.clientId) return "Customer is required.";

        if (!selectedId && form.balance === "") {
            return "Initial balance is required.";
        }

        if (Number(form.balance) < 0) {
            return "Balance cannot be negative.";
        }

        return "";
    };

    const buildPayload = () => ({
        code: form.code,
        name: form.name,
        currencyId: Number(form.currencyId),
        balance: Number(form.balance || 0),
        clientId: Number(form.clientId),
        isActive: form.isActive,
    });

    const handleSave = async () => {
        const validationError = validateForm();

        if (validationError) {
            setError(validationError);
            return;
        }

        try {
            setSaving(true);
            setError("");
            setSuccess("");

            const payload = buildPayload();

            if (selectedId) {
                await updateBankAccount(selectedId, payload);
                setSuccess("Bank account updated successfully.");
            } else {
                await createBankAccount(payload);
                setSuccess("Bank account created successfully.");
            }

            clearForm();
            await loadData();
        } catch (err) {
            setError(err.message || "Could not save bank account.");
        } finally {
            setSaving(false);
        }
    };

    const handleDelete = async () => {
        if (!selectedId) {
            setError("Select a bank account first.");
            return;
        }

        if (!window.confirm("Delete this bank account?")) return;

        try {
            setSaving(true);
            setError("");
            setSuccess("");

            await deleteBankAccount(selectedId);

            setSuccess("Bank account deleted successfully.");
            clearForm();
            await loadData();
        } catch (err) {
            setError(err.message || "Could not delete bank account.");
        } finally {
            setSaving(false);
        }
    };

    const getClientName = (clientId) => {
        const client = clients.find(
            (c) => String(getValue(c, "id", "Id")) === String(clientId)
        );

        if (!client) return clientId;

        return `${getValue(client, "firstName", "FirstName")} ${getValue(
            client,
            "lastName",
            "LastName"
        )}`;
    };

    const getCurrencyCode = (currencyId) => {
        const currency = currencies.find(
            (c) => String(getValue(c, "id", "Id")) === String(currencyId)
        );

        return currency ? getValue(currency, "code", "Code") : currencyId;
    };

    return (
        <div className="page-card">
            <div className="section-header">
                <div>
                    <p className="top-label">Banking Module</p>
                    <h2>Bank Accounts</h2>
                </div>

                <button className="secondary-btn" type="button" onClick={loadData}>
                    Refresh
                </button>
            </div>

            {error && <p className="error-text">{error}</p>}
            {success && <p className="success-text">{success}</p>}

            <div className="form-panel">
                <h3>{selectedId ? "Edit Bank Account" : "New Bank Account"}</h3>

                <div className="form-grid">
                    <div className="input-group">
                        <label>Account Code</label>
                        <input
                            name="code"
                            value={form.code}
                            onChange={handleChange}
                            placeholder="ACC001"
                        />
                    </div>

                    <div className="input-group">
                        <label>Account Name</label>
                        <input
                            name="name"
                            value={form.name}
                            onChange={handleChange}
                            placeholder="Main Account"
                        />
                    </div>

                    <div className="input-group">
                        <label>Customer</label>
                        <select
                            name="clientId"
                            value={form.clientId}
                            onChange={handleChange}
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

                    <div className="input-group">
                        <label>Currency</label>
                        <select
                            name="currencyId"
                            value={form.currencyId}
                            onChange={handleChange}
                        >
                            <option value="">Select currency</option>
                            {currencies.map((currency) => {
                                const id = getValue(currency, "id", "Id");

                                return (
                                    <option key={id} value={id}>
                                        {getValue(currency, "code", "Code")} -{" "}
                                        {getValue(currency, "description", "Description")}
                                    </option>
                                );
                            })}
                        </select>
                    </div>

                    <div className="input-group">
                        <label>Balance</label>
                        <input
                            name="balance"
                            type="number"
                            value={form.balance}
                            onChange={handleChange}
                            placeholder="0"
                            disabled={selectedId}
                        />
                    </div>

                    <div className="input-group checkbox-group">
                        <label>Active</label>
                        <input
                            name="isActive"
                            type="checkbox"
                            checked={form.isActive}
                            onChange={handleChange}
                        />
                    </div>
                </div>

                <div className="form-actions">
                    <button
                        className="primary-btn"
                        type="button"
                        onClick={handleSave}
                        disabled={saving}
                    >
                        {selectedId ? "Update Account" : "Create Account"}
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

            {loading && <p className="loading-text">Loading bank accounts...</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Account Code</th>
                            <th>Name</th>
                            <th>Customer</th>
                            <th>Currency</th>
                            <th>Balance</th>
                            <th>Status</th>
                        </tr>
                    </thead>

                    <tbody>
                        {accounts.map((account) => {
                            const id = getValue(account, "id", "Id");
                            const clientId = getValue(account, "clientId", "ClientId");
                            const currencyId = getValue(account, "currencyId", "CurrencyId");
                            const isActive = getValue(account, "isActive", "IsActive");

                            return (
                                <tr
                                    key={id}
                                    onClick={() => selectAccount(account)}
                                    className={selectedId === id ? "selected-row" : ""}
                                >
                                    <td>{id}</td>
                                    <td>{getValue(account, "code", "Code")}</td>
                                    <td>{getValue(account, "name", "Name")}</td>
                                    <td>{getClientName(clientId)}</td>
                                    <td>{getCurrencyCode(currencyId)}</td>
                                    <td>{getValue(account, "balance", "Balance")}</td>
                                    <td>
                                        <span className={isActive ? "status active" : "status inactive"}>
                                            {isActive ? "Active" : "Inactive"}
                                        </span>
                                    </td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>

            {!loading && accounts.length === 0 && !error && (
                <p className="loading-text">No bank accounts found.</p>
            )}
        </div>
    );
}

export default BankAccounts;