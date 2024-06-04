import { Box, IconButton, Menu, MenuButton, MenuItem, MenuList, Skeleton, Table, TableContainer, Tbody, Td, Th, Thead, Tr } from "@chakra-ui/react";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import EmptyProjects from "../common/Empty/EmptyProjects";
import { FiMoreHorizontal } from "react-icons/fi";

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
                            <Th>More actions</Th>
                        </Tr>
                    </Thead>              
               
                    <Tbody>
                        {projectStore.isLoading ? (
                            Array.from({ length: 8 }).map((_, index) => (
                                <Tr key={index}>
                                    <Td><Skeleton height="20px" /></Td>
                                    <Td><Skeleton height="20px" /></Td>
                                </Tr>
                            ))
                        ) : (
                            projectStore.projects.length === 0 ? (
                                <Tr>
                                    <Td colSpan={2}>
                                        <Box>
                                            <EmptyProjects />
                                        </Box>
                                    </Td>
                                </Tr>
                            ) : (
                                projectStore.projects.map((project) => (
                                    <Tr key={project.id}>
                                        <Td>{project.name}</Td>
                                        <Td>
                                            <Menu>
                                                <MenuButton as={IconButton} icon={<FiMoreHorizontal />} aria-label="More options">More options</MenuButton>
                                                <MenuList>
                                                    <MenuItem>Project settings</MenuItem>
                                                    <MenuItem>Leave project</MenuItem>
                                                </MenuList>
                                            </Menu>
                                        </Td>
                                    </Tr>
                                ))
                            )
                        )}
                    </Tbody>
                </Table>
            </TableContainer>
    )
})