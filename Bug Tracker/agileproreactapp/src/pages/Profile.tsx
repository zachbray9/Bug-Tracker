import { Flex, Grid, Heading, IconButton, Stack, Text } from "@chakra-ui/react";
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
        <Stack gap={16} align="start" justify="start" paddingLeft={{ base: '1rem', md: '4rem' }} >
            <Heading size={{base: 'md', md: 'lg'}}>Account settings</Heading>

            <Stack>
                <Heading as="h5" size="sm">Personal Information</Heading>
                <Grid templateColumns={{ base: '1fr', md: '1fr 2fr' }} gap={{ base: 12, md: 16 } }>
                    <Text >This is how other users see your profile. Anything you change here will be saved and
                        represent how other users see you.
                    </Text>

                    <Stack gap={4} width="fit-content" align="center">
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

                    </Stack>
                </Grid>
            </Stack>

            <Stack>
                <Heading as="h5" size="sm">Email address</Heading>
                <Grid templateColumns={{ base: '1fr', md: '1fr 2fr' }} gap={{base: 12, md: 16}}>
                    <Text>This is the email address that we will use to send email notifications. It also doubles
                        as your Username to login to your account. Any changes made to your email here will be reflected to
                        your login credentials as well.
                    </Text>

                    <Stack width="fit-content" align="center">
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
                    </Stack>
                </Grid>
            </Stack>

        </Stack>
    )
})