import { useEffect, useState } from "react";


export function useGetAllAccounts() {

    const [accounts, setAccounts] = useState([]);

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, []);

    return [accounts];
}

export function useGetAccountsByUserId(userId: string) {

    const [accounts, setAccounts] = useState([]);

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, [userId]);

    return [accounts];
}


export function useGetAccountById(accountId: string | undefined) {

    const [account, setAccount] = useState({});

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, [accountId]);


    return [account];
}


export function useCreateAccount() {

    //return method from service
}