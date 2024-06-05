import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Avatar, Box, Button, HStack, Text } from "@chakra-ui/react";
import EmptyProjects from "../components/common/Empty/EmptyProjects";

export default observer(function ProjectBoard() {
    const { projectStore } = useStore();

    if (projectStore.selectedProject === null) {
        return (
            <EmptyProjects />
        )
    }

    return (
        <Box>
            <HStack gap={2}>
                {projectStore.selectedProject.users.map((user => (
                    <Button key={user.email} variant="ghost" borderRadius="full" padding={0}>
                        <Avatar name={user.firstName + " " + user.lastName} size="sm"/>
                    </Button>
                )))}
            </HStack>
        </Box>
    )
})