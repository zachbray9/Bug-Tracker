import { createContext, useContext } from "react";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import ProjectStore from "./projectStore";

interface Store {
    commonStore: CommonStore
    userStore: UserStore
    projectStore: ProjectStore
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    projectStore: new ProjectStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}