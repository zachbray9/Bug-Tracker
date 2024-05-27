import { Center, Spinner, Text } from "@chakra-ui/react";

interface Props {
    text: string
}

export default function LoadingComponent(props: Props) {
    return (
        <Center height="100vh" gap={4}>
            <Spinner size="lg" />
            <Text>{props.text}</Text>
        </Center>
    )
}