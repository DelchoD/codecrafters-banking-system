import AccountListItem from "../account-list-item/AccountListItem";

import { useAuthContext } from "../../contexts/AuthContext";
import { useGetAccountsByUserId } from "../../hooks/account-hooks"

export default function AccountsMine() {

    const { id } = useAuthContext();
    const [accounts] = useGetAccountsByUserId(id);


    //Put key for each generated account list item

    return (
        <>
            {accounts.map(account => <AccountListItem
                account={account}
            />
            )}

        </>
    )
}