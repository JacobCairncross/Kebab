using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            ICollection<Transaction> transactions = transactionRequests.Select((tr,i) => new Transaction(){
                Id=i,
                BlockId=blockId,
                Inputs=tr.Inputs,
                Outputs=tr.Outputs.Select((o,oi) => new TransactionOutput(){
                    BlockId=blockId,
                    TransactionId=i,
                    OutputIndex=oi,
                    Value=o.Value,
                    PublicKey=o.PublicKey,
                    Nonce=o.Nonce
                }).ToList()
            }).ToList();
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

    public Transaction this[int index] => Transactions.First(t => t.Id == index);
}