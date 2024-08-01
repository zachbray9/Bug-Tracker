import { Grid, Heading, Stack, Text } from "@chakra-ui/react";
import ColorModeSwitch from "../components/ColorModeSwitch";

export default function Preferences() {
    return (
        <Stack gap={16} align="start" justify="start" paddingLeft={{base: '1rem', md: '4rem'}} >
            <Heading size={{base: 'md', md: 'lg'}}>Preferences</Heading>

            <Stack>
                <Heading as="h5" size="sm">Display</Heading>

                <Grid templateColumns={{ base: '1fr', md: '1fr 2fr' }} gap={{base: 12, md: 16}}>
                    <Text>This is where you can change some visual settings for your account such as the color mode.</Text>

                    <Stack gap={4} width="fit-content" align="center">
                        <ColorModeSwitch />
                    </Stack>
                </Grid>
            </Stack>
        </Stack>
    )
}