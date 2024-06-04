import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Text } from "@chakra-ui/react";

export default observer(function ProjectBoard() {
    const { projectStore } = useStore();

    return (
        <>
            <Text>{projectStore.selectedProject?.name}</Text>
        </>
    )
})