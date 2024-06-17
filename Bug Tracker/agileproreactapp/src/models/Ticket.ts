interface Ticket {
    id: string,
    title: string,
    description: string | null,
    authorId: string,
    authorFirstName: string,
    authorLastName: string,
    assigneeId: string | null,
    assigneeFirstName: string | null,
    assigneeLastName: string | null,
    status: string,
    priority: string,
    dateSubmitted: Date
}