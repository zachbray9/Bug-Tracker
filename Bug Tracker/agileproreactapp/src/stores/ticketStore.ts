import { makeAutoObservable, runInAction } from "mobx";
import { Ticket } from "../models/Ticket";
import { TicketFormValues } from "../models/TicketFormValues";
import agent from "../api/axios";
import { store } from "./store";
import { PatchDoc } from "../models/Requests/PatchDoc";

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
            runInAction(() => { store.projectStore.selectedProject?.tickets.push(ticket) });
            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    updateTicket = async (id: string, fieldName: keyof Ticket, value: any) => {
        const patchDocument: PatchDoc[] = [{ op: "replace", path: `/${fieldName}`, value: value }]
        var updatedTicket = await agent.Tickets.updateTicket(id, patchDocument);

        runInAction(() => {
            if (store.projectStore.selectedProject) {
                const index = store.projectStore.selectedProject.tickets.findIndex(ticket => ticket.id === updatedTicket.id);
                if (index !== -1) {
                    store.projectStore.selectedProject.tickets[index] = updatedTicket;
                }
            }
        });
    }

    deleteTicket = async (ticketId: string) => {
        this.setIsLoading(true);

        try {
            agent.Tickets.deleteTicket(ticketId);
            runInAction(() => {
                if (store.projectStore.selectedProject) {
                    store.projectStore.selectedProject.tickets = store.projectStore.selectedProject.tickets.filter(ticket => ticket.id !== ticketId);
                }
            });
      
        } catch (error) {
            console.log(error);
        }
    }

    setIsLoading = (value: boolean) => {
        this.isLoading = value;
    }
}