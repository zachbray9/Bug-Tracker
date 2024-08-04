import { Flex, Heading, Tab, TabList, TabPanel, TabPanels, Tabs } from "@chakra-ui/react";
import Profile from "./Profile";
import Preferences from "./Preferences";
import { Helmet } from "react-helmet-async";

export default function AccountSettings() {
    return (
        <>
            <Helmet>
                <title>Account settings - AgilePro</title>
            </Helmet>

            <Flex flexDir="column" padding={{base: '1rem', md: '4rem'} } gap={8}>
                <Heading size={{ base: 'md', md: 'lg' }}>Settings</Heading>
                <Tabs orientation="vertical" variant="solid-rounded" size={{ base: 'sm', md: 'md' }} colorScheme='messenger'>
                    <TabList>
                        <Tab>Account</Tab>
                        <Tab>Preferences</Tab>
                    </TabList>

                    <TabPanels>
                        <TabPanel>
                            <Profile />
                        </TabPanel>

                        <TabPanel>
                            <Preferences />
                        </TabPanel>
                    </TabPanels>
                </Tabs>
            </Flex>
        </>
    )
}