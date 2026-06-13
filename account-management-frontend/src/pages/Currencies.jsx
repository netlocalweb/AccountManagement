import { useEffect, useState } from "react";
import {
    getCurrencies,
    createCurrency,
    updateCurrency,
    deleteCurrency,
} from "../api/currencyApi";

const emptyForm = {
    code: "",
    description: "",
    exchangeRate: "",
};

function getValue(obj, camel, pascal) {
    return obj?.[camel] ?? obj?.[pascal] ?? "";
}

function Currencies() {
    const [currencies, setCurrencies] = useState([]);
    const [form, setForm] = useState(emptyForm);
    const [selectedId, setSelectedId] = useState(null);
    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const loadCurrencies = async () => {
        try {
            setLoading(true);
            setError("");

            const data = await getCurrencies();
            setCurrencies(Array.isArray(data) ? data : []);
        } catch (err) {
            setError(err.message || "Could not load currencies.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(loadCurrencies, 0);
        return () => clearTimeout(timer);
    }, []);

    const clearForm = () => {
        setForm(emptyForm);
        setSelectedId(null);
        setError("");
        setSuccess("");
    };

    const selectCurrency = (currency) => {
        setSelectedId(getValue(currency, "id", "Id"));
        setForm({
            code: getValue(currency, "code", "Code"),
            description: getValue(currency, "description", "Description"),
            exchangeRate: getValue(currency, "exchangeRate", "ExchangeRate"),
        });
    };

    const handleSave = async () => {
        try {
            setSaving(true);
            setError("");
            setSuccess("");

            const payload = {
                code: form.code,
                description: form.description,
                exchangeRate: Number(form.exchangeRate),
            };

            if (selectedId) {
                await updateCurrency(selectedId, payload);
                setSuccess("Currency updated successfully.");
            } else {
                await createCurrency(payload);
                setSuccess("Currency created successfully.");
            }

            clearForm();
            await loadCurrencies();
        } catch (err) {
            setError(err.message || "Could not save currency.");
        } finally {
            setSaving(false);
        }
    };

    const handleDelete = async () => {
        if (!selectedId) {
            setError("Select a currency first.");
            return;
        }

        if (!window.confirm("Delete this currency?")) return;

        try {
            setSaving(true);
            setError("");

            await deleteCurrency(selectedId);
            setSuccess("Currency deleted successfully.");
            clearForm();
            await loadCurrencies();
        } catch (err) {
            setError(err.message || "Could not delete currency.");
        } finally {
            setSaving(false);
        }
    };

    return (
        <div className="page-card">
            <div className="section-header">
                <div>
                    <p className="top-label">Master Data</p>
                    <h2>Currencies</h2>
                </div>

                <button className="secondary-btn" type="button" onClick={loadCurrencies}>
                    Refresh
                </button>
            </div>

            {error && <p className="error-text">{error}</p>}
            {success && <p className="success-text">{success}</p>}

            <div className="form-panel">
                <h3>{selectedId ? "Edit Currency" : "New Currency"}</h3>

                <div className="form-grid">
                    <div className="input-group">
                        <label>Code</label>
                        <input
                            value={form.code}
                            onChange={(e) => setForm({ ...form, code: e.target.value })}
                            placeholder="EUR"
                        />
                    </div>

                    <div className="input-group">
                        <label>Description</label>
                        <input
                            value={form.description}
                            onChange={(e) =>
                                setForm({ ...form, description: e.target.value })
                            }
                            placeholder="Euro"
                        />
                    </div>

                    <div className="input-group">
                        <label>Exchange Rate</label>
                        <input
                            type="number"
                            value={form.exchangeRate}
                            onChange={(e) =>
                                setForm({ ...form, exchangeRate: e.target.value })
                            }
                            placeholder="1"
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
                        {selectedId ? "Update Currency" : "Create Currency"}
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

                    <button className="secondary-btn" type="button" onClick={clearForm}>
                        Clear
                    </button>
                </div>
            </div>

            {loading && <p className="loading-text">Loading currencies...</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Code</th>
                            <th>Description</th>
                            <th>Exchange Rate</th>
                        </tr>
                    </thead>

                    <tbody>
                        {currencies.map((currency) => (
                            <tr
                                key={getValue(currency, "id", "Id")}
                                onClick={() => selectCurrency(currency)}
                            >
                                <td>{getValue(currency, "id", "Id")}</td>
                                <td>{getValue(currency, "code", "Code")}</td>
                                <td>{getValue(currency, "description", "Description")}</td>
                                <td>{getValue(currency, "exchangeRate", "ExchangeRate")}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default Currencies;