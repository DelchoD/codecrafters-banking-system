import { useParams } from "react-router-dom";
import { useGetAccountById } from "../../hooks/account-hooks";

export default function AccountEdit() {

    const { accountId } = useParams();
    const [account] = useGetAccountById(accountId);

    //Use the "useForm" hook


    return (
        <>
        </>
    )
}