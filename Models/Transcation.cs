using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;
[PrimaryKey(nameof(BlockId), nameof(Id))]
public class Transaction{
    // public Transaction(){}
    public int Id {get;set;}
    public int BlockId { get; set; }
    public Block block{get; set; }
    // TODO: Change these to non list types 
    public List<TransactionInput> Inputs {get;set;} = new List<TransactionInput>();
    public List<TransactionOutput> Outputs {get;set;} = new List<TransactionOutput>(); // Main output is obvs to whoever, then change (if any) will be the last output and sent to self, so sigScript will just be for your own pubkey
    public override string ToString()
    {
        return $"{Id} \nInputs: {Inputs} \nOutputs:{Outputs}";
    }
}