import { makeAutoObservable, runInAction } from "mobx";
import { Ticket } from "../models/Ticket";
import { TicketFormValues } from "../models/TicketFormValues";
import agent from "../api/axios";
import { store } from "./store";

export default class TicketStore {
    selectedTicket: Ticket | null = null;
    isModalOpen = false;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    setSelectedTicket = (ticket: Ticket) => {
        this.selectedTicket = ticket;
        this.isModalOpen = true;
    }

    clearSelectedTicket = () => {
        this.selectedTicket = null;
        this.isModalOpen = false;
    }

    createTicket = async (creds: TicketFormValues) => {
        this.setIsLoading(true)

        try {
            var ticket = await agent.Tickets.createTicket(creds);
            console.log(ticket);
            runInAction(() => { store.projectStore.selectedProject?.tickets.push(ticket) });
            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    updateTicket = async (creds: TicketFormValues) => {
        this.setIsLoading(true);

        try {
            var updatedTicket = await agent.Tickets.updateTicket(creds);
            console.log(updatedTicket);

            runInAction(() => {
                if (store.projectStore.selectedProject) {
                    const index = store.projectStore.selectedProject.tickets.findIndex(ticket => ticket.id === updatedTicket.id);
                    if (index !== -1) {
                        store.projectStore.selectedProject.tickets[index] = updatedTicket;
                    }
                }
            });

            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    setIsLoading = (value: boolean) => {
        this.isLoading = value;
    }
}