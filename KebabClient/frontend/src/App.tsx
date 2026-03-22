import { useEffect, useRef, useState } from 'react'
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
  const popUpsRef = useRef(popUps)

  useEffect(() => {
    popUpsRef.current = popUps;
  }, [popUps]);

  const AddPopUp = (text:string, type:PopupType) => {
    const popUpid = (Math.random()*1000).toString()
    const newPopUp = {id: popUpid, text:text, type: type, RemovePopUp: RemovePopUp}
    setPopUps([...popUps, newPopUp])
    setTimeout(() => RemovePopUp(popUpid), 20000)
  }

  const RemovePopUp = (popUpId: string) => {
    const remainingPopups = popUpsRef.current.filter(p => p.id !== popUpId) 
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
