import { Avatar, Box, Button, Card, CardBody, CardFooter, Flex, IconButton, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
import { FiMoreHorizontal } from "react-icons/fi";
import { Ticket } from "../../models/Ticket";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";

interface Props {
    ticket: Ticket
}

export default observer(function TicketCard({ ticket }: Props) {
    const { ticketStore } = useStore();

    const priorityColorMap = (priority: string) => {
        switch (priority) {
            case "low":
                return "#57D9A3";
            case "medium":
                return "#ffed29";
            case "high":
                return "#ed4337";
            default:
                return "gray";
        }
    }

    const handlePreventModalOpen = (event: React.MouseEvent) => {
        event.stopPropagation();
    }

    return (
        <Card cursor="pointer" onClick={() => { ticketStore.setSelectedTicket(ticket); }}>
            <CardBody>
                <Flex width="100%" justifyContent="space-between" alignItems="start" gap={2}>
                    <Button variant="link" whiteSpace="normal" wordBreak="break-word" textAlign="left">{ticket.title}</Button>
  
                    <Menu>
                        <MenuButton as={IconButton} aria-label="options" onClick={handlePreventModalOpen} icon={<FiMoreHorizontal fontSize="24px" />} />
                        <MenuList>
                            <MenuItem onClick={(e) => { handlePreventModalOpen(e); ticketStore.deleteTicket(ticket.id) }}>Delete</MenuItem>
                        </MenuList>
                    </Menu>
                </Flex>
            </CardBody>
            <CardFooter>
                <Flex justifyContent="space-between" alignItems="center" width="100%">
                    <Box bg={priorityColorMap(ticket.priority)} borderLeftRadius="full" borderRightRadius="full" padding="1px 8px" >
                        <Text fontSize="xs" color="whitesmoke">{ticket.priority}</Text>
                    </Box>
                    <Avatar name={ticket.assignee ? `${ticket.assignee.firstName} ${ticket.assignee.lastName}` : undefined} src={ticket.assignee?.profilePictureUrl ? ticket.assignee.profilePictureUrl : undefined} size="sm" />
                </Flex>
            </CardFooter>
        </Card>
    )
})