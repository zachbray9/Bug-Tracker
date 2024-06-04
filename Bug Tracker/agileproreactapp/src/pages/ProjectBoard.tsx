import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Box, Text } from "@chakra-ui/react";
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
            <Text>{projectStore.selectedProject?.name}</Text>
        </Box>
    )
})