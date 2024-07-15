import { ChevronDownIcon } from "@chakra-ui/icons";
import { Button, FormControl, FormLabel, Menu, MenuButton, MenuItem, MenuList } from "@chakra-ui/react";
import { useField, useFormikContext } from "formik";

interface Props {
    name: string
    options: any[]
    label?: string
    submitOnSelect?: boolean
}

export default function MyDropdown({ name, options, label, submitOnSelect }: Props) {
    const { setFieldValue, submitForm } = useFormikContext();
    const [field, meta] = useField(name);

    const filteredOptions = options.filter(option => option !== field.value);

    const handleSelectionChange = (newSelection: string) => {
        setFieldValue(name, newSelection);
        if (submitOnSelect) {
            submitForm();
        }
    }

    return (
        <FormControl width="fit-content" isInvalid={meta.error ? true : false}>
            {label &&
                <FormLabel htmlFor={`menu-button-${name}`} >{label}</FormLabel>
            }

            <Menu id={name}>
                <MenuButton id={name} as={Button} rightIcon={<ChevronDownIcon />} >{field.value}</MenuButton>
                <MenuList>
                    {filteredOptions.map((option) => (
                        <MenuItem key={option} onClick={() => handleSelectionChange(option)} >{option}</MenuItem>
                    ))}
                </MenuList>
            </Menu>
        </FormControl>
    )
}