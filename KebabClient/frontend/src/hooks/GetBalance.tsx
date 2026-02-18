import { useEffect, useState } from "react"

const GetBalance = async (): Promise<string> => {
    const [balance, setBalance] = useState('')

    useEffect(() => {
        fetchData().then(b => setBalance(b))
    }, [])

    const fetchData = async () => {
        const response = await fetch('/api/Wallet/GetBalance')
        return await response.text()
    } 
    return balance
}

export default GetBalance