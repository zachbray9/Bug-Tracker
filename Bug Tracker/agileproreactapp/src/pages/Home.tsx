import "../styles/home.css";
import { Button, Flex, Image, Text } from "@chakra-ui/react";
import HeroImage from "../assets/AgileProLandingPageImage.png";
import { NavLink } from "react-router-dom";

const Home = () => {
    return (
        <div className='main-wrapper'>
            <section className='hero-section'>
                <Flex justify='center' align='center' direction={{ base: "column", md: "row" }} maxWidth={[540, 720, 960, 1140]}>
                    <Flex className='hero-text-wrapper' direction='column'>
                        <Text fontSize='5xl' color='white' fontWeight='600'>Make your development process Agile</Text>
                        <Text fontSize='2xl' color='white'>Manage tasks, collaborate with team members, and improve your productivity.</Text>
                        <NavLink to='signup'>
                            <Button colorScheme='messenger' marginTop='10px'>Get Started</Button>
                        </NavLink>
                    </Flex>
                    <Image src={HeroImage} boxSize={ [300, 400, 450, 550, 600] } objectFit='cover' />
                </Flex>
            </section>
            <section className='info-section'>
                <Flex direction='column' maxWidth={[540, 720, 960, 1140]}>
                    <Text fontSize='4xl' fontWeight='500'>Increase your productivity</Text>
                    <Text fontSize='2xl'>Easy to use, navigate, and manage. All it takes are boards, lists, and cards to give a clear picture of an entire teams work flow.</Text>
                    <Image src={HeroImage} boxSize={[400, 500, 600, 700]} objectFit='cover' />
                </Flex>
            </section>
        </div>
    )
}

export default Home