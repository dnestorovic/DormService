import { Tokens } from "../models/Tokens";
import { Credentials, LogOutUser, User } from "../models/User";
import BaseService from "./BaseService";

interface IIdentityService {
    login: (credentials: Credentials) => Promise<Tokens>;
    register: (userInfo: User) => Promise<boolean>;
    logout: (logoutInfo: LogOutUser) => Promise<boolean>;
}

const IdentityService: () => IIdentityService = () => {
    const baseUrl = 'http://localhost:4000/api/v1/Authentication/Login';
    const registerUrl = 'http://localhost:4000/api/v1/Authentication/RegisterStudent';
    const logOutUrl = 'http://localhost:4000/api/v1/Authentication/Logout'

    const login = (cred: Credentials) => {
        return BaseService.post<Credentials>(baseUrl, cred);
    }

    const register = (userInfo: User) => {
        return BaseService.postNoData<User>(registerUrl, userInfo);
    }

    const logout = (logoutInfo: LogOutUser) => {
        return BaseService.postNoData<LogOutUser>(logOutUrl, logoutInfo);
    }

    return { login, register, logout };
};

export default IdentityService();