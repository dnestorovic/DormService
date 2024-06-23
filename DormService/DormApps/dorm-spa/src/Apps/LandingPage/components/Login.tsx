import { useState } from "react";
import IdentityService from "../../../services/IdentityService";
import { Credentials } from "../../../models/User";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [credentials, setCredentials] = useState<Credentials>({userName:"", password:""});

    const navigate = useNavigate();

    const handleInput = (value: string, prop: keyof Credentials) => {
        setCredentials((prev) => ({ ...prev, [prop]: value }));
    };

    const handleLogin = () => {
        if (credentials.password === undefined || credentials.userName === undefined) {
            alert("All fields must be populated");
            return;
        }

        IdentityService.login(credentials)
            .then((tokens) => {
                localStorage.clear();
                localStorage.setItem("access-token", tokens.accessToken);
                localStorage.setItem("refresh-token", tokens.refreshToken);
                localStorage.setItem("username", tokens.userName);
                localStorage.setItem("email", tokens.email);
                localStorage.setItem("first-name", tokens.firstName);
                localStorage.setItem("last-name", tokens.lastName);

                navigate("/payments");
            })
            .catch(() => alert("Cannot find user with this credentials."));
    };

    return (
        <div className="login">
            <div className='image-shadow'>
                
                <div className="login-dialog">
                    <div>
                        <h2>{"Log in"}</h2>
                    </div>
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
