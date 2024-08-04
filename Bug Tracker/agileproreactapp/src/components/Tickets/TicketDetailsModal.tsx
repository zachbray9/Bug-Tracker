import { Flex, Grid, GridItem, Heading, IconButton, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Stack, Text } from "@chakra-ui/react";
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
import { CheckIcon, CloseIcon } from "@chakra-ui/icons";

interface Props {
    isOpen: boolean
    onClose: () => void
}

export default observer(function TicketDetailsModal({ isOpen, onClose }: Props) {
    const { projectStore, ticketStore } = useStore();
    const { selectedTicket } = ticketStore;

    const titleValidationSchema = Yup.object({
        title: Yup.string().required("Title field is required.").max(255, 'Title cannot exceed 255 characters.')
    });

    const descriptionValidationSchema = Yup.object({
        description: Yup.string().max(10000, "Description cannot exceed 10,000 characters.")
    });

    const formattedDate = format(new Date(ticketStore.selectedTicket!.dateSubmitted), "MMMM d, yyyy 'at' h:mm a")

    return (
        <Modal size="xl" isOpen={isOpen} onClose={() => { onClose(); }}>
            <ModalOverlay />
            <ModalContent maxW={{ base: '85vw', md: '65vw'}} >
                <ModalCloseButton />

                <ModalHeader></ModalHeader>


                <ModalBody overflowX='auto'>
                    <Grid templateColumns="2fr 1fr" gap={8} >
                        <GridItem width='100%'>
                            <Stack gap={16} maxH="75svh" minW='275px' width='100%' overflowY="auto">
                                <Stack gap={4}>
                                    {/*Title Form*/}
                                    <Formik
                                        initialValues={{ title: ticketStore.selectedTicket!.title, error: null }}
                                        onSubmit={async (values, { setErrors, resetForm }) => {
                                            try {
                                                await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "title", values.title);
                                                resetForm({ values });
                                            }
                                            catch (error) {
                                                setErrors({ error: "Something went wrong. Please try again." })
                                            }
                                        }}
                                        validationSchema={titleValidationSchema}
                                    >
                                        {({ isSubmitting, values, resetForm, dirty, errors, handleSubmit }) => (
                                            <Form onSubmit={handleSubmit} autoComplete="off">
                                                <Stack align="end">
                                                    <MyTextArea name="title" initialValue={values.title} variant='unstyled' colorScheme='messenger' size={{ base: 'sm', md: 'md' }} fontSize={{base: '18', md: '24'}} fontWeight='600' whiteSpace='pre-wrap' resize='none' overflow='hidden' _focus={{ border: '2px solid #0c66e4' }} />
                                                    {errors.error && <Text color="red">{errors.error}</Text>}
                                                    {dirty &&
                                                        <Flex gap={2} >
                                                            <IconButton aria-label="update-ticket-title" icon={<CheckIcon />} type="submit" isLoading={isSubmitting} size="sm" />
                                                            <IconButton aria-label="cancel-update-ticket-title" icon={<CloseIcon />} onClick={() => resetForm()} size="sm"></IconButton>
                                                        </Flex>
                                                        }
                                                </Stack>
                                            </Form>
                                        )}
                                    </Formik>

                                    {/*Description form*/}
                                    <Formik
                                        initialValues={{ description: ticketStore.selectedTicket!.description, error: null }}
                                        onSubmit={async (values, { setErrors, resetForm }) => {
                                            try {
                                                await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "description", values.description);
                                                resetForm({ values });
                                            } catch (error) {
                                                setErrors({ error: "Something went wrong. Please try again." });
                                            }
                                        }}
                                        validationSchema={descriptionValidationSchema}
                                    >
                                        {({ isSubmitting, values, resetForm, dirty, errors, handleSubmit }) => (
                                            <Form onSubmit={handleSubmit} autoComplete="off">
                                                <Stack align="end">
                                                    <MyTextArea name="description" initialValue={values.description} placeholder="Enter a description..." label="Description" size={{base: 'sm', md: 'md'}} variant="outline" resize="none" overflow="hidden" />
                                                    {errors.error && <Text color="red">{errors.error}</Text>}
                                                    {dirty &&
                                                        <Flex gap={2}>
                                                            <IconButton aria-label="update-ticket-description" icon={<CheckIcon />} type="submit" isLoading={isSubmitting} size="sm" />
                                                            <IconButton aria-label="cancel-update-ticket-description" icon={<CloseIcon />} onClick={() => resetForm()} size="sm"></IconButton>
                                                        </Flex>
                                                    }
                                                </Stack>
                                            </Form>
                                        )}
                                    </Formik>
                               
                                </Stack>

                                <TicketCommentSection ticketId={selectedTicket!.id} />
                            </Stack>
                        </GridItem>

                        <GridItem width='100%'>
                            <Stack width="100%" gap={4}>
                                <Flex justify="start" gap={4}>
                                    {/*Status Form*/}
                                    <Formik
                                        initialValues={{ status: selectedTicket!.status, error: null }}
                                        onSubmit={async (values, { setErrors }) => {
                                            try {
                                                await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "status", values.status)
                                            } catch (error) {
                                                setErrors({ error: "Something went wrong. Please try again." })
                                            }
                                        }} 
                                    >
                                        {({  }) => (
                                            <Form>
                                                <MyDropdown name="status" options={StatusOptions} submitOnSelect />
                                            </Form>
                                        )}
                                    </Formik>

                                    <Formik
                                        initialValues={{ priority: selectedTicket!.priority, error: null }}
                                        onSubmit={async (values, { setErrors }) => {
                                            try {
                                                await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "priority", values.priority)
                                            } catch (error) {
                                                setErrors({ error: "Something went wrong. Please try again." })
                                            }
                                        }}
                                    >
                                        {({ }) => (
                                            <Form>
                                                <MyDropdown name="priority" options={PriorityOptions} submitOnSelect />
                                            </Form>
                                        )}
                                    </Formik>
                                
                                </Flex>

                                <Stack gap={4} width="100%" padding={4} border="1px solid #c8c8c8">
                                    <Grid templateColumns='1fr 2fr' width='100%' gap={4} alignItems='center'>
                                        <GridItem>
                                            <Heading size="xs">Assignee</Heading>
                                        </GridItem>

                                        <GridItem>
                                            <Formik
                                                initialValues={{ assignee: ticketStore.selectedTicket!.assignee, error: null }}
                                                onSubmit={async (values, { setErrors }) => {
                                                    try {
                                                        await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "assignee", values.assignee)
                                                    } catch (error) {
                                                        setErrors({ error: "Something went wrong. Please try again." })
                                                    }
                                                }}
                                            >
                                                {({ }) => (
                                                    <Form>
                                                        <UserDropdown name="assignee" options={projectStore.selectedProject!.users} allowNull submitOnSelect />
                                                    </Form>
                                                )}
                                            </Formik>
                                        </GridItem>
                                    </Grid>

                                    <Grid templateColumns='1fr 2fr' width='100%' gap={4} alignItems='center'>
                                        <GridItem>
                                            <Heading size="xs">Reporter</Heading>
                                        </GridItem>

                                        <GridItem>
                                            <Formik
                                                initialValues={{ author: ticketStore.selectedTicket!.author, error: null }}
                                                onSubmit={async (values, { setErrors }) => {
                                                    try {
                                                        await ticketStore.updateTicket(ticketStore.selectedTicket!.id, "author", values.author)
                                                    } catch (error) {
                                                        setErrors({ error: "Something went wrong. Please try again." })
                                                    }
                                                }}
                                            >
                                                {({ }) => (
                                                    <Form>
                                                        <UserDropdown name="author" options={projectStore.selectedProject!.users} submitOnSelect />
                                                    </Form>
                                                )}
                                            </Formik>
                                        </GridItem>
                                    </Grid>
                                </Stack>

                                <Text fontSize={{base: 'xs', md: 'sm'}} color="text.subtle">{`Created ${formattedDate}`}</Text>
                            </Stack>
                        </GridItem>
                    </Grid>
                </ModalBody>

                <ModalFooter >
                </ModalFooter>
            </ModalContent>
        </Modal>
    )
})