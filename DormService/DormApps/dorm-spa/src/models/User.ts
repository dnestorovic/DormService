export type User = {
    userName?: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    email?: string;
}

export type Credentials = {
    userName?: string;
    password?: string;
}

export type LogOutUser = {
    userName?: string;
    refreshToken?: string;
}