import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../models/User";
import { UserFormValues } from "../models/UserFormValues";
import agent from "../api/axios";
import { store } from "./store";
import router from "../routes";

export default class UserStore {
    user: User | null = null

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user
    }

    login = async (creds: UserFormValues) => {
        const user = await agent.Auth.login(creds);
        console.log(user);
        store.commonStore.setAuthToken(user.authToken);
        runInAction(() => this.user = user);
        router.navigate('dashboard');
    }

    register = async (creds: UserFormValues) => {
        const user = await agent.Auth.register(creds);
        console.log(user);
    }

    logout = () => {
        store.commonStore.setAuthToken(null);
        localStorage.removeItem("authToken");
        this.user = null;
        router.navigate('/');
    }
}