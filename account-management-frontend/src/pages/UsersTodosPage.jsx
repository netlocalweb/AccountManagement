import { useEffect, useState } from "react";

function UsersTodosPage() {
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);
    const [todos, setTodos] = useState([]);
    const [loadingUsers, setLoadingUsers] = useState(false);
    const [loadingTodos, setLoadingTodos] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        loadUsers();
    }, []);

    const loadUsers = async () => {
        try {
            setLoadingUsers(true);
            setError("");

            const response = await fetch("https://jsonplaceholder.typicode.com/users");

            if (!response.ok) {
                throw new Error("Gabim gjatë marrjes së users.");
            }

            const data = await response.json();
            setUsers(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingUsers(false);
        }
    };

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
                throw new Error("Gabim gjatë marrjes së todos.");
            }

            const data = await response.json();
            setTodos(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingTodos(false);
        }
    };

    return (
        <div className="page-card">
            <h2>Users / Todos Exercise</h2>
            <p>
                Kjo është pjesa e ushtrimit me React, fetch, promises dhe async/await.
            </p>

            {error && <p className="error-text">{error}</p>}
            {loadingUsers && <p>Po ngarkohen users...</p>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Emri</th>
                            <th>Username</th>
                            <th>Email</th>
                            <th>Qyteti</th>
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
                    <h3>Detajet e user-it të klikuar</h3>
                    <br />

                    <p>
                        <strong>Emri:</strong> {selectedUser.name}
                    </p>
                    <p>
                        <strong>Username:</strong> {selectedUser.username}
                    </p>
                    <p>
                        <strong>Email:</strong> {selectedUser.email}
                    </p>
                    <p>
                        <strong>Telefon:</strong> {selectedUser.phone}
                    </p>
                    <p>
                        <strong>Website:</strong> {selectedUser.website}
                    </p>
                    <p>
                        <strong>Kompania:</strong> {selectedUser.company.name}
                    </p>

                    <br />

                    <h3>Todos të këtij user-i</h3>

                    {loadingTodos ? (
                        <p>Po ngarkohen todos...</p>
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