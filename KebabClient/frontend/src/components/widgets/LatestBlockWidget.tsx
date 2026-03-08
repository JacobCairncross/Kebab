import DashboardWidget from "./DashboardWidget"

const LastestBlockWidget = () =>{
    const lastBlock = () =>
        <div>
            <h3>ID: 1337</h3>
            <h5>Amount sent: 803</h5>
            <h5>Time sent: 2026-02-22:10:48</h5>
        </div>
    return <DashboardWidget MainBox={lastBlock} description={'Latest Block - Test data'}/>
} 

export default LastestBlockWidget