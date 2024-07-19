import { Flex, Grid, Heading, Stack, Text } from "@chakra-ui/react";
import ColorModeSwitch from "../ColorModeSwitch";

export default function Preferences() {
    return (
        <Stack gap={16} align="start" justify="start" paddingX="4rem">
            <Heading as="h3" size="lg">Preferences</Heading>

            <Stack>
                <Heading as="h5" size="sm">Display</Heading>

                <Grid templateColumns="1fr 2fr" gap={16}>
                    <Text>This is where you can change some visual settings for your account such as the color mode.</Text>

                    <Stack gap={4} width="fit-content" align="center">
                        <ColorModeSwitch />
                    </Stack>
                </Grid>
            </Stack>
        </Stack>
    )
}