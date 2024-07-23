import { HStack, Text } from "@chakra-ui/react";
import { FaGithub } from "react-icons/fa";
import "../styles/footer.css"

const Footer = () => {
    return (
        <HStack backgroundColor='rgb(9, 30, 66)' justifyContent='space-between' alignItems='center' padding='20px 40px' width="100%">
            <Text fontSize='1xl' color='white' marginBottom='0px'>&copy; 2024 AgilePro</Text>
            <a className='github-link' href='https://github.com/zachbray9/Bug-Tracker' target='_blank'>
                <FaGithub fontSize='24px' color='white' />
            </a>
        </HStack>
    )
}

export default Footer;