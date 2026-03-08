import { useState } from 'react'
import './App.css'
import Wallet from './views/Wallet'
import Dashboard from './views/Dashboard'
import Transaction from './views/Transaction'

function App() {
// TODO: use react router, probably in framework mode, once this is working well https://reactrouter.com/start/framework/routing
  const [page, setPage] = useState('dashboard')
  return (
    <>
      <div className="header">
        <h1>Kebab</h1>

        <div id="nav-bar">
          <div className="nav-button" onClick={() => setPage("dashboard")}>Dashboard</div>
          <div className="nav-button" onClick={() => setPage("wallet")}>Wallet</div>
          <div className="nav-button" onClick={() => setPage("transaction")}>Transaction</div>
        </div>
      </div>
      <div className="card">
        <h3>Manage your Meat</h3>
      </div>
      {renderPage(page)}
    </>
  )
}

const renderPage = (page: string) => {
  switch (page){
    case "dashboard":
      return <Dashboard />
    case "wallet":
      return <Wallet />
    case "transaction":
      return <Transaction />
  }
}  

export default App
