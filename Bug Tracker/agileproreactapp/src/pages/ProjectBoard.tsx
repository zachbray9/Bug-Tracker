import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Avatar, AvatarGroup, Box, HStack, Heading, IconButton, Input, InputGroup, InputLeftElement, Stack, useBreakpointValue, useDisclosure } from "@chakra-ui/react";
import EmptyProjects from "../components/common/Empty/EmptyProjects";
import TicketColumn from "../components/Tickets/TicketColumn";
import { IoPersonAddSharp } from "react-icons/io5";
import TicketDetailsModal from "../components/Tickets/TicketDetailsModal";
import AddUserModal from "../components/Projects/AddUserModal";
import { Helmet } from "react-helmet-async";
import { useEffect } from "react";
import { SearchIcon } from "@chakra-ui/icons";

export default observer(function ProjectBoard() {
    const { projectStore, ticketStore } = useStore();
    const { isOpen, onOpen, onClose } = useDisclosure();

    useEffect(() => {
        return (
            ticketStore.clearFilterQuery()
        )
    }, [ticketStore]);

    const maxVisibleAvatars = useBreakpointValue({ base: 2, md: 5, lg: 8 });

    if (projectStore.selectedProject === null) {
        return (
            <EmptyProjects />
        )
    }

    return (
        <>
            <Helmet>
                <title>{projectStore.selectedProject.name} - Project board - AgilePro</title>
            </Helmet>

            <Stack padding={8} gap={8}>
                <Heading fontSize={{ base: 'xl', md: '3xl' }}>Task board</Heading>

                <HStack gap={2}>
                    <InputGroup width='fit-content' size={{ base: 'sm', md: 'md' }}>
                        <InputLeftElement>
                            <SearchIcon color='text.subtle' />
                        </InputLeftElement>

                        <Input placeholder='Search' onChange={(e) => ticketStore.setFilterQuery(e.target.value)} />
                    </InputGroup>

                    <AvatarGroup size='sm' max={maxVisibleAvatars}>
                        {projectStore.selectedProject.users.map((user => (
                            <Avatar name={`${user.firstName} ${user.lastName}`} src={user.profilePictureUrl || undefined} key={`${user.firstName} ${user.lastName}`} />
                        )))}
                    </AvatarGroup>

                    {projectStore.isAdmin && (
                        <IconButton aria-label="add user" icon={<IoPersonAddSharp />} isRound={true} onClick={() => onOpen()} />
                    )}
                </HStack>

                <Box overflowX='auto' whiteSpace='nowrap'>
                    <HStack gap={4} alignItems="start">
                        <TicketColumn Title="To do" />
                        <TicketColumn Title="In progress" />
                        <TicketColumn Title="Done" />
                    </HStack>
                </Box>

                {ticketStore.selectedTicket && <TicketDetailsModal isOpen={ticketStore.isModalOpen} onClose={ticketStore.clearSelectedTicket} />}
                <AddUserModal isOpen={isOpen} onClose={onClose} />
            </Stack>
        </>
    )
})