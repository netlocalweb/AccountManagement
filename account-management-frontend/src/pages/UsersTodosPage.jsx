import { useEffect, useState } from "react";

function UsersTodosPage() {
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);
    const [todos, setTodos] = useState([]);
    const [error, setError] = useState("");
    const [loadingUsers, setLoadingUsers] = useState(false);
    const [loadingTodos, setLoadingTodos] = useState(false);

    const loadUsers = async () => {
        try {
            setLoadingUsers(true);
            setError("");

            const response = await fetch("https://jsonplaceholder.typicode.com/users");

            if (!response.ok) {
                throw new Error("Could not load users.");
            }

            const data = await response.json();
            setUsers(data);
        } catch (err) {
            setError(err.message || "Could not load users.");
        } finally {
            setLoadingUsers(false);
        }
    };

    useEffect(() => {
        const timer = setTimeout(() => {
            loadUsers();
        }, 0);

        return () => clearTimeout(timer);
    }, []);

    const handleUserClick = async (user) => {
        try {
            setSelectedUser(user);
            setTodos([]);
            setLoadingTodos(true);
            setError("");

            const response = await fetch(
                `https://jsonplaceholder.typicode.com/todos?userId=${user.id}`
            );

            if (!response.ok) {
                throw new Error("Could not load todos.");
            }

            const data = await response.json();
            setTodos(data);
        } catch (err) {
            setError(err.message || "Could not load todos.");
        } finally {
            setLoadingTodos(false);
        }
    };

    return (
        <div className="page-card">
            <p className="top-label">API Practice</p>
            <h2>Users / Todos</h2>

            {error && <p className="error-text">{error}</p>}

            {loadingUsers && <p className="loading-text">Loading users...</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Username</th>
                            <th>Email</th>
                            <th>City</th>
                        </tr>
                    </thead>

                    <tbody>
                        {users.map((user) => (
                            <tr key={user.id} onClick={() => handleUserClick(user)}>
                                <td>{user.id}</td>
                                <td>{user.name}</td>
                                <td>{user.username}</td>
                                <td>{user.email}</td>
                                <td>{user.address.city}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {selectedUser && (
                <div className="details-box">
                    <h3>User Details</h3>

                    <p>
                        <strong>Name:</strong> {selectedUser.name}
                    </p>

                    <p>
                        <strong>Email:</strong> {selectedUser.email}
                    </p>

                    <p>
                        <strong>Phone:</strong> {selectedUser.phone}
                    </p>

                    <p>
                        <strong>Company:</strong> {selectedUser.company.name}
                    </p>

                    <br />

                    <h3>Todos</h3>

                    {loadingTodos ? (
                        <p className="loading-text">Loading todos...</p>
                    ) : (
                        <ul className="todo-list">
                            {todos.map((todo) => (
                                <li key={todo.id}>
                                    {todo.title} —{" "}
                                    {todo.completed ? "Completed" : "Not completed"}
                                </li>
                            ))}
                        </ul>
                    )}
                </div>
            )}
        </div>
    );
}

export default UsersTodosPage;