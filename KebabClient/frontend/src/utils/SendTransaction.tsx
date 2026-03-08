// TODO: Rework this component once the backend api is remade. It should return something like a transactionID
// to know its worked
const SendTransaction = async (publicKey: string, value: number): Promise<string> => {
    const response = await fetch('/api/Wallet/SendSingle',{
        method: "POST",
        body: new URLSearchParams({ PublicKey: publicKey, Value: value.toString() })
    })
    if(response.status == 200){
        return "Successful"
    }
    return "UnSuccessful"
}

export default SendTransaction