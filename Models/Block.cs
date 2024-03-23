using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Kebab.Models;
public class Block{
    public int BlockId {get;set;}
    public DateTime Timestamp {get;set;}
    public byte[] BlockHash {get;set;}
    public byte[] PreviousHash {get;set;}
    public string Nonce {get;set;}
    public Transaction[] Transactions {get;set;}

    public static byte[] GetHash(Guid blockId, DateTime timestamp, byte[] previousHash, string nonce, Transaction[] transactions)
    {
        return SHA256.HashData(Encoding.ASCII.GetBytes($"{blockId}{timestamp}{previousHash}{nonce}{string.Join<Transaction>(',', transactions)}"));
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}