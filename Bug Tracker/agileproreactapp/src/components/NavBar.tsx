import { Button, Flex, Image, Spacer, Text } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import AgileProLogo from "../assets/AgileProLogoCropped.png";
import "../styles/navbar.css";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";



export default observer(function NavBar() { 
    const { userStore } = useStore();

    return (
        <Flex align="center" gap={4} paddingX={4}>
            <NavLink to='' className='logo-link'>
                <Flex justify='center' align='center' gap='10px'>
                    <Image src={AgileProLogo} alt='Logo' boxSize='60px' objectFit='contain'></Image>
                    <Text fontSize='2xl' fontWeight='500' marginBottom='0px'>AgilePro</Text>
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
    )
});