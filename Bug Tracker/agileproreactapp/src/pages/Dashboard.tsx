import { Box, Text} from "@chakra-ui/react";
import ProjectsTable from "../components/ProjectsTable";

export default function Dashboard() {
    return (
        <Box height="100vh" paddingX={8} paddingY={4}>
            <Text fontSize="3xl">Projects</Text>

            <ProjectsTable />

        </Box>
    )
}