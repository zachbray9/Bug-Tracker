import { RefObject } from "react"
import AvatarEditor from "react-avatar-editor"

interface Props {
    image: any
    editorRef: RefObject<AvatarEditor>
}

export default function PhotoCropper({ image, editorRef }: Props) {
    return (
        <AvatarEditor image={image} ref={editorRef} width={200} height={200} scale={1.2} borderRadius={100} />
    )
}