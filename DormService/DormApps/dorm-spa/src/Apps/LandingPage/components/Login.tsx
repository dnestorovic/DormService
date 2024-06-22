import { useState } from "react";
import IdentityService from "../../../services/IdentityService";
import { Credentials, User } from "../../../models/User";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [registered, setRegistered] = useState(false);
    const [credentials, setCredentials] = useState<Credentials>({userName:"", password:""});
    const [registerUser, setRegisterUser] = useState<User>({});

    const message = "Already registered? Log in ";

    const navigate = useNavigate();

    const handleInput = (value: string, prop: keyof Credentials) => {
        setCredentials((prev) => ({ ...prev, [prop]: value }));
    };

    const handleRegisterInput = (value: string, prop: keyof User) => {
        setRegisterUser((prev) => ({ ...prev, [prop]: value }));
    };

    const handleLogin = () => {
        console.log(credentials);
        console.log(registerUser);
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
                console.log(localStorage);

                navigate("/payments");
            })
            .catch(() => alert("Cannot find user with this credentials."));
    };

    const handleRegister = () => {
        console.log(registerUser);
        if (registerUser.password === undefined 
            || registerUser.userName === undefined
            || registerUser.firstName === undefined
            || registerUser.lastName === undefined
            || registerUser.email === undefined
        ) {
            alert("All fields must be populated");
            return;
        }

        IdentityService.register(registerUser)
            .then(() => {
                setRegisterUser({});
                setCredentials({});
                setRegistered(true);
            })
            .catch(() => alert("Something went wrong!"));
    };

    return (
        <div className="login">
            <div className='image-shadow'>
                
                {registered && 
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
                }
                {!registered &&
                    <div className="register-dialog">
                        <div>
                            <h2>{"Register"}</h2>
                        </div>
                        <div className="form-field">
                            <label>First name:</label>
                            <input
                                value={registerUser.firstName}
                                onChange={(event) => handleRegisterInput(event.currentTarget.value, 'firstName')}
                            />
                        </div>
                        <div className="form-field">
                            <label>Last name:</label>
                            <input
                                value={registerUser.lastName}
                                onChange={(event) => handleRegisterInput(event.currentTarget.value, 'lastName')}
                            />
                        </div>
                        <div className="form-field">
                            <label>Username:</label>
                            <input
                                value={registerUser.userName}
                                onChange={(event) => handleRegisterInput(event.currentTarget.value, 'userName')}
                            />
                        </div>
                        <div className="form-field">
                            <label>Password:</label>
                            <input
                                type="password"
                                value={registerUser.password}
                                onChange={(event) => handleRegisterInput(event.currentTarget.value, 'password')}
                            />
                        </div>
                        <div className="form-field">
                            <label>Email:</label>
                            <input
                                type="email"
                                value={registerUser.email}
                                onChange={(event) => handleRegisterInput(event.currentTarget.value, 'email')}
                            />
                        </div>
                        <footer>
                            <button className="btn-submit" onClick={handleRegister}>
                                <span>Submit</span>
                            </button>
                        </footer>
                        <p>
                            {message}
                            <span
                                onClick={() => {
                                    if (!registered) {
                                        setRegistered(true);
                                    }
                                }}
                            >
                            here.
                        </span>
                    </p>
                    </div>
                }
            </div>
        </div>
    )
}
