import { HStack, Switch, Text, useColorMode } from "@chakra-ui/react"


const ColorModeSwitch = () => {
    const { toggleColorMode, colorMode } = useColorMode();

    return (
        <HStack>
            <Text margin='0px'>Dark Mode</Text>
            <Switch isChecked={colorMode === 'dark'} onChange={toggleColorMode} colorScheme='green' />
        </HStack>
    )
}

export default ColorModeSwitch