import { useState } from "react";
import GetBalance from "../hooks/GetBalance"
import SendTransaction from "../utils/SendTransaction";

const Transaction = () => {
    const [status, setStatus] = useState('presend') // presend, sent, error
    const balance = GetBalance()
    return (
        <div>
            <h1>{status}</h1>
            <h1>Create Transaction:</h1>
            <h2>Your balance: {balance}</h2>
            <form action={handleSubmit}>
                <label>
                    Recipients Address: <input name="recipient"/>
                </label>
                <label>
                    Amount: <input name="amount" />
                </label>
               <button type="submit">Submit</button>
            </form>
        </div>
    )
    async function handleSubmit(formData: FormData) {
      const recipient = formData.get("recipient") as string
      const amount = formData.get("amount") as string
    
      const result = await SendTransaction(recipient, parseInt(amount))
      if(result == 'Successful')
      {
        setStatus('sent')
      }
      else if(result == 'UnSuccessful')
      {
        setStatus('error')
      }
    }
}

export default Transaction;