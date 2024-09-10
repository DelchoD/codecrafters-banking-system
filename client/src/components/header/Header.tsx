
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

import { Link } from 'react-router-dom';

import { useAuthContext } from '../../contexts/AuthContext';



export default function Header() {

    const { isAuthenticated } = useAuthContext();

    return (
        <header>

            <div style={{ position: 'fixed', top: 10, right: 1, left: 1 }}>

                <Navbar expand="lg" className="bg-body-tertiary">

                    <Container>

                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="me-auto" style={{ display: 'flex', margin: '0 auto', gap: '3em' }}>

                                <Nav.Link as={Link} to="/">Home</Nav.Link>
                   
                                {isAuthenticated
                                    ? (
                                        <>

                                            <Nav.Link as={Link} to="/account/mine">My Accounts</Nav.Link>
                                            <Nav.Link as={Link} to="/transaction/mine">My Transactions</Nav.Link>
                                            <Nav.Link as={Link} to="/account/create">Create Account</Nav.Link>
                                      

                                            <Nav.Link as={Link} to="/profile">Manage Profile</Nav.Link>


                                            {/* <Nav.Link as={Link} to="/logout">Logout</Nav.Link> */}
                                        </>

                                    )
                                    : (
                                        <>
                                            <Nav.Link as={Link} to="/login">Login</Nav.Link>
                                            <Nav.Link as={Link} to="/register">Register</Nav.Link>

                                        </>

                                    )
                                }

                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>

            </div>
        </header>
    )
}