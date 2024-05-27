import { makeAutoObservable } from "mobx"

export default class CommonStore {
    token: string | null | undefined = null;
    appLoaded = false;

    constructor(){
        makeAutoObservable(this);
    }

    setAuthToken = (token: string | null) => {
        console.log(token);
        if (token)
            localStorage.setItem('authToken', token);

        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}