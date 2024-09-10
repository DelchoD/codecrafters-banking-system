import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import styles from './Login.module.css';

import { useForm } from '../../hooks/useForm';


const initialValues = {
    email: '',
    password: ''
};


export default function Login() {

    const { formValues, changeHandler } = useForm(initialValues);

    return (

        <>
            <h2 className={styles['login-title']}>Login</h2>
            <Form className={styles['login-form']}>

                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label className={styles['form-label']}>Email address</Form.Label>
                    <Form.Control type="text" value={formValues.email} name="email" onChange={changeHandler} placeholder="Enter email" />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label className={styles['form-label']}>Password</Form.Label>
                    <Form.Control value={formValues.password} name="password" onChange={changeHandler} type="password" placeholder="Password" />
                </Form.Group>

                <Button className={styles['login-btn']} variant="primary" type="submit">
                    Login
                </Button>
            </Form>

        </>
    )
}