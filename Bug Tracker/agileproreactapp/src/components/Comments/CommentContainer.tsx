import { Avatar, Flex, Stack, Text } from "@chakra-ui/react"
import { Comment } from "../../models/Comment"
import { format } from "date-fns"

interface Props {
    comment: Comment
}

export default function CommentContainer({ comment }: Props) {
    const formattedDate = format(new Date(comment.dateSubmitted), "MMMM d, yyyy 'at' h:mm a")

    return (
        <Flex gap={2} align="start">
            <Avatar name={`${comment.author.firstName} ${comment.author.lastName}`} src={comment.author.profilePictureUrl} size="sm" />
            <Stack>
                <Flex gap={4}>
                    <Text>{`${comment.author.firstName} ${comment.author.lastName}`}</Text>
                    <Text>{formattedDate}</Text>
                </Flex>
                <Text>{comment.text}</Text>
            </Stack>
        </Flex>
    )
}