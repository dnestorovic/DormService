import { Tokens } from "../models/Tokens";
import { Credentials, User } from "../models/User";
import BaseService from "./BaseService";

interface IIdentityService {
    login: (credentials: Credentials) => Promise<Tokens>;
    register: (userInfo: User) => Promise<boolean>;
}

const IdentityService: () => IIdentityService = () => {
    const baseUrl = 'http://localhost:4000/api/v1/Authentication/Login';
    const registerUrl = 'http://localhost:4000/api/v1/Authentication/RegisterStudent';

    const login = (cred: Credentials) => {
        console.log(cred);
        return BaseService.post<Credentials>(baseUrl, cred);
    }

    const register = (userInfo: User) => {
        return BaseService.post<User>(registerUrl, userInfo);
    }

    return { login, register };
};

export default IdentityService();