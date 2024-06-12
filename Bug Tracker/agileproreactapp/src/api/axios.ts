import axios, { AxiosError, AxiosResponse } from "axios";
import { User } from "../models/User";
import { UserFormValues } from "../models/UserFormValues";
import { store } from "../stores/store";
import { Project } from "../models/Project";
import { ProjectFormValues } from "../models/ProjectFormValues";
import { v4 as uuidv4 } from "uuid";

axios.defaults.baseURL = 'https://localhost:7226/api';

const ResponseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
})

axios.interceptors.response.use(async response => {
    return response;
}, (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    switch (status) {
        case 400:
            console.log(data)
            break;
        case 401:
            console.log(data)
            break;
        case 403:
            console.log(data)
            break;
        case 404:
            console.log(data)
            break;
        default:
            console.log(data)
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
    register: (user: UserFormValues) => requests.post<User>("/Auth/Register", user),
    getCurrentUser: () => requests.get<User>("/Auth/CurrentUser")
};

const Projects = {
    getCurrentUserProjects: () => requests.get<Project[]>("/Projects"),
    createProject: (project: ProjectFormValues) => requests.post<Project>("/Projects", project)
}

const Profiles = {
    uploadPhoto: (file: Blob) => {
        const uniqueFileName = uuidv4();
        let formData = new FormData();
        formData.append("file", file, `${uniqueFileName}.${file.type.split("/")[1]}`);
        return axios.post<string>("/Photos", formData, {
            headers: { "Content-Type": "multipart/form-data"}
        })
    }
}

const agent = {
    Auth,
    Projects,
    Profiles
};

export default agent;