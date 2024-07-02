import { createContext, useContext } from "react";
import UserStore from "./userStore";
import CommonStore from "./commonStore";
import ProjectStore from "./projectStore";
import TicketStore from "./ticketStore";
import CommentStore from "./commentStore";

interface Store {
    commonStore: CommonStore
    userStore: UserStore
    projectStore: ProjectStore
    ticketStore: TicketStore
    commentStore: CommentStore
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    projectStore: new ProjectStore(),
    ticketStore: new TicketStore(),
    commentStore: new CommentStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}