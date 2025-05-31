using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;
[PrimaryKey(nameof(BlockId), nameof(TransactionId), nameof(OutputIndex))]
public class TransactionInput
{
    // Block the output transaction is from
    public int BlockId{get;set;}
    // index in list of corresponding transactions
    public int TransactionId {get;set;}
    public int OutputIndex {get;set;}
    [JsonIgnore]
    public Transaction Transaction {get;set;}
    // Signature to prove you have the private key associated with the public key of this output
    public byte[] Signature {get;set;}

}