import { Button, FormControl, FormErrorMessage, FormLabel, Icon, Input, InputGroup, InputLeftElement, InputProps, InputRightElement } from "@chakra-ui/react";
import { useField, useFormikContext } from "formik";
import { ChangeEvent, useState } from "react";
import { IconType } from "react-icons";

interface Props extends InputProps {
    name: string;
    label?: string;
    leftIcon?: IconType,
    rightIcon?: IconType,
    hideable?: boolean
}

export default function MyTextInput({ name, label, leftIcon, rightIcon, hideable, ...props }: Props) {
    const { setFieldValue } = useFormikContext();
    const [field, meta] = useField(name);
    const [show, setShow] = useState(false);

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        let inputValue = e.target.value;
        setFieldValue(name, inputValue)
    }

    const toggleVisibility = () => { 
        if (show === false)
            setShow(true);
        else
            setShow(false);
    }

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            <FormLabel htmlFor={name}>{label}</FormLabel>
            <InputGroup alignItems='center'>
                {leftIcon && <InputLeftElement color="#d3d3d3">
                    <Icon as={leftIcon} fontSize='1em'></Icon>
                </InputLeftElement> }

                <Input
                    {...field}
                    {...props}
                    id={name}
                    type={hideable ? (show ? "text" : "password") : "text"}
                    value={field.value}
                    onChange={handleInputChange}
                />

                {/*If input is Hideable, provides a show/hide button. If not, displays a right icon if there is one*/}
                {hideable ? (
                    <InputRightElement color="#d3d3d3" w="4.5rem">
                        <Button onClick={toggleVisibility} size={{base: 'xs', md: 'sm'}} >
                            {show ? "Hide" : "Show" }
                        </Button>
                    </InputRightElement>
                ) : (
                        rightIcon && (
                            <InputRightElement>
                                <Icon as={rightIcon} fontSize='1em'></Icon>
                            </InputRightElement>
                        )
                )}
            </InputGroup>

            {meta.touched && meta.error && (
                <FormErrorMessage>{meta.error}</FormErrorMessage>
            )}
        </FormControl>
    )
}