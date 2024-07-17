import { Avatar, Box, Button, Flex, Image, Menu, MenuButton, MenuItem, MenuList, Spacer, Text, useDisclosure } from "@chakra-ui/react";
import { ChevronDownIcon } from "@chakra-ui/icons";
import { NavLink } from "react-router-dom";
import AgileProLogo from "../assets/AgileProLogoCropped.png";
import "../styles/navbar.css";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import router from "../routes";
import CreateProjectModal from "./Projects/CreateProjectModal";



export default observer(function NavBar() { 
    const { userStore } = useStore();
    const { isOpen, onOpen, onClose } = useDisclosure();

    return (
        <Box borderWidth="0px 0px 1px 0px" padding={1}>
            {userStore.user ?  (
                <Flex align="center" gap={4} paddingX={4}>
                    <NavLink to='' className='logo-link'>
                        <Flex justify='center' align='center' gap={1} >
                            <Image src={AgileProLogo} alt='Logo' boxSize='45px' objectFit='contain'></Image>
                            <Text fontSize='xl' fontWeight='500' marginBottom='0px'>AgilePro</Text>
                        </Flex>
                    </NavLink>
                    <Menu>
                        <MenuButton as={Button} variant="ghost" rightIcon={<ChevronDownIcon />}>Projects</MenuButton>

                        <MenuList>
                            <MenuItem onClick={() => router.navigate('dashboard')}>View all projects</MenuItem>
                            <MenuItem onClick={onOpen}>Create project</MenuItem>
                            <CreateProjectModal isOpen={isOpen} onClose={onClose} />
                        </MenuList>
                    </Menu>
                    <Spacer />
                    <Menu>
                        <MenuButton as={Button} variant="ghost" borderRadius="full" padding="0px">
                            <Avatar name={userStore.user.fullName} src={userStore.user.profilePictureUrl} size="sm"></Avatar>
                        </MenuButton >

                        <MenuList>
                            <MenuItem onClick={() => router.navigate('profile')}>Manage account</MenuItem>
                            <MenuItem onClick={() => userStore.logout()}>Logout</MenuItem>
                        </MenuList>
                    </Menu>
                </Flex>
            ) : (
                <Flex align="center" gap={4} paddingX={4}>
                    <NavLink to='' className='logo-link'>
                            <Flex justify='center' align='center' gap={1}>
                            <Image src={AgileProLogo} alt='Logo' boxSize='45px' objectFit='contain'></Image>
                            <Text fontSize='xl' fontWeight='500' marginBottom='0px'>AgilePro</Text>
                        </Flex>
                    </NavLink>
                    <Spacer />
                    <NavLink to='/Signup'>
                        <Button colorScheme='messenger' variant='outline'>Sign Up</Button>
                    </NavLink>
                    <NavLink to='/Login'>
                        <Button colorScheme='messenger'>Login</Button>
                    </NavLink>
                </Flex>
            )}
        </Box>
    ) 
});