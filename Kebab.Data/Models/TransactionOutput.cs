using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Kebab.Data.Models;

[PrimaryKey(nameof(BlockId), nameof(TransactionId), nameof(OutputIndex))]
public class TransactionOutput
{
    public int BlockId { get; set; }
    public int TransactionId { get; set; }
    public int OutputIndex { get; set; }
    [JsonIgnore]
    public Transaction Transaction { get; set; }
    public int Value { get; set; }
    // Pub Key this Output is addressed to
    public string PublicKey { get; set; }
    // Nonce used to prevent one signature being used to spend another transaction of same amount
    public int Nonce { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}