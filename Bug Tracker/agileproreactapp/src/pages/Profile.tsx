import { Center } from "@chakra-ui/react";
import ChangeableAvatar from "../components/common/ImageUpload/ChangeableAvatar";

export default function Profile() {

    return (
        <Center flexDir="column">
            <ChangeableAvatar />
        </Center>
    )
}