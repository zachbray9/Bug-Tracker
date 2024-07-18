export interface User {
    id: string,
    email: string,
    firstName: string,
    lastName: string,
    fullName: string,
    initials: string,
    profilePictureUrl:string,
    dateJoined: Date,
    authToken: string
}