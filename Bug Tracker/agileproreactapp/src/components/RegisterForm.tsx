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

    const validationSchema = Yup.object({
        Email: Yup.string().required("Email field is required.").email("Must enter a valid email."),
        FirstName: Yup.string().required("First name field is required."),
        LastName: Yup.string().required("Last name field is required."),
        Password: Yup.string().required("Password field is required."),
        ConfirmPassword: Yup.string().required("Confirm password field is required.").oneOf([Yup.ref("Password")], "Passwords do not match.")
    })

    return (
        <Formik
            initialValues={{ Email: "", FirstName: "", LastName: "", Password: "", ConfirmPassword: "", error: null }}
            onSubmit={(values, { setErrors }) => userStore.register(values).catch(() => { setErrors({ error: "Some of the information you entered was invalid." }) })}
            validationSchema={validationSchema}
        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Stack spacing={8}>
                        <MyTextInput name="Email" placeholder="Email" label="Email" leftIcon={MdEmail} />
                        <Flex gap={4}>
                            <MyTextInput name="FirstName" placeholder="First Name" label="First Name" leftIcon={FaUser} />
                            <MyTextInput name="LastName" placeholder="Last Name" label="Last Name" leftIcon={FaUser} />
                        </Flex>
                        <MyTextInput name="Password" placeholder="Password" label="Password" leftIcon={FaLock} hideable />
                        <MyTextInput name="ConfirmPassword" placeholder="Confirm Password" label="Confirm Password" leftIcon={FaLock} hideable />
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