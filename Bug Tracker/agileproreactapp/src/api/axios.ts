import axios, { AxiosError, AxiosResponse } from "axios";
import { User } from "../models/User";
import { UserFormValues } from "../models/UserFormValues";

axios.defaults.baseURL = 'https://localhost:7226/api';

const ResponseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async response => {
    return response;
}, (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    switch (status) {
        case 400:
            break;
        case 401:
            break;
        case 403:
            break;
        case 404:
            break;
        default:
            break;
    }
});

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(ResponseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(ResponseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(ResponseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(ResponseBody)
};

const Auth = {
    login: (user: UserFormValues) => requests.post<User>("/Auth/Login", user),
    register: (user: UserFormValues) => requests.post<User>("/Auth/Register", user)
};

const agent = {
    Auth
};

export default agent;