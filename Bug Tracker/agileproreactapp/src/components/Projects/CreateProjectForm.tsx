import { Form, Formik } from "formik";
import { useStore } from "../../stores/store";
import * as Yup from "yup"
import { Button, Center, Stack, Text } from "@chakra-ui/react";
import MyTextInput from "../common/form/MyTextInput";
import MyTextArea from "../common/form/MyTextArea";

export default function CreateProjectForm() {
    const { projectStore } = useStore();

    const validationSchema = Yup.object({
        name: Yup.string().required("Title field is required.").max(255, "Title cannot exceed 255 characters."),
        description: Yup.string().max(10000, 'Description cannot exceed 10,000 characters.')
    });

    return (
        <Formik
            initialValues={{ name: '', description: '', error: null }}
            onSubmit={(values, { setErrors }) => projectStore.createProject(values).catch(() => setErrors({ error: "Something went wrong, please try again." }))}
            validationSchema={validationSchema}

        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form onSubmit={handleSubmit} autoComplete="off">
                    <Stack spacing={8} >
                        <MyTextInput name="name" placeholder="Title" label="Title" size={{base: 'sm', md: 'md'}} />
                        <MyTextArea name="description" placeholder="Add a description" label="Description" size={{base: 'sm', md: 'md'}} />
                        {errors.error && <Text color="red">{errors.error}</Text>}
                        <Center>
                            <Button type='submit' isLoading={isSubmitting} colorScheme="messenger" w="100%" size={{base: 'sm', md: 'md'} }>Create project</Button>
                        </Center>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}