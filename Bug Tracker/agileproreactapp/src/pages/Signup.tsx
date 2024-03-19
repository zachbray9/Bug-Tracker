import { Button, Card, CardHeader, CardBody, CardFooter, Flex, Heading, Image, Input, InputGroup, InputLeftElement, InputRightElement, Text, Stack } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { FaLock } from "react-icons/fa6";
import Image1 from "../assets/AgileProLoginPageImage1.png";
import Image2 from "../assets/AgileProLoginPageImage2.png";
import { useState } from "react";

const Signup = () => {
    const [show, setShow] = useState(false);
    const [showConfirm, setShowConfirm] = useState(false);

    const togglePasswordVisibility = () => {
        if (show === false)
            setShow(true);
        else
            setShow(false);
    }

    const toggleConfirmPasswordVisibility = () => {
        if (showConfirm === false)
            setShowConfirm(true);
        else
            setShowConfirm(false);
    }

    return (
        <div className='login-main-wrapper'>
            <Image src={Image1} pos='absolute' bottom='0' left='3rem' boxSize={[0, 200, 300, 400]} />
            <Image src={Image2} pos='absolute' bottom='0' right='3rem' boxSize={[0, 200, 300, 400]} />

            <Card variant='outline' align='center' maxW='lg' width='100%' boxShadow='rgba(0, 0, 0, 0.24) 0px 3px 8px'>
                <CardHeader>
                    <Heading size='lg'>Sign Up</Heading>
                </CardHeader>
                <CardBody>
                    <form>
                        <Stack>
                            <Text size='xs' fontWeight='400' marginBottom='0px'>Email</Text>
                            <InputGroup>
                                <InputLeftElement>
                                    <FaUser color='#d3d3d3' />
                                </InputLeftElement>

                                <Input size='md' mb={4} placeholder='Email'></Input>
                            </InputGroup>
                        </Stack>

                        <Stack>
                            <Text size='xs' marginBottom='0px'>Password</Text>
                            <InputGroup>
                                <InputLeftElement>
                                    <FaLock color='#d3d3d3' />
                                </InputLeftElement>

                                <InputRightElement width='4.5rem'>
                                    <Button h='1.75rem' size='sm' onClick={togglePasswordVisibility}>
                                        {show ? 'Hide' : 'Show'}
                                    </Button>
                                </InputRightElement>
                                <Input type={show ? 'text' : 'password'} size='md' mb={4} placeholder='Password'></Input>

                            </InputGroup>
                        </Stack>

                        <Stack>
                            <Text size='xs' marginBottom='0px'>Confirm Password</Text>
                            <InputGroup>
                                <InputLeftElement>
                                    <FaLock color='#d3d3d3' />
                                </InputLeftElement>

                                <InputRightElement width='4.5rem'>
                                    <Button h='1.75rem' size='sm' onClick={toggleConfirmPasswordVisibility}>
                                        {showConfirm ? 'Hide' : 'Show'}
                                    </Button>
                                </InputRightElement>
                                <Input type={showConfirm ? 'text' : 'password'} size='md' mb={4} placeholder='Confirm Password'></Input>

                            </InputGroup>
                        </Stack>

                        <Flex width='100%' justify='center' align='center'>
                            <Button colorScheme='messenger' width='100%' color='white'>Login</Button>
                        </Flex>
                    </form>
                </CardBody>
                <CardFooter>
                    <NavLink to='/login'>
                        <Text>Already have an account?</Text>
                    </NavLink>
                </CardFooter>
            </Card>
        </div>
    )
}

export default Signup