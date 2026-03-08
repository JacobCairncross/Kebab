import GetKey from "../hooks/GetKey";
import GetBalance from "../hooks/GetBalance";
import formatNumber from "../utils/FormatNumber";
import KebabIcon from '../assets/skewer-kebab-svgrepo-com.svg'
import EyeOpen from '../assets/eye-svgrepo-com.svg'
import EyeClosed from '../assets/eye-off-svgrepo-com.svg'
import Copy from '../assets/copy-document-svgrepo-com.svg'
import { useState } from "react";

const Wallet = () => {
    const [showKey, setShowKey] = useState(false)
    const pubKey = GetKey()
    const balance = formatNumber(parseInt(GetBalance()))

    return (
        <div>
            <h1>Wallet</h1>
            <div className="pub-key-container">
                <p>Your Public Key:</p> 
                <div className="pub-key-box">
                    <img 
                        className="copy-button" 
                        src={Copy}
                        onClick={() => {navigator.clipboard.writeText(pubKey)}}
                        />
                    <p>{showKey ? pubKey : '*'.repeat(300)}</p>
                </div>
                <img className="show-key" onClick={() => setShowKey(!showKey)} src={showKey ? EyeOpen : EyeClosed} />
            </div>
            <h2>Your Balance: <img className="kebab-icon" src={KebabIcon} alt='icon' />{balance}</h2>
        </div>
    )
}

export default Wallet;