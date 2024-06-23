import { useState } from "react";
import IdentityService from "../../../services/IdentityService";
import { User } from "../../../models/User";
import { useNavigate } from "react-router-dom";

export default function Register() {
    const [registerUser, setRegisterUser] = useState<User>({});

    const message = "Already registered? Log in ";

    const navigate = useNavigate();

    const handleRegisterInput = (value: string, prop: keyof User) => {
        setRegisterUser((prev) => ({ ...prev, [prop]: value }));
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
                navigate("/login");
            })
            .catch(() => alert("Something went wrong!"));
    };

    return (
        <div className="login">
            <div className='image-shadow'>
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
                                navigate("/login");
                            }}
                        >
                        here.
                    </span>
                </p>
                </div>
            </div>
        </div>
    )
}
