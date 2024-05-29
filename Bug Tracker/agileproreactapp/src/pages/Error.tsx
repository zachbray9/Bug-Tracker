import { Center, Code, Text } from "@chakra-ui/react";

interface Props {
    errorCode: string,
    errorMessage: string
}

export default function Error(props: Props) {
    return (
        <Center>
            <Text>{props.errorCode}</Text>
            <Code>{props.errorMessage}</Code>
        </Center>
    )
}