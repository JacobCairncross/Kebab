using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;
[PrimaryKey(nameof(BlockId), nameof(TransactionId), nameof(OutputIndex))]
public class TransactionInput
{
    // Block this transaction is from
    public int BlockId{get;set;}
    // index in list of corresponding out transactions
    public int TransactionId {get;set;}
    public int OutputIndex {get;set;}
    public Transaction Transaction {get;set;}
    // Signature to prove you have the private key associated with the public key of this output
    public byte[] Signature {get;set;}

}