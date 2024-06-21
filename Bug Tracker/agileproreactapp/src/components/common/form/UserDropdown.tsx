import { Avatar, Flex, FormControl, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
import { ProjectParticipant } from "../../../models/ProjectParticipant";
import { useFormikContext } from "formik";
import { useState } from "react";

interface Props {
    name: string
    options: ProjectParticipant[]
    currentSelection: ProjectParticipant | null
    allowNull?: boolean
}

export default function UserDropdown({ name, options, currentSelection, allowNull }: Props) {
    const { setFieldValue } = useFormikContext();
    const [selectedValue, setSelectedValue] = useState(currentSelection || null);

    const filteredOptions = options.filter(option => option !== selectedValue);

    const handleSelectionChange = (option: ProjectParticipant | null) => {
        setSelectedValue(option);
        setFieldValue(name, option);
    }

    return (
        <FormControl>
            <Menu>
                <MenuButton>
                    <Flex align="center" gap={4}>
                        <Avatar name={selectedValue ? `${selectedValue.firstName} ${selectedValue.lastName}` : undefined} src={selectedValue?.profilePictureUrl} size="sm" />
                        <Text>{selectedValue ? `${selectedValue.firstName} ${selectedValue.lastName}` : "Unassigned"}</Text>
                    </Flex>
                </MenuButton>

                <MenuList>
                    {filteredOptions.map((option) => (
                        <MenuItem onClick={() => handleSelectionChange(option)}>
                            <Flex align="center" gap={4}>
                                <Avatar name={ `${option.firstName} ${option.lastName}` } src={option.profilePictureUrl} size="sm" />
                                <Text>{`${option.firstName} ${option.lastName}`}</Text>
                            </Flex>
                        </MenuItem>
                    ))}
                    {allowNull && (
                        <MenuItem onClick={() => handleSelectionChange(null)}>
                            <Flex align="center" gap={4}>
                                <Avatar size="sm" />
                                <Text>Unassigned</Text>
                            </Flex>
                        </MenuItem>
                    )}
                </MenuList>
            </Menu>
        </FormControl>
    )
}