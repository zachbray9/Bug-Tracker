import { Avatar, Button, Flex, FormControl, Menu, MenuButton, MenuItem, MenuList, Spinner, Text } from "@chakra-ui/react";
import { ProjectParticipant } from "../../../models/ProjectParticipant";
import { useField, useFormikContext } from "formik";

interface Props {
    name: string
    options: ProjectParticipant[]
    allowNull?: boolean
    submitOnSelect?: boolean
}

export default function UserDropdown({ name, options, allowNull, submitOnSelect }: Props) {
    const [field, meta] = useField(name);
    const { setFieldValue, submitForm, isSubmitting } = useFormikContext();

    const filteredOptions = options.filter(option => option.userId !== field.value?.userId);

    const handleSelectionChange = (option: ProjectParticipant | null) => {
        setFieldValue(name, option);
        if (submitOnSelect) {
            submitForm();
        }
    }

    return (
        <FormControl width="fit-content" isInvalid={meta.error ? true : false}>
            <Menu>
                <MenuButton as={Button} variant="unstyled" isDisabled={isSubmitting}>
                    
                    <Flex align="center" gap={4}>
                        <Avatar
                            size="sm"
                            key={field.value ? `${field.value.firstName} ${field.value.lastName}` : "unassigned"}
                            name={field.value ? `${field.value.firstName} ${field.value.lastName}` : undefined}
                            src={field.value?.profilePictureUrl || undefined}
                        />
                        <Text noOfLines={1} fontSize={{ base: 'sm', md: 'md' }}>{field.value ? `${field.value.firstName} ${field.value.lastName}` : "Unassigned"}</Text>
                        {isSubmitting && <Spinner size="sm" />}
                    </Flex>
                    
                </MenuButton>

                <MenuList>
                    {filteredOptions.map((option) => (
                        <MenuItem key={option.email} onClick={() => handleSelectionChange(option)}>
                            <Flex align="center" gap={4}>
                                <Avatar name={`${option.firstName} ${option.lastName}`} src={option.profilePictureUrl || undefined} key={`${option.firstName} ${option.lastName}`} size="sm" />
                                <Text noOfLines={1} fontSize={{ base: 'sm', md: 'md' }}>{`${option.firstName} ${option.lastName}`}</Text>
                            </Flex>
                        </MenuItem>
                    ))}
                    {allowNull && field.value && (
                        <MenuItem key="Unassigned" onClick={() => handleSelectionChange(null)}>
                            <Flex align="center" gap={4}>
                                <Avatar size="sm" key="unassigned" />
                                <Text>Unassigned</Text>
                            </Flex>
                        </MenuItem>
                    )}
                </MenuList>
            </Menu>
        </FormControl>
    )
}