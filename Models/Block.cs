using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Kebab.Models;
public class Block{
    public Block() { }
    public Block(int blockId, DateTimeOffset timestamp, byte[] blockHash, byte[] previousHash, string nonce, Transaction[] transactions) 
    { 
            BlockId = blockId;
            Timestamp = timestamp;
            BlockHash = blockHash;
            PreviousHash = previousHash;
            Nonce = nonce;
            Transactions = transactions;
    }
    [Key]
    public int BlockId {get;set;}
    public DateTimeOffset Timestamp {get;set;} = DateTimeOffset.UtcNow;
    public byte[]? BlockHash {get;set;}
    public byte[]? PreviousHash {get;set;}
    public string? Nonce {get;set;}
    public ICollection<Transaction> Transactions {get;set;} = new List<Transaction>();

    public static byte[] GetHash(int blockId, DateTimeOffset timestamp, byte[] previousHash, string nonce, Transaction[] transactions)
    {
        return SHA256.HashData(Encoding.ASCII.GetBytes($"{blockId}{timestamp}{previousHash}{nonce}{string.Join<Transaction>(',', transactions)}"));
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}