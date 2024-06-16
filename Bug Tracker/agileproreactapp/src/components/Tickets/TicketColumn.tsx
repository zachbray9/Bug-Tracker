import { Button, Card, CardBody, CardFooter, CardHeader, Heading, Stack, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";

interface Props {
    Title: string
}

export default observer(function TicketColumn({ Title }: Props) {
    const { projectStore } = useStore();

    return (
        <Card width="sm" variant="filled">
            <CardHeader>
                <Heading size="sm">{ Title }</Heading>
            </CardHeader>

            <CardBody>
                <Stack>
                    {projectStore.selectedProject?.tickets.map((ticket) => (
                        <Text width="100%">{ticket.title}</Text>
                    ))}
                </Stack>
            </CardBody>

            <CardFooter>
                <Button width="100%" justifyContent="start">+ Add task</Button>
            </CardFooter>
        </Card>
    )
})