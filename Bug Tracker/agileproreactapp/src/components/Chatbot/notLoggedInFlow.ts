import { Flow, Params } from "react-chatbotify"

const helpOptions = ["What is AgilePro?", "Is it free?", "How does it work?", "Github"]

export const NotLoggedInFlow: Flow = {
    start: {
        message: 'Hello, I am AgileBot. Welcome to AgilePro! Is there anything I can help you with?',
        options: helpOptions,
        path: "process_options"
    },
    prompt_again: {
        message: 'Is there anything else I can help you with?',
        options: helpOptions,
        path: 'process_options'
    },
    process_options: {
        transition: { duration: 0 },
        chatDisabled: true,
        path: async (params: Params) => {
            let link: string = ''
            switch (params.userInput) {
                case 'What is AgilePro?':
                    return 'agilepro_about'
                case 'Github':
                    link = 'https://github.com/zachbray9/Bug-Tracker'
                    break
                case 'How does it work?':
                    link = 'https://youtu.be/nnRKnhd6yys'
                    await params.injectMessage("There's a great demo video on youtube that shows AgilePro in action.")
                    break
                case 'Is it free?':
                    return 'agilepro_free'
                default:
                    return 'unknown_input'
            }

            await params.injectMessage("Sit tight! I'll send you right there!");
            setTimeout(() => {
                window.open(link);
            }, 3000)
            return "repeat"
        }
    },
    agilepro_about: {
        message: 'AgilePro is a project management tool designed to help teams collaborate, track progress, and manage tasks efficiently. It supports Agile methodologies, enabling users to break down projects into manageable tasks, set priorities, and monitor progress in real-time. With features like task boards, customizable workflows, and reporting, AgilePro makes it easy to stay organized and deliver projects on time.',
        transition: { duration: 0 },
        path: 'repeat'
    },
    agilepro_free: {
        message: "Yes, AgilePro is completely free to use! You can enjoy all its features without any cost. We believe in providing a valuable tool for project management without any barriers.",
        transition: { duration: 0 },
        path: 'repeat'
    },
    unknown_input: {
        message: 'Sorry, I do not understand your message! If you require further assistance, you may click on ' +
            'the Github option and open an issue there',
        options: helpOptions,
        path: "process_options"
    },
    repeat: {
        transition: { duration: 3000 },
        path: "prompt_again"
    }
}