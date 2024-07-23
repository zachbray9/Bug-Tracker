import { Box, Button, Flex, Grid, GridItem, Heading, Image, Stack, Text } from "@chakra-ui/react";
import HeroImage from "../assets/LandingPageHeroImage.png";

export default function Home() {
    return (
        <Flex width="100%" justify="center" align="start">
            <Stack gap={8} align="center" padding="4rem" maxWidth="1200px" width="100%">
                <Grid templateColumns={{ base: "1fr", md: "repeat(2, 1fr)" }} gap={6}>
                    <GridItem display="flex" alignItems="center" justifyContent="center">
                        <Stack gap={8}>
                            <Heading as="h2" size="3xl">Make your development process Agile.</Heading>
                            <Text color="text.subtle">
                                Discover AgilePro, the ultimate tool for seamless collaboration and efficient project management.
                                Customize workflows, track progress, and boost productivity with our intuitive and powerful platform.
                            </Text>
                            <Flex gap={4}>
                                <Button colorScheme="messenger">Get Started</Button>
                                <Button variant="outline">See how it works</Button>
                            </Flex>
                        </Stack>
                    </GridItem>

                    <GridItem display="flex" alignItems="center" justifyContent="center">
                        <Image src={HeroImage} alt="Hero image" objectFit="cover" boxSize={{base: "300px", md: "500px"} } />
                    </GridItem>
                </Grid>
            </Stack>

            <Stack align="center" padding="4rem" maxWidth="1200px" width="100%">
                <Text>Trusted by individuals and teams all over the world</Text>
                <Flex>

                </Flex>
            </Stack>
        </Flex>
    )
}