import { useEffect, useState } from "react";

export function useGetAllTransactions() {

    const [transactions, setTransactions] = useState([]);

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, []);

    return [transactions];
}


export function useGetTransactionsByUserId(userId: string) {

    const [transactions, setTransactions] = useState([]);

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, [userId]);

    return [transactions];
}

export function useGetTransactionById(transactionId: string | undefined) {

    const [transaction, setTransaction] = useState({});

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, [transactionId]);


    return [transaction];
}


export function useCreateTransaction() {
    
    //return method from service
}