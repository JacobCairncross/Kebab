import { useEffect, useState } from "react"

const GetKey = async (): Promise<string> => {
    const [key, setKey] = useState('')

    useEffect(() => {
        fetchData().then(k => setKey(k))
    }, [])

    const fetchData = async () => {
        const response = await fetch('/api/Wallet/GetKey')
        return await response.text()
    } 
    return key
}

export default GetKey