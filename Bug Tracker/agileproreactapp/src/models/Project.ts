import { ProjectParticipant } from "./ProjectParticipant";
import { Ticket } from "./Ticket";

export interface Project {
    id: string,
    name: string,
    description: string,
    dateStarted: Date,
    users: ProjectParticipant[],
    tickets: Ticket[]
}