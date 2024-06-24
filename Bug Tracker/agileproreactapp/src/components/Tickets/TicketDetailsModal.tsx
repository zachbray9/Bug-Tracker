import {  Button, Flex, Grid, Heading, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Stack, Text } from "@chakra-ui/react";
import * as Yup from "yup";
import { Form, Formik } from "formik";
import MyTextArea from "../common/form/MyTextArea";
import MyDropdown from "../common/form/MyDropdown";
import UserDropdown from "../common/form/UserDropdown";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { format } from "date-fns"

interface Props {
    isOpen: boolean
    onClose: () => void
}

export default observer(function TicketDetailsModal({ isOpen, onClose }: Props) {
    const { projectStore, ticketStore } = useStore();

    const validationSchema = Yup.object({
        title: Yup.string().required("Title field is required.").max(255)
    })

    const formattedDate = format(new Date(ticketStore.selectedTicket!.dateSubmitted), "MMMM d, yyyy 'at' h:mm a")

    console.log("TicketDetailsModal Ticket: " + ticketStore.selectedTicket!.title);

    return (
        <Formik
            initialValues={{
                id: ticketStore.selectedTicket!.id,
                projectId: projectStore.selectedProject!.id,
                title: ticketStore.selectedTicket!.title,
                description: ticketStore.selectedTicket!.description,
                status: ticketStore.selectedTicket!.status,
                priority: ticketStore.selectedTicket!.priority,
                assignee: ticketStore.selectedTicket!.assignee,
                author: ticketStore.selectedTicket!.author, error: null
            }}
            onSubmit={(values, { setErrors }) => {
                console.log(values);
                try {
                    projectStore.updateTicket(values);
                    onClose();
                } catch (error) {
                    setErrors({ error: "Something went wrong. Please try again." });
                }
            }}
            validationSchema={validationSchema}
        >
            {({ handleSubmit, isSubmitting, errors, dirty, resetForm }) => (
                <Form id="updateTicketForm" onSubmit={handleSubmit} autoComplete="off">
                    <Modal size="xl" isOpen={isOpen} onClose={() => { onClose(); resetForm(); }}>
                        <ModalOverlay />
                        <ModalContent maxW="65vw">
                            <ModalCloseButton />

                            <ModalHeader></ModalHeader>

                            <ModalBody>
                                <Grid templateColumns="2fr 1fr" gap={8}>
                                    <Stack gap={4}>
                                        <MyTextArea name="title" initialValue={ticketStore.selectedTicket!.title} variant="unstyled" colorScheme="messenger" fontSize="24" fontWeight="600" resize="none" _focus={{ border: "2px solid #0c66e4" }} />
                                        <MyTextArea name="description" initialValue={ticketStore.selectedTicket!.description} placeholder="Enter a description..." label="Description" variant="outline" resize="none" />
                                    </Stack>

                                    <Stack width="100%" gap={4}>
                                        <Flex justify="start" gap={4}>
                                            <MyDropdown name="status" options={["To do", "In progress", "Done"]} currentSelection={ticketStore.selectedTicket!.status} />
                                            <MyDropdown name="priority" options={["low", "medium", "high"]} currentSelection={ticketStore.selectedTicket!.priority} />
                                        </Flex>

                                        <Stack gap={4} width="100%" padding={4} border="1px solid #c8c8c8">
                                            <Flex align="center" gap={24} width="100%">
                                                <Heading size="xs">Assignee</Heading>
                                                <UserDropdown name="assignee" options={projectStore.selectedProject!.users} currentSelection={ticketStore.selectedTicket!.assignee} allowNull />
                                            </Flex>

                                            <Flex align="center" gap={24} width="100%">
                                                <Heading size="xs">Reporter</Heading>
                                                <UserDropdown name="author" options={projectStore.selectedProject!.users} currentSelection={ticketStore.selectedTicket!.author} />
                                            </Flex>
                                        </Stack>

                                        <Text fontSize="smaller" color="#44546f">{`Created ${formattedDate}`}</Text>
                                    </Stack>
                                </Grid>
                            </ModalBody>

                            <ModalFooter>
                                {errors.error && < Text color="red">{errors.error}</Text>}
                                {dirty && <Button type="submit" isLoading={isSubmitting} form="updateTicketForm">Save changes</Button>}
                            </ModalFooter>
                        </ModalContent>
                    </Modal>
                </Form>
            )}
        </Formik>
    )
})