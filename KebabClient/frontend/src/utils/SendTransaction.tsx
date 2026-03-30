// TODO: Rework this component once the backend api is remade. It should return something like a transactionID
// to know its worked. Maybe some proper objects
const SendTransaction = async (publicKey: string, value: number): Promise<string> => {
    const response = await fetch('/api/Transaction/Send',{
        method: "POST",
        body: new URLSearchParams({ PublicKey: publicKey, Value: value.toString() })
    })
    const responseBody = await response.json
    console.log(responseBody)
    if(response.status == 200){
        return "Successful"
    }
    return "UnSuccessful"
}

export default SendTransaction