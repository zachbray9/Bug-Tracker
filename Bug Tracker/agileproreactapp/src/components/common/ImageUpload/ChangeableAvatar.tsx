import { Avatar, Box, IconButton, useDisclosure } from "@chakra-ui/react";
import PhotoUploadModal from "./PhotoUploadModal";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store";
import { FaCamera } from "react-icons/fa";
import { useState } from "react";

export default observer(function ChangeableAvatar() {
    const { userStore } = useStore();
    const { isOpen, onOpen, onClose } = useDisclosure();
    const [isHovered, setIsHovered] = useState(false);
    const [ isVisible, setIsVisible ] = useState(false);

    const closeModal = () => {
        onClose();
        setIsHovered(false);
        setIsVisible(false);
    }

    return (
        <Box
            position="relative"
            display="inline-block"
            borderRadius="50%"
            onMouseEnter={() => {
                setIsVisible(true)
                setIsHovered(true)
            }}
            onMouseLeave={() => {
                setIsVisible(false)
                setIsHovered(false)
            }}
        >
            <Avatar
                name={`${userStore.user!.firstName} ${userStore.user!.lastName}`}
                src={userStore.user!.profilePictureUrl || undefined}
                key={`${userStore.user!.firstName} ${userStore.user!.lastName}`}
                size="2xl"
                filter={isHovered ? "brightness(0.8)" : "brighness(1)"}
                transition="all 0.2s ease-in-out"
                
            />
           
            <IconButton
                aria-label="change picture"
                icon={<FaCamera color="white" fontSize="18px" />}
                onClick={onOpen}
                variant="ghost"
                colorScheme="blackAlpha"
                size="md"
                position="absolute"
                top="50%"
                left="50%"
                transform="translate(-50%, -50%)"
                opacity={isVisible ? 1 : 0}
                transition="all 0.2s ease-in-out"
            />


            <PhotoUploadModal isOpen={isOpen} onClose={closeModal} />
        </Box>
    )
})