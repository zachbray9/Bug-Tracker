import { FormControl, FormErrorMessage, FormLabel, Textarea, TextareaProps } from "@chakra-ui/react";
import { useField } from "formik";

interface Props extends TextareaProps{
    name: string,
    label?: string,
    initialValue?: string
}

export default function MyTextArea({ name, label, initialValue, ...props }: Props) {
    const [field, meta] = useField(name);

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            {label &&
                < FormLabel htmlFor={name}>{label}</FormLabel>
            }

            <Textarea
                {...field}
                {...props}
                id={name}
                placeholder={props.placeholder}
            >
                {initialValue}
            </Textarea>

            {meta.touched && meta.error && (
                <FormErrorMessage>{meta.error}</FormErrorMessage>
            )}
        </FormControl>
    )
}