import { Button, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/react";
import { Ticket } from "../../models/Ticket";
import * as Yup from "yup";
import { Form, Formik } from "formik";
import MyTextArea from "../common/form/MyTextArea";
import MyDropdown from "../common/form/MyDropdown";
import UserDropdown from "../common/form/UserDropdown";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";

interface Props {
    isOpen: boolean
    onClose: () => void
    ticket: Ticket
}

export default observer(function TicketDetailsModal({ isOpen, onClose, ticket }: Props) {
    const { projectStore } = useStore();

    const validationSchema = Yup.object({
        title: Yup.string().required("Title field is required.").max(255)
    })

    return (
        <Formik
            initialValues={{ title: ticket.title, description: ticket.description, status: ticket.status, priority: ticket.priority, assignee: ticket.assignee, author: ticket.author }}
            onSubmit={(values) => console.log(values)}
            validationSchema={validationSchema}
        >
            {({ handleSubmit, isSubmitting, errors, dirty, resetForm }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Modal isOpen={isOpen} onClose={() => { onClose(); resetForm() }}>
                        <ModalOverlay />
                        <ModalContent>
                            <ModalCloseButton />

                            <ModalHeader></ModalHeader>

                            <ModalBody>
                                <MyTextArea name="title" initialValue={ticket.title} variant="unstyled" colorScheme="messenger" fontSize="24" fontWeight="600" resize="none" _focus={{ border: "2px solid #0c66e4" }} />
                                <MyTextArea name="description" initialValue={ticket.description} placeholder="Enter a description..." label="Description" variant="outline" resize="none" />
                                <MyDropdown name="status" options={["To do", "In progress", "Done"]} currentSelection={ticket.status} />
                                <MyDropdown name="priority" options={["low", "medium", "high"]} currentSelection={ticket.priority} />
                                <UserDropdown name="assignee" options={projectStore.selectedProject!.users} currentSelection={ticket.assignee} allowNull />
                                <UserDropdown name="author" options={projectStore.selectedProject!.users} currentSelection={ticket.author} />
                            </ModalBody>

                            <ModalFooter>
                                {dirty && <Button type="submit" isLoading={isSubmitting}>Save changes</Button>}
                            </ModalFooter>
                        </ModalContent>
                    </Modal>
                </Form>
            )}
        </Formik>
    )
})