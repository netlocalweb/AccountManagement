import { useState } from "react";
import "./App.css";

import DashboardPage from "./pages/DashboardPage";
import UsersTodosPage from "./pages/UsersTodosPage";

function App() {
    const [currentPage, setCurrentPage] = useState("dashboard");

    const renderPage = () => {
        if (currentPage === "dashboard") {
            return <DashboardPage />;
        }

        if (currentPage === "users-todos") {
            return <UsersTodosPage />;
        }

        return (
            <div className="page-card">
                <h2>{currentPage}</h2>
                <p>Këtë pjesë do ta bëjmë në hapin tjetër.</p>
            </div>
        );
    };

    return (
        <div className="layout">
            <aside className="sidebar">
                <h2>Account Management</h2>

                <button onClick={() => setCurrentPage("dashboard")}>Dashboard</button>
                <button onClick={() => setCurrentPage("clients")}>Clients</button>
                <button onClick={() => setCurrentPage("currencies")}>Currencies</button>
                <button onClick={() => setCurrentPage("accounts")}>Bank Accounts</button>
                <button onClick={() => setCurrentPage("transactions")}>Transactions</button>
                <button onClick={() => setCurrentPage("reports")}>Reports</button>
                <button onClick={() => setCurrentPage("users-todos")}>
                    Users / Todos Exercise
                </button>
            </aside>

            <main className="content">{renderPage()}</main>
        </div>
    );
}

export default App;