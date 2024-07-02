import { ProjectParticipant } from "./ProjectParticipant";

export interface Comment {
    id: string
    text: string,
    author: ProjectParticipant,
    dateSubmitted: Date,
}