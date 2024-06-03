import { Skeleton, Table, TableContainer, Tbody, Td, Th, Thead, Tr } from "@chakra-ui/react";
import { Project } from "../../models/Project";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import EmptyProjects from "../common/Empty/EmptyProjects";

export default observer(function ProjectsTable() {
    const { projectStore } = useStore();

    useEffect(() => {
        projectStore.loadProjects();
    }, [projectStore])

    if (projectStore.isLoading) {
        return (
            <TableContainer>
                <Table>
                    <Thead>
                        <Tr>
                            <Th>Title</Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        {Array.from({ length: 10 }).map((_, index) => (
                            <Tr key={index}>
                                <Td>
                                    <Skeleton height="20px" />
                                </Td>
                            </Tr>
                        ))}
                    </Tbody>
                </Table>
            </TableContainer>
        )
    }

    if (projectStore.projects.length === 0) {
        return (
            <EmptyProjects />
        )
    }

    return (
        <TableContainer>
            <Table>
                <Thead>
                    <Tr>
                        <Th>Title</Th>
                    </Tr>
                </Thead>
                <Tbody>
                    {projectStore.projects.map((project: Project) => (
                        <Tr key={project.id}>
                            <Td>{project.name}</Td>
                        </Tr>
                    ))}
                </Tbody>
            </Table>
        </TableContainer>
    )
})