import { useEffect, useState } from "react"
import { Block } from "../models/Blocks"

// Need to actually make the corresponding function in the chain code
const GetChain = (): Block[] => {
    const [chain, setChain] = useState<Block[]>([])

    useEffect(() => {
        fetchData().then(c => {
            setChain(c)
        })
    }, [])

    const fetchData = async () => {
        const response = await fetch('/api/miner/chain')
        const blockChain = await response.json()
        console.log("Chain response: ")
        console.log(blockChain)
        return blockChain
    } 
    return chain
}

export default GetChain