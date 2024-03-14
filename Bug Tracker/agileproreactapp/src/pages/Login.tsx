import { Button, Card, CardBody, CardFooter, CardHeader, Flex, Heading, Input, InputGroup, InputLeftElement, Stack, Text } from "@chakra-ui/react"
import { FaUser } from "react-icons/fa"
import { FaLock } from "react-icons/fa6"
import { NavLink } from "react-router-dom"
import "../styles/login.css"

const Login = () => {
    return (
        <div className='main-wrapper'>
            <Card align='center' size='lg'>
                <CardHeader>
                    <Heading size='lg'>Login</Heading>
                </CardHeader>
                <CardBody>
                    <Stack>
                        <Text size='xs' fontWeight='400' marginBottom='0px'>Username</Text>
                        <InputGroup>
                            <InputLeftElement>
                                <FaUser color='#d3d3d3' />
                            </InputLeftElement>
                            <Input size='md' mb={4} placeholder='Username'></Input>
                        </InputGroup>
                    </Stack>
                    <Stack>
                        <Text size='xs' marginBottom='0px'>Password</Text>
                        <InputGroup>
                            <InputLeftElement>
                                <FaLock color='#d3d3d3' />
                            </InputLeftElement>
                            <Input size='md' mb={4} placeholder='Password'></Input>
                        </InputGroup>
                    </Stack>
                    <Flex width='100%' justify='center' align='center'>
                        <Button width='100%' bgGradient='linear(to-r, #5f0a87, #a4508b)' color='white'>Login</Button>
                    </Flex>
                </CardBody>
                <CardFooter>
                    <NavLink to='signup'>
                        <Text>Don't have an account?</Text>
                    </NavLink>
                </CardFooter>
            </Card>
        </div>
    )
}

export default Login