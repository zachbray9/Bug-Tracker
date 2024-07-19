import { Center, Flex, IconButton, Stack } from "@chakra-ui/react";
import ChangeableAvatar from "../components/common/ImageUpload/ChangeableAvatar";
import { Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import MyTextInput from "../components/common/form/MyTextInput";
import { CheckIcon, CloseIcon } from "@chakra-ui/icons";
import * as Yup from "yup";

export default observer(function Profile() {
    const { userStore } = useStore();
    const { user } = userStore;

    const nameRegex = /^[a-zA-Z\s'-]+$/;

    return (
        <Center flexDir="column" gap={8} justifyContent="start" height="100dvh">
            <ChangeableAvatar />

            {/*First Name form*/}
            <Formik
                initialValues={{ firstName: user!.firstName, error: null }}
                onSubmit={(values, { setErrors, resetForm }) => {
                    try {
                        userStore.updateUser(user!.id, "firstName", values.firstName)
                        resetForm({ values });
                    } catch (error) {
                        setErrors({ error: "Something went wrong, please try again." })
                    }
                }}
                validationSchema={Yup.object({
                    firstName: Yup.string().required("First name is required.").max(50, "First name cannot be longer than 50 characters.").matches(nameRegex, "First name can only contain letters, spaces, hyphens, and apostrophes.").trim()
                })}
            >
                {({ handleSubmit, dirty, resetForm }) => (
                    <Form onSubmit={handleSubmit} autoComplete="off">
                        <Stack align="end">
                            <MyTextInput name="firstName" placeholder="First name" label="First name" />
                            {dirty &&
                                <Flex gap={2}>
                                    <IconButton aria-label="confirm-first-name-button" icon={<CheckIcon />} type="submit" size="sm"></IconButton>
                                    <IconButton aria-label="cancel-first-name-button" icon={<CloseIcon />} size="sm" onClick={() => resetForm()}></IconButton>
                                </Flex>
                            }
                        </Stack>
                    </Form>
                )}
            </Formik>

            {/*Last Name form*/}
            <Formik
                initialValues={{ lastName: user!.lastName, error: null }}
                onSubmit={(values, { setErrors, resetForm }) => {
                    try {
                        userStore.updateUser(user!.id, "lastName", values.lastName)
                        resetForm({ values });
                    } catch (error) {
                        setErrors({ error: "Something went wrong, please try again." })
                    }
                }}
                validationSchema={Yup.object({
                    lastName: Yup.string().required("Last name is required.").max(50, "Last name cannot be longer than 50 characters.").matches(nameRegex, "Last name can only contain letters, spaces, hyphens, and apostrophes.").trim()
                })}
            >
                {({ handleSubmit, dirty, resetForm }) => (
                    <Form onSubmit={handleSubmit} autoComplete="off">
                        <Stack align="end">
                            <MyTextInput name="lastName" placeholder="Last name" label="Last name" />
                            {dirty &&
                                <Flex gap={2}>
                                    <IconButton aria-label="confirm-last-name-button" icon={<CheckIcon />} type="submit" size="sm"></IconButton>
                                    <IconButton aria-label="cancel-last-name-button" icon={<CloseIcon />} size="sm" onClick={() => resetForm()}></IconButton>
                                </Flex>    
                            }
                        </Stack>
                    </Form>
                )}
            </Formik>

            {/*Email form*/}
            <Formik
                initialValues={{ email: user!.email, error: null }}
                onSubmit={(values, { setErrors, resetForm }) => {
                    try {
                        userStore.updateUser(user!.id, "email", values.email)
                        resetForm({ values });
                    } catch (error) {
                        setErrors({ error: "Something went wrong, please try again." })
                    }
                }}
                validationSchema={Yup.object({
                    email: Yup.string().required("Email is required.").email("Invalid email format.").max(320, "Email cannot exceed 320 characters.").trim()
                })}
            >
                {({ handleSubmit, dirty, resetForm }) => (
                    <Form onSubmit={handleSubmit} autoComplete="off">
                        <Stack align="end">
                            <MyTextInput name="email" placeholder="Email" label="Email" />
                            {dirty &&
                                <Flex gap={2}>
                                    <IconButton aria-label="confirm-email-button" icon={<CheckIcon />} type="submit" size="sm"></IconButton>
                                    <IconButton aria-label="cancel-email-button" icon={<CloseIcon />} size="sm" onClick={() => resetForm()}></IconButton>
                                </Flex>
                            }
                        </Stack>
                    </Form>
                )}
            </Formik>
        </Center>
    )
})