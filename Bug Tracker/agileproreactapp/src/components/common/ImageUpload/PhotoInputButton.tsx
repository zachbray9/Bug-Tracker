import { Button, Input } from "@chakra-ui/react"
import { useCallback } from "react"

interface Props {
    setFiles: any
}

export default function PhotoInputButton({ setFiles }: Props) {
    const handleFileChange = useCallback((event: any) => {
        const files = Array.from(event.target.files);
        setFiles(files.map((file: any) => Object.assign(file, {
            preview: URL.createObjectURL(file)
        })))
    }, [setFiles])

    return (
        <>
            <Input id="file-input" type="file" accept="image/*" onChange={handleFileChange} style={{ display: "none" }} />
            <Button as="label" htmlFor="file-input" variant="outline" colorScheme="messenger" cursor="pointer">Upload a photo</Button>
        </>
     )
}