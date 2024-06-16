import { Box, Button, Flex, Spacer, Text, useDisclosure} from "@chakra-ui/react";
import ProjectsTable from "../components/Projects/ProjectsTable";
import CreateProjectModal from "../components/Projects/CreateProjectModal";

export default function Dashboard() {
    const { isOpen, onOpen, onClose } = useDisclosure();

    return (
        <Box height="100vh" paddingX={8} paddingY={4}>
            <Flex>
                <Text fontSize="3xl">Projects</Text>
                <Spacer />
                <Button colorScheme="messenger" onClick={onOpen}>Create project</Button>
                <CreateProjectModal isOpen={isOpen} onClose={onClose} />
            </Flex>

            <ProjectsTable />
        </Box>
    )
}