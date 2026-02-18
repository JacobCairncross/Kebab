import GetKey from "../hooks/GetKey";
import GetBalance from "../hooks/GetBalance";

const Wallet = () => {
    const pubKey = GetKey()
    const balance = GetBalance()

    return (
        <div>
            <h1>Wallet</h1>
            <h2>Your Public Key: {pubKey}</h2>
            <h2>Your Balance: {balance}</h2>
        </div>
    )
}

export default Wallet;