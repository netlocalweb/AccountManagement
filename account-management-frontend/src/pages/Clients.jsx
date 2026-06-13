import { useEffect, useState } from "react";
import { getClients } from "../api/clientApi";

function Clients() {
    const [clients, setClients] = useState([]);
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const loadClients = async () => {
        try {
            setLoading(true);
            setError("");

            const data = await getClients();
            setClients(Array.isArray(data) ? data : []);
        } catch (err) {
            setError(err.message || "Could not load customers.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadClients();
        }, 0);

        return () => clearTimeout(timer);
    }, []);

    return (
        <div className="page-card">
            <div className="section-header">
                <div>
                    <p className="top-label">Customer Management</p>
                    <h2>Customers</h2>
                </div>

                <button className="secondary-btn" type="button" onClick={loadClients}>
                    Refresh
                </button>
            </div>

            {loading && <p className="loading-text">Loading customers...</p>}

            {error && <p className="error-text">{error}</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Username</th>
                            <th>Email</th>
                            <th>Phone</th>
                        </tr>
                    </thead>

                    <tbody>
                        {clients.map((client) => (
                            <tr key={client.id || client.Id}>
                                <td>{client.id || client.Id}</td>
                                <td>{client.firstName || client.FirstName}</td>
                                <td>{client.lastName || client.LastName}</td>
                                <td>{client.username || client.Username}</td>
                                <td>{client.email || client.Email}</td>
                                <td>{client.phone || client.Phone}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {!loading && clients.length === 0 && !error && (
                <p className="loading-text">No customers found in database.</p>
            )}
        </div>
    );
}

export default Clients;