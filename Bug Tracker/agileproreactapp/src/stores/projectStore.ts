import { makeAutoObservable, runInAction } from "mobx"
import { Project } from "../models/Project";
import agent from "../api/axios";
import { ProjectFormValues } from "../models/ProjectFormValues";

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
            runInAction(() => {
                this.projects.push(project);
                this.selectedProject = project;
            });
            
            this.setIsLoading(false);
            //navigate to that projects task board
        } catch (error) {
            console.log(error);
            this.setIsLoading(false);
        }
    }

    setIsLoading(state: boolean) {
        this.isLoading = state;
    }
}