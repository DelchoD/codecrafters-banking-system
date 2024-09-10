import { useState } from "react";

import { ControlledFormData } from "../types/ControlledFormData";



export function useForm(initialValues : any) : ControlledFormData {

    const [formValues, setFormValues] = useState(initialValues);


    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {

        e.preventDefault();

        setFormValues((oldValues: any) => ({
            ...oldValues,
            [e.target.name]: e.target.value
        }));
    };


    return {
        formValues,
        changeHandler
    };
}