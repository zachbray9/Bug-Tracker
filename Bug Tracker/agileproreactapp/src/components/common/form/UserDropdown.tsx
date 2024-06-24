import { Avatar, Button, Flex, FormControl, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
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
        <FormControl width="fit-content" >
            <Menu>
                <MenuButton as={Button} variant="unstyled">
                    {selectedValue ? (
                        <Flex align="center" gap={4}>
                            <Avatar name={`${selectedValue.firstName} ${selectedValue.lastName}`} src={selectedValue.profilePictureUrl} size="sm" />
                            <Text>{`${selectedValue.firstName} ${selectedValue.lastName}`}</Text>
                        </Flex>
                    ): (
                            <Flex align="center" gap={4}>
                                <Avatar size="sm" bg="gray.400" />
                                <Text>Unassigned</Text>
                            </Flex>
                    )}
                </MenuButton>

                <MenuList>
                    {filteredOptions.map((option) => (
                        <MenuItem key={option.email} onClick={() => handleSelectionChange(option)}>
                            <Flex align="center" gap={4}>
                                <Avatar name={ `${option.firstName} ${option.lastName}` } src={option.profilePictureUrl} size="sm" />
                                <Text>{`${option.firstName} ${option.lastName}`}</Text>
                            </Flex>
                        </MenuItem>
                    ))}
                    {allowNull && (
                        <MenuItem key="Unassigned" onClick={() => handleSelectionChange(null)}>
                            <Flex align="center" gap={4}>
                                <Avatar size="sm" bg="gray.400" />
                                <Text>Unassigned</Text>
                            </Flex>
                        </MenuItem>
                    )}
                </MenuList>
            </Menu>
        </FormControl>
    )
}