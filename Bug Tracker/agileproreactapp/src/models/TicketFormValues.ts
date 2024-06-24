import { ProjectParticipant } from "./ProjectParticipant"

export interface TicketFormValues {
    id?: string
    title: string
    description?: string
    status: string
    priority: string
    assignee?: ProjectParticipant | null
    author?: ProjectParticipant
    projectId: string
}