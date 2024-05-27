import { createContext, useContext } from "react";
import UserStore from "./userStore";
import CommonStore from "./commonStore";

interface Store {
    commonStore: CommonStore
    userStore: UserStore
}

export const store: Store = {
    userStore: new UserStore(),
    commonStore: new CommonStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}