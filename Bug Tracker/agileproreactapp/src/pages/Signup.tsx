import { Button, Card, CardHeader, CardBody, CardFooter, Flex, Heading, Image, Input, InputGroup, InputLeftElement, InputRightElement, Text, Stack } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { FaLock } from "react-icons/fa6";
import Image1 from "../assets/AgileProLoginPageImage1.png";
import Image2 from "../assets/AgileProLoginPageImage2.png";
import { useState } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { z } from 'zod';
import { zodResolver } from "@hookform/resolvers/zod/dist/zod.js";
import "../styles/signup.css";


const Signup = () => {
    const [show, setShow] = useState(false);
    const [showConfirm, setShowConfirm] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

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

    /*schema form validation*/
    const schema = z.object({
        email: z.string().email({ message: "Invalid email." }),
        password: z.string().min(8, { message: "Password must be at least 8 characters." }),
        confirmPassword: z.string()
    }).refine((data) => data.password === data.confirmPassword, {
        message: "Passwords do not match.",
        path: ['confirmPassword']
    })

    const { register, handleSubmit, formState: { errors } } = useForm<FormData>({ resolver: zodResolver(schema) });

    const onSubmit = (data: FieldValues) => {
        setIsLoading(true);
        console.log(data);
        setIsLoading(false);
    }


    type FormData = z.infer<typeof schema>;

    return (
        <div className='login-main-wrapper'>
            <Image src={Image1} pos='absolute' bottom='0' left='3rem' boxSize={[0, 200, 300, 400]} />
            <Image src={Image2} pos='absolute' bottom='0' right='3rem' boxSize={[0, 200, 300, 400]} />

            <Card variant='outline' align='center' maxW='lg' width='100%' boxShadow='rgba(0, 0, 0, 0.24) 0px 3px 8px'>
                <CardHeader>
                    <Heading size='lg'>Sign Up</Heading>
                </CardHeader>
                <CardBody>
                    <form onSubmit={handleSubmit(onSubmit)} className='signup-form'>
                        <Stack>
                            <Text size='xs' fontWeight='400' marginBottom='0px'>Email</Text>
                            <InputGroup>
                                <InputLeftElement>
                                    <FaUser color='#d3d3d3' />
                                </InputLeftElement>

                                <Input {...register('email')} size='md' mb={0} placeholder='Email'></Input>
                            </InputGroup>
                            {errors.email && (
                                <Text color='red' mb={0}>{errors.email.message}</Text>
                            )}
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
                                <Input {...register('password')} type={show ? 'text' : 'password'} size='md' mb={0} placeholder='Password'></Input>
                            </InputGroup>
                            {errors.password && (
                                <Text color='red' mb={0}>{errors.password.message}</Text>
                            )}
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
                                <Input {...register('confirmPassword')} type={showConfirm ? 'text' : 'password'} size='md' mb={0} placeholder='Confirm Password'></Input>
                            </InputGroup>
                            {errors.confirmPassword && (
                                <Text color='red' mb={0}>{errors.confirmPassword.message}</Text>
                            )}
                        </Stack>

                        <Flex width='100%' justify='center' align='center'>
                            <Button type='submit' colorScheme='messenger' width='100%' color='white' isLoading={isLoading} loadingText='Submitting'>Login</Button>
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