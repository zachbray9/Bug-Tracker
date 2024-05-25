import { Form, Formik } from "formik";
import { useStore } from "../stores/store";
import { Button, Center, Stack } from "@chakra-ui/react";
import MyTextInput from "./common/form/MyTextInput";
import { MdEmail } from "react-icons/md";
import { FaLock } from "react-icons/fa";

export default function LoginForm() {
    const { userStore } = useStore();

    return (
        <Formik
            initialValues={{ Email: '', Password: '' }}
            onSubmit={values => userStore.login(values)}
        >
            {({ handleSubmit, isSubmitting }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Stack spacing={8} >
                        <MyTextInput name="Email" placeholder="Email" label="Email" leftIcon={MdEmail} />
                        <MyTextInput name="Password" placeholder="Password" label="Password" leftIcon={FaLock} hideable />

                        <Center>
                            <Button type='submit' isLoading={isSubmitting} colorScheme="messenger" w="100%">Login</Button>
                        </Center>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}