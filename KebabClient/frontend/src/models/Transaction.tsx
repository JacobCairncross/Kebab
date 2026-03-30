import { TransactionInput } from "./transactionInput"
import { TransactionOutput } from "./transactionOutput"

export interface Transaction{
    id: string
    blockId: Date
    inputs: TransactionInput[]
    outputs: TransactionOutput[]
}