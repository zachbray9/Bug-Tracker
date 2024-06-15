import { Box, Slider, SliderFilledTrack, SliderThumb, SliderTrack } from "@chakra-ui/react"
import { RefObject, useState } from "react"
import AvatarEditor from "react-avatar-editor"

interface Props {
    image: any
    editorRef: RefObject<AvatarEditor>
}

export default function PhotoCropper({ image, editorRef }: Props) {
    const [scale, setScale] = useState(1);

    return (
        <Box>
            <AvatarEditor image={image} ref={editorRef} width={200} height={200} scale={scale} borderRadius={100} />

            <Slider value={scale} onChange={(value) => setScale(value)} min={1} max={3}>
                <SliderTrack>
                    <SliderFilledTrack />
                </SliderTrack>
                <SliderThumb />
            </Slider>
        </Box>
    )
}