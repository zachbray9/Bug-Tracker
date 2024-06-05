import { ProjectParticipant } from "./ProjectParticipant";

export interface Project {
    id: string,
    name: string,
    description: string,
    dateStarted: Date,
    users: ProjectParticipant[]
}