import { ErrorMessage, Form, Formik } from "formik";
import { useStore } from "../stores/store";
import { Button, Center, Stack, Text } from "@chakra-ui/react";
import MyTextInput from "./common/form/MyTextInput";
import { MdEmail } from "react-icons/md";
import { FaLock } from "react-icons/fa";
import * as Yup from "yup";

export default function LoginForm() {
    const { userStore } = useStore();

    const validationSchema = Yup.object({
        Email: Yup.string().required("Email field is required.").email("The email you entered is not a valid email."),
        Password: Yup.string().required("Password field is required.")
    });

    return (
        <Formik
            initialValues={{ Email: '', Password: '', error: null }}
            onSubmit={(values, { setErrors }) => userStore.login(values).catch(() => setErrors({ error: "Invalid email or password." }))}
            validationSchema={validationSchema}
            
        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Stack spacing={8} >
                        <MyTextInput name="Email" placeholder="Email" label="Email" leftIcon={MdEmail} />
                        <MyTextInput name="Password" placeholder="Password" label="Password" leftIcon={FaLock} hideable />
                        {errors.error && <Text color="red">{errors.error}</Text>}
                        <Center>
                            <Button type='submit' isLoading={isSubmitting} colorScheme="messenger" w="100%">Login</Button>
                        </Center>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}