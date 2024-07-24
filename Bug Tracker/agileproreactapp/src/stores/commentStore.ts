import { makeAutoObservable, runInAction } from "mobx";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr"
import { store } from "./store";
import { Comment } from "../models/Comment";

export default class CommentStore {
    comments: Comment[] = [];
    hubConnection: HubConnection | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    createHubConnection = (ticketId: string) => {
        if (store.ticketStore.selectedTicket) {
            this.hubConnection = new HubConnectionBuilder()
                .withUrl(import.meta.env.VITE_CHAT_URL + `?ticketId=${ticketId}`, {
                    accessTokenFactory: () => store.userStore.user?.authToken!
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();

            this.hubConnection.start().catch(error => console.log(`Error establishing chathub connection: ${error}`));

            this.hubConnection.on('LoadComments', (comments: Comment[]) => {
                runInAction(() => {
                    comments.forEach(comment => {
                        comment.dateSubmitted = new Date(comment.dateSubmitted + 'Z');
                    })
                    this.comments = comments;
                });
            })

            this.hubConnection.on('ReceiveComment', (comment: Comment) => {
                runInAction(() => {
                    comment.dateSubmitted = new Date(comment.dateSubmitted + 'Z');
                    this.comments.unshift(comment);
                });
            })
        }
    }

    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => console.log(`Error stopping chathub connection: ${error}`));
    }

    addComment = async (values: any) => {
        values.ticketId = store.ticketStore.selectedTicket?.id;

        try {
            await this.hubConnection?.invoke("SendComment", values);
        } catch (error) {
            console.log(error);
        }
    }

    clearComments = () => {
        this.comments = [];
        this.stopHubConnection();
    }
}