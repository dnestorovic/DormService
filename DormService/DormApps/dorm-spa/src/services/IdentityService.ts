import { Tokens } from "../models/Tokens";
import { Credentials } from "../models/User";
import BaseService from "./BaseService";

interface IIdentityService {
    login: (credentials: Credentials) => Promise<Tokens>;
}

const IdentityService: () => IIdentityService = () => {
    const baseUrl = 'http://localhost:4000/api/v1/Authentication/Login/';

    const login = (cred: Credentials) => {
        return BaseService.post<Credentials>(baseUrl, cred);
    }

    return { login };
};

export default IdentityService();