import { FormControl, FormErrorMessage, FormLabel, Textarea, TextareaProps } from "@chakra-ui/react";
import { useField, useFormikContext } from "formik";
import { ChangeEvent, useEffect, useRef } from "react";

interface Props extends TextareaProps{
    name: string,
    label?: string,
    initialValue?: string
}

export default function MyTextArea({ name, label, initialValue, ...props }: Props) {
    const [field, meta] = useField(name);
    const {setFieldValue} = useFormikContext();
    const textAreaRef = useRef<HTMLTextAreaElement>(null);

    useEffect(() => {
        if (textAreaRef.current) {
            textAreaRef.current.style.height = "auto";
            textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
        }
    }, [textAreaRef]);

    const handleInputChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        let inputValue = e.target.value;
        setFieldValue(name, inputValue);

        if (textAreaRef.current) {
            textAreaRef.current.style.height = "auto";
            textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
        }
    }

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            {label &&
                < FormLabel htmlFor={name}>{label}</FormLabel>
            }

            <Textarea
                {...field}
                {...props}
                ref={textAreaRef}
                id={name}
                value={field.value}
                placeholder={props.placeholder}
                onChange={handleInputChange}
            />
                
            

            {meta.touched && meta.error && (
                <FormErrorMessage>{meta.error}</FormErrorMessage>
            )}
        </FormControl>
    )
}