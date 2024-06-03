import { FormControl, FormErrorMessage, FormLabel, Textarea } from "@chakra-ui/react";
import { useField } from "formik";

interface Props {
    name: string,
    label: string,
    placeholder: string
}

export default function MyTextArea(props: Props) {
    const [field, meta] = useField(props.name);

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            <FormLabel htmlFor={props.name}>{props.label}</FormLabel>
            <Textarea {...field} id={props.name} placeholder={props.placeholder} resize="vertical" />

            {meta.touched && meta.error && (
                <FormErrorMessage>{meta.error}</FormErrorMessage>
            )}
        </FormControl>
    )
}