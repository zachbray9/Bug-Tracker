import { makeAutoObservable, runInAction } from "mobx"
import { Project } from "../models/Project";
import agent from "../api/axios";
import { ProjectFormValues } from "../models/ProjectFormValues";
import router from "../routes";
import { TicketFormValues } from "../models/TicketFormValues";

export default class ProjectStore {
    projects: Project[] = [];
    selectedProject: Project | null = null;
    isLoading = false;

    constructor(){
        makeAutoObservable(this);
    }

    loadProjects = async () => {
        this.setIsLoading(true);

        try {
            var projects = await agent.Projects.getCurrentUserProjects();
            runInAction(() => this.projects = projects);
            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    createProject = async (creds: ProjectFormValues) => {
        this.setIsLoading(true);

        try {
            var project = await agent.Projects.createProject(creds);
            console.log(project);
            runInAction(() => { this.projects.push(project) });
            this.setSelectedProject(project);
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
                if (this.selectedProject) {
                    const index = this.selectedProject.tickets.findIndex(ticket => ticket.id === updatedTicket.id);
                    if (index !== -1) {
                        this.selectedProject.tickets[index] = updatedTicket;
                    }
                }
            });

            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    setSelectedProject = (project: Project) => {
        this.selectedProject = project;
        router.navigate("projectBoard");
    }

    setIsLoading(state: boolean) {
        this.isLoading = state;
    }
}