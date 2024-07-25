import { makeAutoObservable, runInAction } from "mobx"
import { Project } from "../models/Project";
import agent from "../api/axios";
import { ProjectFormValues } from "../models/ProjectFormValues";
import router from "../routes";
import { AddUserFormValues } from "../models/Requests/AddUserFormValues";
import { store } from "./store";

export default class ProjectStore {
    projects: Project[] = [];
    selectedProject: Project | null = null;
    isLoading = false; 
    isAdmin = false;

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

    setIsAdmin = () => {
        const user = this.selectedProject?.users.find(u => u.email === store.userStore.user?.email);
        if (user && (user.role === "Owner" || user.role === "Administrator")) {
            this.isAdmin = true;
        }
        else {
            this.isAdmin = false;
        }
    }

    createProject = async (creds: ProjectFormValues) => {
        this.setIsLoading(true);

        try {
            var project = await agent.Projects.createProject(creds);
            runInAction(() => { this.projects.push(project) });
            this.setSelectedProject(project);
            this.setIsLoading(false);
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    addUser = async (creds: AddUserFormValues) => {
        var user = await agent.Projects.addUser(creds);
        runInAction(() => { this.selectedProject!.users.push(user) })
    }

    setSelectedProject = (project: Project) => {
        this.selectedProject = project;
        this.setIsAdmin();
        router.navigate("projectBoard");
    }

    setIsLoading(state: boolean) {
        this.isLoading = state;
    }
}