import { Button, Flex, HStack, Image, Text } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import AgileProLogo from "../assets/AgileProLogoCropped.png";
import "../styles/navbar.css";



const NavBar = () => { 
    return (
        <div className='main-wrapper'>
            <HStack className='content-wrapper' justifyContent='space-between' padding=' 0px 40px'>
                <NavLink to='' className='logo-link'>
                    <Flex justify='center' align='center' gap='10px'>
                        <Image src={AgileProLogo} alt='Logo' boxSize='60px' objectFit='contain'></Image>
                        <Text fontSize='2xl' fontWeight='500' marginBottom='0px'>AgilePro</Text>
                    </Flex>
                </NavLink>
                <HStack gap={ 4 }>
                    <NavLink to='/Signup'>
                        <Button colorScheme='messenger' variant='outline'>Sign Up</Button>
                    </NavLink>
                    <NavLink to='/Login'>
                        <Button colorScheme='messenger'>Login</Button>
                    </NavLink>
                </HStack>
            </HStack>
        </div>
    )
}

export default NavBar