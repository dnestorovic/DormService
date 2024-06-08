import { useState } from "react";
import "./../design/login.scss"

type CredentialsType = { username?: string; password?: string };

export default function Login() {
    const [credentials, setCredentials] = useState<CredentialsType>({});

    const handleInput = (value: string, prop: keyof CredentialsType) => {
        setCredentials((prev) => ({ ...prev, [prop]: value }));
    };

    const handleLogin = () => {
        alert("API CALL TO LOGIN USER: " + credentials.username)
    };

    return (
        <div className="login">
            <div className='image-shadow'>
                <div className="login-dialog">
                    <div className="form-field">
                        <label>Username:</label>
                        <input
                            value={credentials.username}
                            onChange={(event) => handleInput(event.currentTarget.value, 'username')}
                        />
                    </div>
                    <div className="form-field">
                        <label>Password:</label>
                        <input
                            type="password"
                            value={credentials.password}
                            onChange={(event) => handleInput(event.currentTarget.value, 'password')}
                        />
                    </div>
                    <footer>
                        <button className="btn-submit" onClick={handleLogin}>
                            <span>Submit</span>
                        </button>
                    </footer>
                </div>
            </div>
        </div>
    )
}
