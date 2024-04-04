namespace Kebab.Models;

public class TransactionOutput
{
    // Amount of Doners to be sent
    public int Value {get;set;}
    // Pub Key this Output is addressed to
    public string PublicKey{get;set;}
    // Nonce used to prevent one signature being used to spend another transaction of same amount
    public int Nonce{get;set;}
}