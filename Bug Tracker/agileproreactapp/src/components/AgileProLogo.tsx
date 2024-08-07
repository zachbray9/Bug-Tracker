import { Flex, Image, Text } from "@chakra-ui/react";

export default function () {
    return (
        <Flex justify='center' align='center' gap={1} >
            <Image src='https://agileproblobstorage.blob.core.windows.net/site-assets/AgileProLogo.png' alt='Logo' boxSize='45px' objectFit='contain'></Image>
            <Text fontSize={{base: '0px', md: 'xl'}} fontWeight='500' marginBottom='0px'>AgilePro</Text>
        </Flex>
    )
}