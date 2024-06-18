import { ProjectParticipant } from "./ProjectParticipant"

export interface Ticket {
    id: string,
    title: string,
    description: string | null,
    author: ProjectParticipant
    assignee: ProjectParticipant | null
    status: string,
    priority: string,
    dateSubmitted: Date
}