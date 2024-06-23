import { ChevronDownIcon } from "@chakra-ui/icons";
import { Button, FormControl, Menu, MenuButton, MenuItem, MenuList } from "@chakra-ui/react";
import { useField, useFormikContext } from "formik";
import { useState } from "react";

interface Props {
    name: string
    options: any[]
    currentSelection: any
}

export default function MyDropdown({ name, options, currentSelection }: Props) {
    const { setFieldValue } = useFormikContext();
    const [field, meta] = useField(name);
    const [selectedValue, setSelectedValue] = useState(currentSelection);

    const filteredOptions = options.filter(option => option !== selectedValue);

    const handleSelectionChange = (newSelection: string) => {
        setSelectedValue(newSelection);
        setFieldValue(name, newSelection);
    }

    return (
        <FormControl width="fit-content">
            <Menu>
                <MenuButton as={Button} rightIcon={<ChevronDownIcon />}>{selectedValue}</MenuButton>
                <MenuList>
                    {filteredOptions.map((option) => (
                        <MenuItem key={option} onClick={() => handleSelectionChange(option)} >{option}</MenuItem>
                    ))}
                </MenuList>
            </Menu>
        </FormControl>
    )
}