import { Flex, Heading, Tab, TabList, TabPanel, TabPanels, Tabs } from "@chakra-ui/react";
import Profile from "./Profile";
import Preferences from "../components/Account/Preferences";

export default function AccountSettings() {
    return (
        <Flex flexDir="column" padding="4rem" gap={8}>
            <Heading as="h3" size="lg">Settings</Heading>
            <Tabs orientation="vertical" variant="solid-rounded">
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
    )
}