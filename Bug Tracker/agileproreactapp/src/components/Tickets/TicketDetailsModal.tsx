import {  Button, Flex, Grid, Heading, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Stack, Text } from "@chakra-ui/react";
import * as Yup from "yup";
import { Form, Formik } from "formik";
import MyTextArea from "../common/form/MyTextArea";
import MyDropdown from "../common/form/MyDropdown";
import UserDropdown from "../common/form/UserDropdown";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { format } from "date-fns"
import { StatusOptions } from "../common/Options/StatusOptions";
import { PriorityOptions } from "../common/Options/PriorityOptions";
import TicketCommentSection from "../Comments/TicketCommentSection";

interface Props {
    isOpen: boolean
    onClose: () => void
}

export default observer(function TicketDetailsModal({ isOpen, onClose }: Props) {
    const { projectStore, ticketStore } = useStore();
    const { selectedTicket } = ticketStore;

    const validationSchema = Yup.object({
        title: Yup.string().required("Title field is required.").max(255)
    })

    const formattedDate = format(new Date(ticketStore.selectedTicket!.dateSubmitted), "MMMM d, yyyy 'at' h:mm a")

    return (
        <Modal size="xl" isOpen={isOpen} onClose={() => { onClose(); }}>
            <ModalOverlay />
            <ModalContent maxW="65vw">
                <ModalCloseButton />

                <ModalHeader></ModalHeader>

                <Formik
                    initialValues={{
                        id: selectedTicket!.id,
                        projectId: projectStore.selectedProject!.id,
                        title: selectedTicket!.title,
                        description: selectedTicket!.description,
                        status: selectedTicket!.status,
                        priority: selectedTicket!.priority,
                        assignee: selectedTicket!.assignee,
                        author: selectedTicket!.author,
                        error: null
                    }}
                    onSubmit={async (values, { setErrors, resetForm }) => {
                        try {
                            await ticketStore.updateTicket(values);
                            resetForm({ values });
                        } catch {
                            setErrors({ error: "Something went wrong. Please try again." })
                        }
                    }}
                    validationSchema={validationSchema}
                >
                    {({ handleSubmit, isSubmitting, errors, dirty, values }) => (
                        <Form onSubmit={handleSubmit} autoComplete="off">
                            <ModalBody>
                                <Grid templateColumns="2fr 1fr" gap={8}>
                                    <Stack gap={16} maxH="75vh" overflowY="auto">
                                        <Stack gap={4}>
                                            <MyTextArea name="title" initialValue={values.title} variant="unstyled" colorScheme="messenger" fontSize="24" fontWeight="600" whiteSpace="pre-wrap" resize="none" overflow="hidden" _focus={{ border: "2px solid #0c66e4" }} />
                                            <MyTextArea name="description" initialValue={values.description} placeholder="Enter a description..." label="Description" variant="outline" resize="none" overflow="hidden" />
                                        </Stack>

                                        <TicketCommentSection ticketId={selectedTicket!.id} />
                                    </Stack>

                                    <Stack width="100%" gap={4}>
                                        <Flex justify="start" gap={4}>
                                            <MyDropdown name="status" options={StatusOptions} />
                                            <MyDropdown name="priority" options={PriorityOptions} />
                                        </Flex>

                                        <Stack gap={4} width="100%" padding={4} border="1px solid #c8c8c8">
                                            <Flex align="center" gap={24} width="100%">
                                                <Heading size="xs">Assignee</Heading>
                                                <UserDropdown name="assignee" options={projectStore.selectedProject!.users} allowNull />
                                            </Flex>

                                            <Flex align="center" gap={24} width="100%">
                                                <Heading size="xs">Reporter</Heading>
                                                <UserDropdown name="author" options={projectStore.selectedProject!.users} />
                                            </Flex>
                                        </Stack>

                                        <Text fontSize="smaller" color="#44546f">{`Created ${formattedDate}`}</Text>
                                    </Stack>
                                </Grid>
                            </ModalBody>

                            <ModalFooter flexDir="column" alignItems="end" gap={2}>
                                {errors.error && < Text color="red">{errors.error}</Text>}
                                {dirty && <Button type="submit" isLoading={isSubmitting} >Save changes</Button>}
                            </ModalFooter>
                        </Form>
                    )}
                </Formik>
            </ModalContent>
        </Modal>
    )
})