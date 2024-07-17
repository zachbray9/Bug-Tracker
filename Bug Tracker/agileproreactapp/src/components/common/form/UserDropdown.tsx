import { Avatar, Button, Flex, FormControl, Menu, MenuButton, MenuItem, MenuList, Text } from "@chakra-ui/react";
import { ProjectParticipant } from "../../../models/ProjectParticipant";
import { useField, useFormikContext } from "formik";
import { useEffect } from "react";

interface Props {
    name: string
    options: ProjectParticipant[]
    allowNull?: boolean
    submitOnSelect?: boolean
}

export default function UserDropdown({ name, options, allowNull, submitOnSelect }: Props) {
    const [field, meta] = useField(name);
    const { setFieldValue, submitForm } = useFormikContext();

    const filteredOptions = options.filter(option => option.userId !== field.value?.userId);

    const handleSelectionChange = (option: ProjectParticipant | null) => {
        setFieldValue(name, option);
        if (submitOnSelect) {
            submitForm();
        }
    }

    useEffect(() => {
        console.log('field value:', field.value);
    }, [field.value]);

    return (
        <FormControl width="fit-content" isInvalid={meta.error ? true : false}>
            <Menu>
                <MenuButton as={Button} variant="unstyled">
                    {field.value ? (
                        <Flex align="center" gap={4}>
                            <Avatar size="sm" name={`${field.value.firstName} ${field.value.lastName}`} src={field.value.profilePictureUrl ? field.value.profilePictureUrl : undefined} />
                            <Text>{`${field.value.firstName} ${field.value.lastName}`}</Text>
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
                                <Avatar name={`${option.firstName} ${option.lastName}`} src={option.profilePictureUrl || undefined} size="sm" />
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