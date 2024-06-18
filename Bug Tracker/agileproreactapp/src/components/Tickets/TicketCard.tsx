import { Avatar, Box, Button, Card, CardBody, CardFooter, Flex, IconButton, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
import { FiMoreHorizontal } from "react-icons/fi";
import { Ticket } from "../../models/Ticket";

interface Props {
    ticket: Ticket
}

export default function TicketCard({ ticket }: Props) {
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

    return (
        <Card cursor="pointer">
            <CardBody>
                <Flex width="100%" justifyContent="space-between" alignItems="start">
                    <Button variant="link">{ticket.title}</Button>
                    <Menu>
                        <MenuButton as={IconButton} aria-label="options" icon={<FiMoreHorizontal fontSize="24px" />} />
                        <MenuList>
                            <MenuItem>Delete</MenuItem>
                        </MenuList>
                    </Menu>
                </Flex>
            </CardBody>
            <CardFooter>
                <Flex justifyContent="space-between" alignItems="center" width="100%">
                    <Box bg={priorityColorMap(ticket.priority)} borderLeftRadius="full" borderRightRadius="full" padding="1px 8px" >
                        <Text fontSize="xs" color="whitesmoke">{ticket.priority}</Text>
                    </Box>
                    <Avatar name={ticket.assignee ? ticket.assignee.firstName + " " + ticket.assignee.lastName : ""} src={ticket.assignee ? ticket.assignee.profilePictureUrl : ""} size="sm" />
                </Flex>
            </CardFooter>
        </Card>
    )
}