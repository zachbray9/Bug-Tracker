import { Avatar, Button, Flex, Heading, Stack } from "@chakra-ui/react";
import { useStore } from "../../stores/store";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Form, Formik } from "formik";
import MyTextArea from "../common/form/MyTextArea";
import * as Yup from "yup";
import CommentContainer from "./CommentContainer";

interface Props {
    ticketId: string
}

export default observer(function TicketCommentSection({ ticketId }: Props) {
    const { userStore, commentStore } = useStore();

    useEffect(() => {
        if (ticketId) {
            commentStore.createHubConnection(ticketId);
        }

        return () => {
            commentStore.clearComments();
        }
    }, [commentStore, ticketId])

    const validationSchema = Yup.object({
        text: Yup.string().max(255, "Comment must be less than 255 characters.")
    })

    return (
        <Stack gap={4}>
            <Heading size="md">{commentStore.comments.length} Comments</Heading>

            <Formik
                initialValues={{ text: "" }}
                onSubmit={(values, { resetForm }) => commentStore.addComment(values).then(() => resetForm())}
                validationSchema={validationSchema}
            >
                {({ isSubmitting, values, dirty }) => (
                    <Form>
                        <Stack>
                            <Flex align="start" gap={2}>
                                <Avatar name={`${userStore.user!.firstName} ${userStore.user!.lastName}`} src={userStore.user!.profilePictureUrl} size="sm" />
                                <MyTextArea name="text" placeholder="Add a comment..." initialValue={values.text} resize="none" overflow="hidden" whiteSpace="pre-wrap" />
                            </Flex>
                            <Button type="submit" isLoading={isSubmitting} isDisabled={!dirty} alignSelf="end">Comment</Button>
                        </Stack>
                    </Form>
                )}
            </Formik>

            <Stack gap={6}>
                {commentStore.comments.map(comment => (
                    <CommentContainer key={comment.id} comment={comment} />
                ))}
            </Stack>
        </Stack>
    )
})