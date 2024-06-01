using System.ComponentModel.DataAnnotations;

namespace Kebab.Models;

public class TransactionOutput
{
    [Key]
    public int Id { get; set; }
    public int Value {get;set;}
    // Pub Key this Output is addressed to
    public string PublicKey{get;set;}
    // Nonce used to prevent one signature being used to spend another transaction of same amount
    public int Nonce{get;set;}
}