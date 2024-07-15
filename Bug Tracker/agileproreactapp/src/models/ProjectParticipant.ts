export interface ProjectParticipant {
    userId: string,
    projectId: string,
    email: string,
    firstName: string,
    lastName: string,
    profilePictureUrl: string | null,
    role: string
}