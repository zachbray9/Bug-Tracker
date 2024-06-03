import { Image, Stack, Text} from "@chakra-ui/react";
import BluePaint from "../../../assets/BluePaint.png";

export default function EmptyProjects() {
    return (
        <Stack justify="center" align="center">
            <Image title="No projects" src={BluePaint} boxSize={190} objectFit="contain" />
            <Text fontSize="x-large">It looks like you don't have any projects yet </Text>
        </Stack>
    )
}