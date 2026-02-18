import GetBalance from "../hooks/GetBalance"

const Transaction = () => {
    const balance = GetBalance()

    return (
        <div>
            <h1>Create Transaction:</h1>
            <h2>Your balance: {balance}</h2>
            <label>
                Recipients Address: <input name="recipient" />
            </label>
        </div>
    )
}

export default Transaction;