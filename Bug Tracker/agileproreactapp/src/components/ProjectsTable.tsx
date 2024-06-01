import { Skeleton, Table, TableContainer, Tbody, Td, Th, Thead, Tr } from "@chakra-ui/react";
import { Project } from "../models/Project";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";

export default observer(function ProjectsTable() {
    const { projectStore } = useStore();

    useEffect(() => {
        projectStore.loadProjects();
    }, [projectStore])

    return (
        <TableContainer>
            <Table>
                <Thead>
                    <Tr>
                        <Th>Title</Th>
                    </Tr>
                </Thead>
                <Tbody>
                    {projectStore.isLoading ? (
                        Array.from({ length: 5 }).map((_, index) => (
                            <Tr key={index}>
                                <Td>
                                    <Skeleton height="20px" />
                                </Td>
                            </Tr>
                        ))
                    ) : (
                            projectStore.projects.map((project: Project) => (
                                <Tr>
                                    <Td>{project.title}</Td>
                                </Tr>
                            ))
                    )}
                </Tbody>
            </Table>
        </TableContainer>

    )
})