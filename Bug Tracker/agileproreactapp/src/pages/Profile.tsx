import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Avatar, Button, Center, Stack, useDisclosure } from "@chakra-ui/react";
import PhotoUploadModal from "../components/common/ImageUpload/PhotoUploadModal";

export default observer(function Profile() {
    const { userStore } = useStore();
    const { isOpen, onOpen, onClose } = useDisclosure();

    return (
        <Center flexDir="column">
            <Avatar name={userStore.user?.firstName + " " + userStore.user?.lastName} src={userStore.user?.profilePictureUrl} />
            <PhotoUploadModal isOpen={isOpen} onClose={onClose} />
            <Button onClick={onOpen}>Open Modal</Button>
        </Center>
    )
})