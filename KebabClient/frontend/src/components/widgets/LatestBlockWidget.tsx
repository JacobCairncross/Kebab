import GetChain from "../../hooks/GetChain"
import DashboardWidget from "./DashboardWidget"

const LastestBlockWidget = () =>{
    const chain = GetChain()
    const lastBlockData = chain[chain.length -1]
    console.log("LatestBlockWidget: ")
    console.log(chain)
    const lastBlock = chain.length > 0 ? 
        () =>
            <div>
                <h3>ID: {lastBlockData.blockId}</h3>
                <h5>Amount sent: {lastBlockData.nonce}</h5>
                <h5>Time sent: {lastBlockData.timestamp}</h5>
            </div>
        : () =>
            <div>Chain is empty</div>
    return <DashboardWidget MainBox={lastBlock} description={'Latest Block - Test data'}/>
} 

export default LastestBlockWidget