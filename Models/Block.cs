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
    public Block(int blockId, DateTimeOffset timestamp, byte[] previousHash, string nonce, TransactionRequest[] transactionRequests) 
    { 
            BlockId = blockId;
            Timestamp = timestamp;
            PreviousHash = previousHash;
            Nonce = nonce;
            // TODO: Theres got to be a better way to do this
            ICollection<Transaction> transactions = (ICollection<Transaction>)transactionRequests.Select((tr,i) => new Transaction(){
                Id=i,
                BlockId=blockId,
                block=this,
                Inputs=tr.Inputs,
                Outputs=tr.Outputs
            });
            Transactions = transactions;
            BlockHash = GetHash(blockId, timestamp, previousHash, nonce, transactions);
    }
    [Key]
    public int BlockId {get;set;}
    public DateTimeOffset Timestamp {get;set;} = DateTimeOffset.UtcNow;
    public byte[]? BlockHash {get;set;}
    public byte[]? PreviousHash {get;set;}
    public string? Nonce {get;set;}
    public ICollection<Transaction> Transactions {get;set;} = new List<Transaction>();

    public static byte[] GetHash(int blockId, DateTimeOffset timestamp, byte[] previousHash, string nonce, ICollection<Transaction> transactions)
    {
        return SHA256.HashData(Encoding.ASCII.GetBytes($"{blockId}{timestamp}{previousHash}{nonce}{string.Join<Transaction>(',', transactions)}"));
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}