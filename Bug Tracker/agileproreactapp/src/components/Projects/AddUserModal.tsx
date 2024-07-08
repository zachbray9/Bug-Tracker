import { Button, Flex, Heading, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Stack, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import { Form, Formik } from "formik";
import MyTextInput from "../common/form/MyTextInput";
import MyDropdown from "../common/form/MyDropdown";
import { RoleOptions } from "../common/Options/RoleOptions";
import * as Yup from "yup";

interface Props {
    isOpen: boolean,
    onClose: () => void
}

export default observer(function AddUserModal({ isOpen, onClose }: Props) {
    const { projectStore } = useStore();
    const validationSchema = Yup.object({
        email: Yup.string().email("Must enter a valid email.").required("This field is required.")
    })

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <ModalOverlay />
            <ModalContent>
                <ModalCloseButton />

                <ModalHeader>
                    <Heading size="lg">Add people to project</Heading>
                </ModalHeader>

                <Formik
                    initialValues={{ projectId: projectStore.selectedProject!.id, email: '', role: "Developer", error: null }}
                    onSubmit={(values, { setErrors }) => projectStore.addUser(values).catch(() => setErrors({ error: "User could not be found or is already on this project." }))}
                    validationSchema={validationSchema}
                >
                    {({ handleSubmit, isSubmitting, errors }) => (
                        <Form onSubmit={handleSubmit} autoComplete="off">
                            <ModalBody> 
                                <Stack gap={4}>
                                    <MyTextInput name="email" placeholder="e.g., zach@company.com" label="Email" />
                                    <MyDropdown name="role" options={RoleOptions} label="Role" />
                                </Stack>
                            </ModalBody>

                            <ModalFooter flexDir="column" alignItems="end" gap={2}>
                                    {errors.error && <Text color="red" fontSize="sm">{errors.error}</Text>}
                                    <Flex gap={4}>
                                        <Button variant="ghost" onClick={onClose}>Cancel</Button>
                                        <Button type="submit" colorScheme="messenger" isLoading={isSubmitting}>Add</Button>
                                    </Flex>
                            </ModalFooter>
                        </Form>
                    )}

                </Formik>

            </ModalContent>
        </Modal>
    )
})