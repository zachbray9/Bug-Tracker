import axios, { AxiosResponse } from "axios";

axios.create({
    baseURL: "https://localhost:7226"
});

const ResponseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(ResponseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(ResponseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(ResponseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(ResponseBody)
}

export default axios;