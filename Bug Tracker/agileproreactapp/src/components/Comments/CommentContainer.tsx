import { Avatar, Flex, Stack, Text } from "@chakra-ui/react"
import { Comment } from "../../models/Comment"
import { formatDistanceToNow } from "date-fns"

interface Props {
    comment: Comment
}

export default function CommentContainer({ comment }: Props) {
    const formattedDate = formatDistanceToNow(comment.dateSubmitted);

    return (
        <Flex gap={2} align="start">
            <Avatar name={`${comment.author.firstName} ${comment.author.lastName}`} src={comment.author.profilePictureUrl ? comment.author.profilePictureUrl : undefined} size="sm" />
            <Stack>
                <Flex gap={4}>
                    <Text as="b">{`${comment.author.firstName} ${comment.author.lastName}`}</Text>
                    <Text fontSize="sm" color="#44546f">{`${formattedDate} ago`}</Text>
                </Flex>
                <Text>{comment.text}</Text>
            </Stack>
        </Flex>
    )
}