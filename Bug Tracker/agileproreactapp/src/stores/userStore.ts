import { makeAutoObservable } from "mobx";
import { User } from "../models/User";
import { UserFormValues } from "../models/UserFormValues";
import agent from "../api/axios";

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
    }

    register = async (creds: UserFormValues) => {
        const user = await agent.Auth.register(creds);
        console.log(user);
    }
}