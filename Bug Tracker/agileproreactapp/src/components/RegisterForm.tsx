import { Form, Formik } from "formik";
import * as Yup from "yup";
import { useStore } from "../stores/store";
import MyTextInput from "./common/form/MyTextInput";
import { MdEmail} from "react-icons/md";
import { FaUser } from "react-icons/fa";
import { Button, Center, Flex, Stack, Text } from "@chakra-ui/react";
import { FaLock } from "react-icons/fa6";

export default function () {
    const { userStore } = useStore();

    const nameRegex = /^[a-zA-Z\s'-]+$/;

    const validationSchema = Yup.object({
        email: Yup.string().required("Email is required.").email("Must enter a valid email.").max(320, "Email cannot exceed 320 characters.").trim(),
        firstName: Yup.string().required("First name is required.").max(50, "First name cannot exceed 50 characters.").matches(nameRegex, "First name can only contain letters, spaces, hyphens, and apostrophes.").trim(),
        lastName: Yup.string().required("Last name is required.").max(50, "Last name cannot exceed 50 characters.").matches(nameRegex, "Last name can only contain letters, spaces, hyphens, and apostrophes.").trim(),
        password: Yup.string().required("Password is required.").min(6, "Password must be at least 6 characters.").trim(),
        confirmPassword: Yup.string().required("Confirm password is required.").oneOf([Yup.ref("password")], "Passwords do not match.")
    })

    return (
        <Formik
            initialValues={{ email: "", firstName: "", lastName: "", password: "", confirmPassword: "", error: null }}
            onSubmit={(values, { setErrors }) => userStore.register(values).catch(() => { setErrors({ error: "Some of the information you entered was invalid." }) })}
            validationSchema={validationSchema}
        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Stack spacing={8}>
                        <MyTextInput name="email" placeholder="Email" label="Email" leftIcon={MdEmail} />
                        <Flex gap={4}>
                            <MyTextInput name="firstName" placeholder="First Name" label="First Name" leftIcon={FaUser} />
                            <MyTextInput name="lastName" placeholder="Last Name" label="Last Name" leftIcon={FaUser} />
                        </Flex>
                        <MyTextInput name="password" placeholder="Password" label="Password" leftIcon={FaLock} hideable />
                        <MyTextInput name="confirmPassword" placeholder="Confirm Password" label="Confirm Password" leftIcon={FaLock} hideable />
                        {errors.error && <Text color="red">{errors.error}</Text>}

                        <Center>
                            <Button type="submit" isLoading={isSubmitting} colorScheme="messenger" w="100%">Register</Button>
                        </Center>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}