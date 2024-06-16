import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Avatar, Button, HStack, Heading, IconButton, Stack } from "@chakra-ui/react";
import EmptyProjects from "../components/common/Empty/EmptyProjects";
import TicketColumn from "../components/Tickets/TicketColumn";
import { IoPersonAdd } from "react-icons/io5";

export default observer(function ProjectBoard() {
    const { projectStore } = useStore();

    if (projectStore.selectedProject === null) {
        return (
            <EmptyProjects />
        )
    }

    return (
        <Stack padding={8} gap={8} minH="100vh">
            <Heading>Task board</Heading>

            <HStack gap={2}>
                {projectStore.selectedProject.users.map((user => (
                    <Button key={user.email} variant="ghost" borderRadius="full" padding={0}>
                        <Avatar name={user.firstName + " " + user.lastName} size="sm"/>
                    </Button>
                )))}

                <IconButton aria-label="add user" icon={<IoPersonAdd />} isRound={true} />
            </HStack>

            <HStack gap={4}>
                <TicketColumn Title="To do" />
                <TicketColumn Title="In progress" />
                <TicketColumn Title="Done" />
            </HStack>
        </Stack>
    )
})