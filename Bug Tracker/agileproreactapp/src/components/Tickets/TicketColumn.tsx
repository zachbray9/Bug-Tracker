import { Button, Card, CardBody, CardHeader, Heading, Stack, Textarea, useOutsideClick } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import { useRef, useState } from "react";
import TicketCard from "./TicketCard";

interface Props {
    Title: string
}

export default observer(function TicketColumn({ Title }: Props) {
    const { projectStore, ticketStore } = useStore();
    const [isCreatingTask, setIsCreatingTask] = useState(false);
    const [newTaskTitle, setNewTaskTitle] = useState("");
    const textAreaRef = useRef<HTMLTextAreaElement>(null);

    const handleSubmit = () => {
        if (newTaskTitle.trim() !== "") {
            ticketStore.createTicket({
                title: newTaskTitle,
                status: Title,
                priority: "low",
                projectId: projectStore.selectedProject!.id
            })
            setNewTaskTitle("");
            setIsCreatingTask(false);
        }
    }

    const handleCancel = () => {
        setNewTaskTitle("");
        setIsCreatingTask(false);
    }

    useOutsideClick ({
        ref: textAreaRef,
        handler: handleCancel
    })

    return (
        <Card minW={{ base: 'xs', md: 'sm' }} maxW={{ base: 'xs', md: 'sm' }} variant="filled" alignSelf='stretch'>
            <CardHeader>
                <Heading fontSize={{base: 'md', md: 'lg'} }>{Title}</Heading>
            </CardHeader>

            <CardBody>
                <Stack>
                    {projectStore.selectedProject?.tickets
                        .filter(ticket => ticket.status === Title)
                        .map((ticket) => (
                            <TicketCard key={ticket.title} ticket={ticket} />
                     ))}

                    {/*Create Task Button*/}
                    {isCreatingTask ? (
                        <Textarea
                            ref={textAreaRef}
                            onChange={(e) => setNewTaskTitle(e.target.value)}
                            onKeyDown={(e) => {
                                if (e.key === "Enter" && !e.shiftKey) {
                                    e.preventDefault();
                                    handleSubmit()
                                }
                            } }
                            placeholder="What needs to be done?"
                            autoFocus />
                    ) : (
                            <Button onClick={() => setIsCreatingTask(true)} variant="ghost" width="100%" justifyContent="start" _hover={{ bg: "gray.200" }} _active={{bg: "gray.300"} }>+ Add task</Button>
                    )}
                </Stack>
            </CardBody>

        </Card>
    )
})