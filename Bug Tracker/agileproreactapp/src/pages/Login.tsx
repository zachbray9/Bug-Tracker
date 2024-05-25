import { Card, CardBody, CardFooter, CardHeader, Center, Heading, Image, Text, calc } from "@chakra-ui/react"
import { NavLink } from "react-router-dom"
import Image1 from "../assets/AgileProLoginPageImage1.png";
import Image2 from "../assets/AgileProLoginPageImage2.png";
import { observer } from "mobx-react-lite";
import LoginForm from "../components/LoginForm";

export default observer(function Login() {

    return (
        <Center pos="relative" height="calc(100vh - 60px)">
            <Image src={Image1} pos='absolute' bottom='0' left='3rem' boxSize={[0, 200, 300, 400]} />
            <Image src={Image2} pos='absolute' bottom='0' right='3rem' boxSize={[0, 200, 300, 400]} />

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
    )
})