import { useState } from "react";
import IdentityService from "../../../services/IdentityService";
import { Credentials } from "../../../models/User";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [credentials, setCredentials] = useState<Credentials>({});

    const navigate = useNavigate();

    const handleInput = (value: string, prop: keyof Credentials) => {
        setCredentials((prev) => ({ ...prev, [prop]: value }));
    };

    const handleLogin = () => {
        console.log(credentials);
        if (credentials.password === undefined || credentials.userName === undefined) {
            alert("All fields must be populated");
            return;
        }

        IdentityService.login(credentials)
            .then((tokens) => {
                localStorage.clear();
                localStorage.setItem("access-token", tokens.AccessToken);
                localStorage.setItem("refresh-token", tokens.RefreshToken);
                localStorage.setItem("username", tokens.UserName);
                localStorage.setItem("email", tokens.UserEmail);

                navigate("/payments");
            })
            .catch(() => alert("Cannot find user with this credentials."));
    };

    return (
        <div className="login">
            <div className='image-shadow'>
                <div className="login-dialog">
                    <div className="form-field">
                        <label>Username:</label>
                        <input
                            value={credentials.userName}
                            onChange={(event) => handleInput(event.currentTarget.value, 'userName')}
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
