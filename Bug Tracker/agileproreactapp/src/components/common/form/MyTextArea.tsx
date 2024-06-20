import { FormControl, FormErrorMessage, FormLabel, Textarea, TextareaProps } from "@chakra-ui/react";
import { useField, useFormikContext } from "formik";
import { ChangeEvent, useState } from "react";

interface Props extends TextareaProps{
    name: string,
    label?: string,
    initialValue?: string
}

export default function MyTextArea({ name, label, initialValue, ...props }: Props) {
    const [field, meta] = useField(name);
    const {setFieldValue} = useFormikContext();
    const [value, setValue] = useState(initialValue);

    const handleInputChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        let inputValue = e.target.value;
        setValue(inputValue);
        setFieldValue(name, inputValue);
    }

    return (
        <FormControl isInvalid={meta.touched && !!meta.error}>
            {label &&
                < FormLabel htmlFor={name}>{label}</FormLabel>
            }

            <Textarea
                {...field}
                {...props}
                id={name}
                value={value}
                placeholder={props.placeholder}
                onChange={handleInputChange}
            />
                
            

            {meta.touched && meta.error && (
                <FormErrorMessage>{meta.error}</FormErrorMessage>
            )}
        </FormControl>
    )
}