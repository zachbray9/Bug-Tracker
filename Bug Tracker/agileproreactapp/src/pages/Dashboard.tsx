import { Button, Flex, Spacer, Stack, Text, useDisclosure} from "@chakra-ui/react";
import ProjectsTable from "../components/Projects/ProjectsTable";
import CreateProjectModal from "../components/Projects/CreateProjectModal";

export default function Dashboard() {
    const { isOpen, onOpen, onClose } = useDisclosure();

    return (
        <Stack paddingX={8} paddingY={4} gap={4}>
            <Flex>
                <Text fontSize={{base: 'xl', md: '3xl'}} >Projects</Text>
                <Spacer />
                <Button colorScheme="messenger" onClick={onOpen} size={{base: 'sm', md: 'md'}}>Create project</Button>
                <CreateProjectModal isOpen={isOpen} onClose={onClose} />
            </Flex>

            <ProjectsTable />
        </Stack>
    )
}