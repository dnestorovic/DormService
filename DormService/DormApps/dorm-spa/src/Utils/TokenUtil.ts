import jwt_decode from 'jwt-decode';

type JwtPayload = {
    [key: string]: string; 
};

export const getRole = () => {

    const token = localStorage.getItem("access-token");
    if (token !== null) {
        const decodedToken = jwt_decode<JwtPayload>(token);

        const role = Object.keys(decodedToken).map((key: string) => {

            if (key.includes('role')) {
                return decodedToken[key];
            }
            return null;
        }).find(elem => elem !== null);

        return role;
    }
    return '';
    
}
