import { FC, useState } from "react";
import GetBalance from "../hooks/GetBalance"
import SendTransaction from "../utils/SendTransaction";
import PopUp, { PopUpProps, PopupType } from "../components/PopUp";
import { PageProps } from "../App";

enum Status {
    Presend = "Pre-Send",
    Sent = "Sent",
    Error = "Error"
}

const Transaction:FC<PageProps> = ({AddPopUp}) => {
    const [status, setStatus] = useState(Status.Presend)
    const balance = GetBalance()
    return (
        <>
            <div>
                {/* <h1>Status: {status}</h1> */}
                <h1>Create Transaction:</h1>
                <h2>Your balance: {balance}</h2>
                <form className="transaction-form" action={handleSubmit}>
                    <label className="transaction-form-input">
                        Recipients Address: <input name="recipient"/>
                    </label>
                    <label className="transaction-form-input">
                        Amount: <input name="amount" />
                    </label>
                <button type="submit">Submit</button>
                </form>
            </div>
        </>
    )
    async function handleSubmit(formData: FormData) {
      const recipient = formData.get("recipient") as string
      const amount = formData.get("amount") as string
    
      const result = await SendTransaction(recipient, parseInt(amount))
      if(result == 'Successful')
      {
        AddPopUp("Sent successfuly", PopupType.Success)
      }
      else if(result == 'UnSuccessful')
      {
        AddPopUp("Transaction Failed", PopupType.Error)
      }
    }
}

export default Transaction;