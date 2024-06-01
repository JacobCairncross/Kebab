using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;
[PrimaryKey(nameof(BlockId), nameof(Id))]
public class Transaction{
    public int BlockId { get; set; }
    public string Id {get;set;}
    public TransactionInput[] Inputs {get;set;}
    public TransactionOutput[] Outputs {get;set;} // Main output is obvs to whoever, then change (if any) will be the last output and sent to self, so sigScript will just be for your own pubkey
    public override string ToString()
    {
        return $"{Id} \nInputs: {Inputs} \nOutputs:{Outputs}";
    }
}