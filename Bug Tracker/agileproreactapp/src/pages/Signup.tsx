import { Card, CardHeader, CardBody, CardFooter, Heading, Image, Text, } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import Image1 from "../assets/AgileProLoginPageImage1.png";
import Image2 from "../assets/AgileProLoginPageImage2.png";
import "../styles/signup.css";
import RegisterForm from "../components/RegisterForm";


export default function Signup(){
    return (
        <div className='login-main-wrapper'>
            <Image src={Image1} pos='absolute' bottom='0' left='3rem' boxSize={[0, 200, 300, 400]} />
            <Image src={Image2} pos='absolute' bottom='0' right='3rem' boxSize={[0, 200, 300, 400]} />

            <Card variant='outline' align='center' maxW='lg' width='100%' boxShadow='rgba(0, 0, 0, 0.24) 0px 3px 8px'>
                <CardHeader>
                    <Heading size='lg'>Sign Up</Heading>
                </CardHeader>
                <CardBody>
                    <RegisterForm/>
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