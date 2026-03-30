import { Transaction } from "./Transaction"

export interface Block{
    blockId: string
    timestamp: string
    blockHash: string
    previousHash: string
    nonce: string
    transactions: Transaction[]
}