import { Button, Flex, Grid, GridItem, Heading, Image, Stack, Text } from "@chakra-ui/react";
import HeroImage from "../assets/LandingPageHeroImage.png";
import SecurityImage from "../assets/LandingPageSecurityImage.png";
import MicrosoftLogo from "../assets/MicrosoftLogo.png";
import MetaLogo from "../assets/MetaLogo.png";
import AppleLogo from "../assets/AppleLogo.png";
import NvidiaLogo from "../assets/NvidiaLogo.png";
import TeslaLogo from "../assets/TeslaLogo.png";
import Footer from "../components/Footer";
import { useStore } from "../stores/store";
import router from "../routes";

export default function Home() {
    const { userStore } = useStore();

    if (userStore.user) {
        router.navigate('dashboard');
    }

    return (
        <Stack width="100%" justify="start" align="center" gap={16} >
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
                        <Image src={HeroImage} alt="Hero image" objectFit="cover" boxSize={{ base: "300px", md: "500px" }} />
                    </GridItem>
                </Grid>
            </Stack>

            <Stack align="center" padding="4rem" maxWidth="1200px" width="100%">
                <Text color="text.subtle">Trusted by individuals and teams all over the world</Text>
                <Flex align="center" wrap="wrap" gap={8}>
                    <Image src={MicrosoftLogo} maxWidth="150px" maxHeight="70px" objectFit="contain" />
                    <Image src={MetaLogo} maxWidth="150px" maxHeight="70px" objectFit="contain" />
                    <Image src={AppleLogo} maxWidth="150px" maxHeight="70px" objectFit="contain" />
                    <Image src={NvidiaLogo} maxWidth="150px" maxHeight="70px" objectFit="contain" />
                    <Image src={TeslaLogo} maxWidth="150px" maxHeight="70px" objectFit="contain" />
                </Flex>
                <Text color="text.subtle" fontSize="xs">*Logos used are for demonstration purposes only and do not imply endorsement
                    by the respective companies.
                </Text>
            </Stack>

            <Flex justify="center" padding="4rem" maxWidth="1200px" width="100%">
                <Grid templateColumns={{ base: "1fr", md: "repeat(2, 1fr)" }} gap={6}>
                    <GridItem display="flex" justifyContent="center" alignItems="center">
                        <Image src={SecurityImage} alt="Security image" objectFit="cover" boxSize={{base: "300px", md: "500px"} } />
                    </GridItem>

                    <GridItem display="flex" justifyContent="center" alignItems="center">
                        <Stack gap={8}>
                            <Heading as="h3" size="lg">All your work is safe with us</Heading>
                            <Text color="text.subtle">
                                We take your data security seriously, which is why we use advanced authorization and authentication protocols to protect
                                your files in the cloud. We scored an 'A' rating for security from a well known security scanning site. Your data is safe and
                                secure with us.
                            </Text>
                            <Flex gap={4}>
                                <Button variant="outline">Try now</Button>
                            </Flex>
                        </Stack>
                    </GridItem>
                </Grid>
            </Flex>

            <Footer />
        </Stack>
    )
}