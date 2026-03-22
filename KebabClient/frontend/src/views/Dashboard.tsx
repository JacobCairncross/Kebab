import { FC } from "react"
import LastestBlockWidget from "../components/widgets/LatestBlockWidget"
import { PageProps } from "../App"

const Dashboard:FC<PageProps> = ({AddPopUp}) => {
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