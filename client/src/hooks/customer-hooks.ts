import { useEffect, useState } from "react";


export function useGetAllCustomers() {

    const [customers, setCustomers] = useState([]);

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, []);

    return [customers];
}

export function useGetCustomerById(customerId: string) {

    const [customer, setCustomer] = useState({});

    useEffect(() => {

        //Use service to make request to API and set data to state

    }, [customerId]);


    return [customer];
}


export function useCreateCustomer() {
    
    //return method from service
}