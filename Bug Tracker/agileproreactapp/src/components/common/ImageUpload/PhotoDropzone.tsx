import { Center, Icon, Text } from '@chakra-ui/react'
import { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import { BsCloudUploadFill } from 'react-icons/bs'
interface Props {
    setFiles: any
}

export default function PhotoDropzone({ setFiles }: Props) {
    const onDrop = useCallback((acceptedFiles: any) => {
        setFiles(acceptedFiles.map((file: any) => Object.assign(file, {
            preview: URL.createObjectURL(file)
        })))
    }, [setFiles])

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <Center {...getRootProps()} w="200px" h="200px" p={4} flexDirection="column" borderWidth="2px" borderStyle="dashed" borderColor={isDragActive ? "blue" : "gray.400"} borderRadius="50%">
            <input {...getInputProps()} />
            <Icon as={BsCloudUploadFill} w={8} h={8} />
            <Text textAlign="center">Drag and drop your images here</Text>
        </Center>
    )
}