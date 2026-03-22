import { useState } from 'react'
import './App.css'
import Wallet from './views/Wallet'
import Dashboard from './views/Dashboard'
import Transaction from './views/Transaction'
import PopUpQueue from './components/PopUpQueue'
import { PopUpProps, PopupType } from './components/PopUp'

export interface PageProps {
  AddPopUp: (text:string, type:PopupType) => void
} 

function App() {
// TODO: use react router, probably in framework mode, once this is working well https://reactrouter.com/start/framework/routing
  const [page, setPage] = useState('dashboard')
  const [popUps, setPopUps] = useState<PopUpProps[]>([])

  const AddPopUp = (text:string, type:PopupType) => {
    const newPopUp = {id: (Math.random()*1000).toString(), text:text, type: type, RemovePopUp: RemovePopUp}
    console.log(...popUps)
    console.log(newPopUp)
    setPopUps([...popUps, newPopUp])
  }

  const RemovePopUp = (popUpId: string) => {
    console.log('=================')
    const remainingPopups = popUps.filter(p => p.id !== popUpId) 
    console.log(Date.now())
    console.log(popUpId);
    console.log(popUps)
    console.log(remainingPopups)
    setPopUps([...remainingPopups])
  }

  const renderPage = (page: string) => {
    switch (page){
      case "dashboard":
        return <Dashboard AddPopUp={AddPopUp}/>
      case "wallet":
        return <Wallet AddPopUp={AddPopUp}/>
      case "transaction":
        return <Transaction AddPopUp={AddPopUp}/>
    }
  }  

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
      <PopUpQueue popUps={popUps}/>
    </>
  )
}



export default App
