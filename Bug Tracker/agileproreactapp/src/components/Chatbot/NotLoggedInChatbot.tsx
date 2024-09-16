import Chatbot from "react-chatbotify"
import { NotLoggedInFlow } from "./notLoggedInFlow"
import { ChatbotStyles } from "../../styles/chatbotStyles"
import { ChatbotSettings } from "./chatbotSettings"

export default function NotLoggedInChatbot() {
    return (
        <Chatbot flow={NotLoggedInFlow} styles={ChatbotStyles} settings={ChatbotSettings} />
    )
}