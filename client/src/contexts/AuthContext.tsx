import { createContext, useContext, useState } from "react";
import { AuthData } from "../types/AuthData";


const AuthContext = createContext({
    id: '',
    email: '',
    isAuthenticated: false
});


export function AuthContextProvider(props: any) {

    const [authData, setAuthData] = useState({
        id: '',
        email: '',
        isAuthenticated: false,
    });

    const changeAuthData = (newData: AuthData) => {

        //Make validations if needed

        setAuthData(newData);
    };


    const dataObj = {

        id: authData.id,
        email: authData.email,
        isAuthenticated: !!authData.email,
        changeAuthData
    };


    return (

        <AuthContext.Provider value={dataObj}>
            {props.children}
        </AuthContext.Provider>

    )

}

export function useAuthContext() {
    return useContext(AuthContext);
}