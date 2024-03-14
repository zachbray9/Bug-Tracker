import "../styles/home.css";
import { Box, Button, Card, CardBody, CardHeader, Flex, Heading, Image, Square, Text } from "@chakra-ui/react";
import HeroImage from "../assets/AgileProLandingPageImage.png";
import DemoImage from "../assets/Bug Tracker Project Details Page Screenshot.png";
import { NavLink } from "react-router-dom";
import { FaFolder, FaTasks } from "react-icons/fa";
import { BiSolidMegaphone } from "react-icons/bi";
import { PiLeafDuotone } from "react-icons/pi";

const Home = () => {
    return (
        <div className='main-wrapper'>
            <section className='hero-section'>
                <Flex justify='center' align='center' direction={{ base: "column", md: "row" }} maxWidth={[540, 720, 960, 1140]}>
                    <Flex direction='column'>
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
                    <Text fontSize='2xl'>Easy to use, navigate, and manage. All it takes are boards, lists, and cards to give a clear picture of an entire team's work flow.</Text>
                    <Image src={DemoImage} boxSize={[400, 500, 600, 700]} objectFit='contain' />
                </Flex>
            </section>
            <section className='usecase-section'>
                <Flex maxWidth={[540, 720, 960, 1140]} width='100%'>
                    <Flex direction='column' gap='1rem'>
                        <Text fontSize='4xl' fontWeight='500'>Workflows for any project, big or small</Text>
                    </Flex>
                </Flex>

                <Flex wrap='wrap' gap='1rem'>
                    <Card className='usecase-card' maxW='sm' variant='outline'>
                        <Box bg='rgb(255, 116, 82)' height='3rem'></Box>
                        <CardHeader>
                        </CardHeader>
                        <CardBody>
                            <Heading size='md'>Project Management</Heading>
                            <Text>Keep tasks in order, deadlines on track, and team members aligned with AgilePro.</Text>
                        </CardBody>
                        <Square className='card-icon' bg='white' position='absolute' size='3rem' left='1rem' top='1.5rem' borderRadius='1rem'>
                        <FaFolder color='rgb(255, 116, 82)' size='2rem'></FaFolder> 
                        </Square>
                    </Card>

                    <Card className='usecase-card' maxW='sm' variant='outline'>
                    <Box bg='rgb(38, 132, 255)' height='3rem'></Box>
                        <CardHeader>
                        </CardHeader>
                        <CardBody>
                            <Heading size='md'>Meetings</Heading>
                            <Text>Empower your team's meetings to be more productive, focused, and thorough.</Text>
                        </CardBody>
                        <Square className='card-icon' bg='white' position='absolute' size='3rem' left='1rem' top='1.5rem' borderRadius='1rem'>
                        <BiSolidMegaphone color='rgb(38, 132, 255)' size='2rem'></BiSolidMegaphone>
                        </Square>
                    </Card>

                    <Card className='usecase-card' maxW='sm' variant='outline'>
                    <Box bg='rgb(87, 217, 163)' height='3rem'></Box>
                        <CardHeader>
                        </CardHeader>
                        <CardBody>
                            <Heading size='md'>Onboarding</Heading>
                            <Text>Onboaring to a new company is easy with AgilePro's visual layout of to-do's and progress tracking.</Text>
                        </CardBody>
                        <Square className='card-icon' bg='white' position='absolute' size='3rem' left='1rem' top='1.5rem' borderRadius='1rem'>
                        <PiLeafDuotone color='rgb(87, 217, 163)' size='2rem'></PiLeafDuotone>
                        </Square>
                    </Card>

                    <Card className='usecase-card' maxW='sm' variant='outline'>
                    <Box bg='rgb(255, 196, 0)' height='3rem'></Box>
                        <CardHeader>
                        </CardHeader>
                        <CardBody>
                            <Heading size='md'>Task Management</Heading>
                            <Text>Track, manage, complete, and bring tasks together like pieces of a puzzle and make your team's projects a cohesive success every time.</Text>
                        </CardBody>
                        <Square className='card-icon' bg='white' position='absolute' size='3rem' left='1rem' top='1.5rem' borderRadius='1rem'>
                        <FaTasks color='rgb(255, 196, 0)' size='2rem'></FaTasks>
                        </Square>
                    </Card>
                </Flex>
            </section>

            <section className='getstarted-section'>
                <Flex direction='column' align='center' gap='1rem'>
                    <Text fontSize='4xl' fontWeight='500' color='white'>Get started with AgilePro today</Text>
                    <NavLink to='signup'>
                        <Button colorScheme='messenger'>Create an account - It's free!</Button>
                    </NavLink>
                </Flex>
            </section>
        </div>
    )
}

export default Home