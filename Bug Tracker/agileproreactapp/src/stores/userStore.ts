import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../models/User";
import { UserFormValues } from "../models/UserFormValues";
import agent from "../api/axios";
import { store } from "./store";
import router from "../Router/routes";
import { PatchDoc } from "../models/Requests/PatchDoc";

export default class UserStore {
    user: User | null = null
    isUploading = false;
    refreshTokenTimeout?: number

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user
    }

    login = async (creds: UserFormValues) => {
        const user = await agent.Auth.login(creds);
        store.commonStore.setAuthToken(user.authToken);
        this.startRefreshTokenTimer(user);
        runInAction(() => this.user = user);
        router.navigate('dashboard');
    }

    register = async (creds: UserFormValues) => {
        const user = await agent.Auth.register(creds);
        store.commonStore.setAuthToken(user.authToken);
        this.startRefreshTokenTimer(user);
        runInAction(() => this.user = user);
        router.navigate('dashboard');
    }

    logout = () => {
        store.commonStore.setAuthToken(null);
        this.user = null;
        router.navigate('/');
    }

    getCurrentUser = async () => {
        try {
            const user = await agent.Auth.getCurrentUser();
            store.commonStore.setAuthToken(user.authToken);
            this.startRefreshTokenTimer(user);
            runInAction(() => this.user = user);

            if (user)
                router.navigate("dashboard");
        } catch (error) {
            throw error;
        }
    }

    updateUser = async (id: string, fieldName: keyof User, value: any) => {
        const patchDocument: PatchDoc[] = [{ op: "replace", path: `/${fieldName}`, value: value }];
        var user = await agent.Profiles.updateUser(id, patchDocument);
        runInAction(() => {
            if (this.user) {
                this.user = {
                    ...this.user,
                    [fieldName]: user[fieldName]
                };
            }
        })
    }

    uploadPhoto = async (file: Blob) => {
        this.setIsUploading(true);

        try {
            const response = await agent.Profiles.uploadPhoto(file);
            const photoUrl = response.data;
            this.setProfilePhoto(photoUrl);
            this.setIsUploading(false);
        } catch (error) {
            console.log(error);
            this.setIsUploading(false);
        }
    }

    refreshToken = async () => {
        this.stopRefreshTokenTimer();

        try {
            const user = await agent.Auth.refresh();
            runInAction(() => this.user = user);
            store.commonStore.setAuthToken(user.authToken);
            this.startRefreshTokenTimer(user);
        } catch (error) {
            console.log(error);
        }
    }

    //helpers

    setProfilePhoto = (url: string) => {
        if (this.user)
            this.user.profilePictureUrl = url;
    }

    setIsUploading = (state: boolean) => {
        this.isUploading = state;
    }

    private startRefreshTokenTimer(user: User) {
        const jwtToken = JSON.parse(atob(user.authToken.split('.')[1]));
        const expires = new Date(jwtToken.exp * 1000);
        const timeout = expires.getTime() - Date.now() - (60 * 1000);
        this.refreshTokenTimeout = setTimeout(this.refreshToken, timeout);
        console.log({ refreshTimeout: this.refreshTokenTimeout });
    }

    private stopRefreshTokenTimer() {
        clearTimeout(this.refreshTokenTimeout);
    }
}