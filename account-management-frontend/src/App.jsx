import { useState } from "react";
import "./App.css";

import DashboardPage from "./pages/DashboardPage";
import Login from "./pages/Login";
import Clients from "./pages/Clients";
import Currencies from "./pages/Currencies";
import BankAccounts from "./pages/BankAccounts";
import Transactions from "./pages/Transactions";
import Reports from "./pages/Reports";
import UsersTodosPage from "./pages/UsersTodosPage";

function App() {
    const [currentPage, setCurrentPage] = useState("dashboard");

    const mainMenu = [
        { key: "dashboard", label: "Dashboard", icon: "DB" },
        { key: "clients", label: "Customers", icon: "CU" },
        { key: "currencies", label: "Currencies", icon: "FX" },
        { key: "accounts", label: "Bank Accounts", icon: "BA" },
        { key: "transactions", label: "Transactions", icon: "TR" },
        { key: "reports", label: "Reports", icon: "RP" },
    ];

    const bottomMenu = [
        { key: "login", label: "Login", icon: "LG" },
        { key: "users-todos", label: "API Practice", icon: "API" },
    ];

    const allMenu = [...mainMenu, ...bottomMenu];

    const pageTitle =
        allMenu.find((item) => item.key === currentPage)?.label || "Dashboard";

    const renderPage = () => {
        switch (currentPage) {
            case "dashboard":
                return <DashboardPage onNavigate={setCurrentPage} />;

            case "login":
                return <Login />;

            case "clients":
                return <Clients />;

            case "currencies":
                return <Currencies />;

            case "accounts":
                return <BankAccounts />;

            case "transactions":
                return <Transactions />;

            case "reports":
                return <Reports />;

            case "users-todos":
                return <UsersTodosPage />;

            default:
                return <DashboardPage onNavigate={setCurrentPage} />;
        }
    };

    return (
        <div className="bank-layout">
            <aside className="bank-sidebar">
                <div className="bank-brand">
                    <div className="bank-logo">B</div>

                    <div>
                        <h2>BankAcc</h2>
                        <p>Management System</p>
                    </div>
                </div>

                <div className="nav-section">
                    <span className="nav-title">Main</span>

                    {mainMenu.map((item) => (
                        <button
                            key={item.key}
                            className={
                                currentPage === item.key ? "nav-link active" : "nav-link"
                            }
                            onClick={() => setCurrentPage(item.key)}
                            type="button"
                        >
                            <span className="nav-icon">{item.icon}</span>
                            <span>{item.label}</span>
                        </button>
                    ))}
                </div>

                <div className="nav-section bottom">
                    <span className="nav-title">System</span>

                    {bottomMenu.map((item) => (
                        <button
                            key={item.key}
                            className={
                                currentPage === item.key ? "nav-link active" : "nav-link"
                            }
                            onClick={() => setCurrentPage(item.key)}
                            type="button"
                        >
                            <span className="nav-icon">{item.icon}</span>
                            <span>{item.label}</span>
                        </button>
                    ))}
                </div>
            </aside>

            <main className="bank-content">
                <header className="bank-topbar">
                    <div>
                        <p className="top-label">Account Management</p>
                        <h1>{pageTitle}</h1>
                    </div>

                    <div className="top-actions">
                        <span className="api-status">API Connected: localhost:5001</span>
                    </div>
                </header>

                {renderPage()}
            </main>
        </div>
    );
}

export default App;
