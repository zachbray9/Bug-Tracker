import { Button, Center, Modal, ModalBody, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/react";
import PhotoDropzone from "./PhotoDropzone";
import { useEffect, useRef, useState } from "react";
import PhotoCropper from "./PhotoCropper";
import AvatarEditor from "react-avatar-editor";

interface Props {
    isOpen: boolean,
    onClose: () => void
}

export default function PhotoUploadModal(props: Props) {
    const [files, setFiles] = useState<any>([]);
    const editorRef = useRef<AvatarEditor>(null);

    function onCrop() {
        if (editorRef.current) {
            editorRef.current.getImageScaledToCanvas().toBlob(blob => console.log(blob));
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
                        ): (
                            <PhotoDropzone setFiles={setFiles} />
                        )
                    }
                </ModalBody>
                <ModalFooter gap={4}>
                    <Button variant="ghost" onClick={() => cancelUpload()} >Close</Button>
                    <Button colorScheme="messenger" onClick={() => onCrop()} disabled={files[0]}>Upload</Button>
                </ModalFooter>
            </ModalContent>
        </Modal>
    )
}