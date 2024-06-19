import { Button, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/react";
import { Ticket } from "../../models/Ticket";
import * as Yup from "yup";
import { Form, Formik } from "formik";
import MyTextArea from "../common/form/MyTextArea";
import { useEffect } from "react";

interface Props {
    isOpen: boolean
    onClose: () => void
    ticket: Ticket
}

export default function TicketDetailsModal({ isOpen, onClose, ticket }: Props) {

    const validationSchema = Yup.object({
        title: Yup.string().required().max(255)
    })

    return (
        <Formik
            initialValues={{ title: ticket.title }}
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
                                <MyTextArea name="title" initialValue={ticket.title} variant="unstyled" colorScheme="messenger" fontSize="24" fontWeight="600" resize="none" _focus={{ border: "2px solid #0c66e4" }}/>
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
}