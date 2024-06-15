import { Button, Center, Modal, ModalBody, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Stack, Text } from "@chakra-ui/react";
import PhotoDropzone from "./PhotoDropzone";
import { useEffect, useRef, useState } from "react";
import PhotoCropper from "./PhotoCropper";
import AvatarEditor from "react-avatar-editor";
import { useStore } from "../../../stores/store";
import { observer } from "mobx-react-lite";
import PhotoInputButton from "./PhotoInputButton";

interface Props {
    isOpen: boolean,
    onClose: () => void
}

export default observer(function PhotoUploadModal(props: Props) {
    const { userStore } = useStore();
    const [files, setFiles] = useState<any>([]);
    const editorRef = useRef<AvatarEditor>(null);

    const handleUploadPhoto = async () => {
        if (editorRef.current) {
            const canvas = editorRef.current.getImageScaledToCanvas();
            const croppedImageData = canvas.toDataURL();

            const blob = await fetch(croppedImageData).then((res) => res.blob())
            userStore.uploadPhoto(blob);
            setFiles([]);
            props.onClose();
        }
    }

    function cancelUpload() {
        props.onClose();
        setFiles([]);
    }

    useEffect(() => {
        return () => {
            files.forEach((file: any) => URL.revokeObjectURL(file.preview));
        }
    }, [files])

    return (
        <Modal isOpen={props.isOpen} onClose={props.onClose}>
            <ModalOverlay />
            <ModalContent>
                <ModalHeader>Add a profile photo</ModalHeader>
                <ModalBody as={Center}>
                    {
                        files[0] ? (
                            <PhotoCropper image={files[0].preview} editorRef={editorRef} />
                        ) : (
                                <Center as={Stack} gap={4}>
                                    <PhotoDropzone setFiles={setFiles} />
                                    <Text>or</Text>
                                    <PhotoInputButton setFiles={setFiles} />
                                </Center>
                        )
                    }
                </ModalBody>
                <ModalFooter gap={4}>
                    <Button variant="ghost" onClick={() => cancelUpload()} >Close</Button>
                    <Button colorScheme="messenger" onClick={() => handleUploadPhoto()} isLoading={userStore.isUploading} isDisabled={!files.length}>Upload</Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    )
})