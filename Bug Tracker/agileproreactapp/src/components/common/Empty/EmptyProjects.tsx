import { Center, Container, Image, Text } from "@chakra-ui/react";

export default function EmptyProjects() {
    return (
        <Center>
            <Image />
            <Container>
                <Text>It looks like you don't have any projects yet </Text>
            </Container>
        </Center>
    )
}