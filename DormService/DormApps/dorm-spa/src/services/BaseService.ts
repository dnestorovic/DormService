interface IBaseService {
    get: (url: string) => Promise<any>;
    post: <T>(url: string, data: T) => Promise<any>;
    put: <T>(url: string, data: T) => Promise<any>;
    head: (url: string) => Promise<any>;
    delete: (url: string) => Promise<any>;
}

const BaseService = (): IBaseService => {

    const get = (url: string) => {
        return fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('access-token')}`
            },
            mode: 'cors'
        }).then((data) => {
            if (data.status > 400) {
                throw new Error('Bad request - invalid data');
            }

            const contentType = data.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return data.json();
            } else {
                return data.text();
            }
        })
    }

    const post = <T>(url: string, data: T) => {
        return fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('access-token')}`
            },
            body: JSON.stringify(data)
        }).then((res) => {
            if (res.status >= 400) {
                throw new Error('Bad request - invalid data');
            }
            const contentType = res.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return res.json();
            } else {
                return res;
            }
        });
    }

    const put = <T>(url: string, data: T) => {
        return fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('access-token')}`
            },
            body: JSON.stringify(data)
        }).then((res) => {
            if (res.status > 400) {
                throw new Error('Bad request - invalid data');
            }

            return res;
        });
    }

    const head = (url: string) => {
        return fetch(url, {
            method: 'HEAD',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('access-token')}`
            },
        }).then((res) => {
            if (res.status >= 400) {
                throw new Error('Bad request - invalid data');
            }

            return res;
        });
    }

    const deleteItem = (url: string) => {
        return fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('access-token')}`
            }
        }).then((res) => {
            console.log(res.status);
            if (res.status >= 400) {
                throw new Error('Bad request - invalid data');
            }

            return res;
        });
    }

    return { get, post, put, head, delete: deleteItem }
}

export default BaseService();