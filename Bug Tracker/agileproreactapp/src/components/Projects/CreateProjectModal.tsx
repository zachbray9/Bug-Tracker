import { Modal, ModalBody, ModalContent, ModalFooter, ModalHeader, ModalOverlay} from "@chakra-ui/react";
import CreateProjectForm from "./CreateProjectForm";


//Use the useDisclosure hook from Chakra and pass isOpen and onClose as props. Use on open on whatever button opens the modal.
interface Props {
    isOpen: boolean,
    onClose: () => void
}

export default function CreateProjectModal(props: Props) {
    return (
        <Modal isOpen={props.isOpen} onClose={props.onClose} isCentered scrollBehavior="inside">
            <ModalOverlay />
            <ModalContent>
                <ModalHeader>Create a project</ModalHeader>
                <ModalBody>
                    <CreateProjectForm />
                </ModalBody>
                <ModalFooter></ModalFooter>
            </ModalContent>
        </Modal>
    )
}