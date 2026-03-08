import LastestBlockWidget from "../components/widgets/LatestBlockWidget"

const Dashboard = () => {
    return (
        <div>
            <h1>Dashboard</h1>
            <div className="widgets">
                <LastestBlockWidget />

            </div>
        </div>
    )
}

export default Dashboard