import { Form, Formik } from "formik";
import { useStore } from "../../stores/store";
import * as Yup from "yup"
import { Button, Center, Stack, Text } from "@chakra-ui/react";
import MyTextInput from "../common/form/MyTextInput";
import MyTextArea from "../common/form/MyTextArea";

export default function CreateProjectForm() {
    const { projectStore } = useStore();

    const validationSchema = Yup.object({
        name: Yup.string().required("Title field is required."),
        description: Yup.string().required("Description field is required.")
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
                        <MyTextInput name="name" placeholder="Title" label="Title" />
                        <MyTextArea name="description" placeholder="Add a description" label="Description" />
                        {errors.error && <Text color="red">{errors.error}</Text>}
                        <Center>
                            <Button type='submit' isLoading={isSubmitting} colorScheme="messenger" w="100%">Create project</Button>
                        </Center>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}