import { Component, JSX, ReactNode } from "react"

interface Props {
  MainBox: React.ComponentType
  description: string
}

const DashboardWidget: React.FC<Props> = ({MainBox, description}) => {
    return (
        <div className="dashboard-widget">
            <h1>DashboardWidget</h1>
            <div className="main-box">
                <MainBox />
            </div>
            <div className="description">{description}</div>
        </div>
    )
}

export default DashboardWidget