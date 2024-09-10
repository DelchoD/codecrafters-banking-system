import TransactionListItem from "../transaction-list-item/TransactionListItem";

import { useAuthContext } from "../../contexts/AuthContext";
import { useGetTransactionsByUserId } from "../../hooks/transaction-hooks";


export default function TransactionsMine() {

    const { id } = useAuthContext();
    const [transactions] = useGetTransactionsByUserId(id);


    //Put key for each generated transaction list item

    return (
        <>
            {transactions.map(transaction => <TransactionListItem
                transaction={transaction}
            />
            )}

        </>
    )
}