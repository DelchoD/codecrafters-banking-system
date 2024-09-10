import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import styles from './Register.module.css';

import { useForm } from "../../hooks/useForm";


const initialValues = {
    email: '',
    password: '',
    rePassword: ''
};

export default function Register() {

    const { formValues, changeHandler } = useForm(initialValues);


    return (

        <>

            <h2 className={styles['register-title']}>Register</h2>
            <Form className={styles['register-form']}>

                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label className={styles['register-label']}>Email address</Form.Label>
                    <Form.Control value={formValues.email} onChange={changeHandler} name="email" type="email" placeholder="Enter email" />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label className={styles['register-label']}>Password</Form.Label>
                    <Form.Control value={formValues.password} onChange={changeHandler} name="password" type="password" placeholder="Password" />
                </Form.Group>


                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label className={styles['register-label']}>Confirm Password</Form.Label>
                    <Form.Control value={formValues.rePassword} onChange={changeHandler} name="rePassword" type="password" placeholder="Password" />
                </Form.Group>


                <Button className={styles['register-btn']} variant="primary" type="submit">
                    Register
                </Button>
            </Form >

        </>
    )

}


