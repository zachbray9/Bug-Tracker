import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Avatar, Button, HStack, Heading, IconButton, Stack, useDisclosure } from "@chakra-ui/react";
import EmptyProjects from "../components/common/Empty/EmptyProjects";
import TicketColumn from "../components/Tickets/TicketColumn";
import { IoPersonAddSharp } from "react-icons/io5";
import TicketDetailsModal from "../components/Tickets/TicketDetailsModal";
import AddUserModal from "../components/Projects/AddUserModal";

export default observer(function ProjectBoard() {
    const { projectStore, ticketStore } = useStore();
    const { isOpen, onOpen, onClose } = useDisclosure();

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
                        <Avatar name={`${user.firstName} ${user.lastName}`} src={user.profilePictureUrl || undefined} key={`${user.firstName} ${user.lastName}`} size="sm" />
                    </Button>
                )))}

                {projectStore.isAdmin && (
                    <IconButton aria-label="add user" icon={<IoPersonAddSharp />} isRound={true} onClick={() => onOpen()} />
                )}
            </HStack>

            <HStack gap={4} alignItems="start">
                <TicketColumn Title="To do" />
                <TicketColumn Title="In progress" />
                <TicketColumn Title="Done" />
            </HStack>

            {ticketStore.selectedTicket && <TicketDetailsModal isOpen={ticketStore.isModalOpen} onClose={ticketStore.clearSelectedTicket} />}
            <AddUserModal isOpen={isOpen} onClose={onClose} />
        </Stack>
    )
})