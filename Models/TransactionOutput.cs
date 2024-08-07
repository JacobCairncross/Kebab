using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;

[PrimaryKey(nameof(BlockId), nameof(TransactionId), nameof(OutputIndex))]
public class TransactionOutput
{
    public int BlockId { get; set; }
    public int TransactionId { get; set; }
    public int OutputIndex { get; set; }
    public Transaction Transaction { get; set; }
    public int Value {get;set;}
    // Pub Key this Output is addressed to
    public string PublicKey{get;set;}
    // Nonce used to prevent one signature being used to spend another transaction of same amount
    public int Nonce{get;set;}
}