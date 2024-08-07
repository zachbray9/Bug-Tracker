import { Card, CardBody, CardFooter, CardHeader, Center, Heading, Image, Text } from "@chakra-ui/react"
import { NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import LoginForm from "../components/LoginForm";
import { Helmet } from "react-helmet-async";

export default observer(function Login() {
    return (
        <>
            <Helmet>
                <title>Login - AgilePro</title>
            </Helmet>

            <Center pos="relative" minH="calc(100svh - 60px)" paddingX="1rem">
                <Image src='https://agileproblobstorage.blob.core.windows.net/site-assets/AgileProLoginPageImageRocket.png' pos='absolute' bottom='0' left={{ base: '1rem', md: '3rem'}} boxSize={[150, 200, 300, 400]} />
                <Image src='https://agileproblobstorage.blob.core.windows.net/site-assets/AgileProLoginPageImageTimer.png' pos='absolute' bottom='0' right={{base: '1rem', md: '3rem'}} boxSize={[150, 200, 300, 400]} />

                <Card variant='outline' align='center' maxW='lg' width='100%' boxShadow='rgba(0, 0, 0, 0.24) 0px 3px 8px'>
                    <CardHeader>
                        <Heading size='lg'>Login</Heading>
                    </CardHeader>
                    <CardBody>
                        <LoginForm/>
                    </CardBody>
                    <CardFooter>
                        <NavLink to='/signup'>
                            <Text>Don't have an account?</Text>
                        </NavLink>
                    </CardFooter>
                </Card>
            
           </Center>
        </>
    )
})