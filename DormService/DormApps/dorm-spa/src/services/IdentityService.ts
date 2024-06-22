import { Tokens } from "../models/Tokens";
import { Credentials, LogOutUser, User } from "../models/User";
import BaseService from "./BaseService";

interface IIdentityService {
    login: (credentials: Credentials) => Promise<Tokens>;
    register: (userInfo: User) => Promise<boolean>;
    logout: (logoutInfo: LogOutUser) => Promise<boolean>;
}

const IdentityService: () => IIdentityService = () => {
    const baseUrl = 'http://localhost:4000/api/v1/Authentication/';

    const login = (cred: Credentials) => {
        return BaseService.post<Credentials>(`${baseUrl}Login`, cred);
    }

    const register = (userInfo: User) => {
        return BaseService.post<User>(`${baseUrl}RegisterStudent`, userInfo);
    }

    const logout = (logoutInfo: LogOutUser) => {
        return BaseService.post<LogOutUser>(`${baseUrl}Logout`, logoutInfo);
    }

    return { login, register, logout };
};

export default IdentityService();