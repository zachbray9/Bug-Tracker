import { Button, FormControl, FormErrorMessage, FormLabel, Icon, Input, InputGroup, InputLeftElement, InputRightElement } from "@chakra-ui/react";
import { useField } from "formik";
import { useState } from "react";
import { IconType } from "react-icons";

interface Props {
    placeholder: string;
    name: string;
    label?: string;
    leftIcon?: IconType,
    rightIcon?: IconType,
    hideable?: boolean
}

export default function MyTextInput(props: Props) {
    const [field, meta] = useField(props.name);
    const [show, setShow] = useState(false);

    const toggleVisibility = () => { 
        if (show === false)
            setShow(true);
        else
            setShow(false);
    }

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            <FormLabel htmlFor={props.name}>{props.label}</FormLabel>
            <InputGroup>
                {props.leftIcon && <InputLeftElement color="#d3d3d3">
                    <Icon as={props.leftIcon}></Icon>
                </InputLeftElement> }

                <Input {...field} id={props.name} type={props.hideable ? (show ? "text" : "password") : "text"} placeholder={props.placeholder} />

                {/*If input is Hideable, provides a show/hide button. If not, displays a right icon if there is one*/}
                {props.hideable ? (
                    <InputRightElement color="#d3d3d3" w="4.5rem">
                        <Button onClick={toggleVisibility} size="sm">
                            {show ? "Hide" : "Show" }
                        </Button>
                    </InputRightElement>
                ) : (
                        props.rightIcon && (
                            <InputRightElement>
                                <Icon as={props.rightIcon}></Icon>
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