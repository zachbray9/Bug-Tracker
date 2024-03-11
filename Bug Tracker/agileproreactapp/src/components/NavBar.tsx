import { HStack, Image } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import AgileProLogo from "../assets/AgileProLogo.png";



const NavBar = () => { 
    return (
        <div className='main-wrapper'>
            <HStack className='content-wrapper' justifyContent='space-between' padding=' 0px 40px'>
                <NavLink to='' >
                    <Image src={AgileProLogo} alt='Logo' boxSize='80px' objectFit='cover'></Image>
                </NavLink>
                <HStack gap={ 4 }>
                    <NavLink to='/Signup' className='btn btn-outline-primary'>Sign Up</NavLink>
                    <NavLink to='/Login' className='btn btn-primary'>Login</NavLink>
                </HStack>
            </HStack>
        </div>
    )
}

export default NavBar