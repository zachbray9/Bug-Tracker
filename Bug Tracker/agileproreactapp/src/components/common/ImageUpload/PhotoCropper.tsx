import { Flex, IconButton, Slider, SliderFilledTrack, SliderThumb, SliderTrack, Stack } from "@chakra-ui/react"
import { RefObject, useState } from "react"
import AvatarEditor from "react-avatar-editor"
import { FaRegImage } from "react-icons/fa6"

interface Props {
    image: any
    editorRef: RefObject<AvatarEditor>
}

export default function PhotoCropper({ image, editorRef }: Props) {
    const [scale, setScale] = useState(1);
    const sliderFilledTrackBg = "#2c3e5d";

    return (
        <Stack gap={4}>
            <AvatarEditor image={image} ref={editorRef} width={200} height={200} scale={scale} borderRadius={100} />

            <Flex gap={4}>
                <IconButton aria-label="Zoom out" icon={<FaRegImage />} size="xs" fontSize="10px" onClick={() => setScale(1)} />

                <Slider value={scale} onChange={(value) => setScale(value)} min={1} max={3} step={0.01}>
                    <SliderTrack>
                        <SliderFilledTrack bg={sliderFilledTrackBg} />
                    </SliderTrack>
                    <SliderThumb bg={sliderFilledTrackBg} />
                </Slider>

                <IconButton aria-label="Zoom in" icon={<FaRegImage />} size="xs" fontSize="18px" onClick={() => setScale(3)} />
            </Flex>
        </Stack>
    )
}