namespace Kebab.Models;
public class Transaction{
    public string ID {get;set;}
    public TransactionInput[] Inputs {get;set;}
    public TransactionOutput[] Outputs {get;set;} // Main output is obvs to whoever, then change (if any) will be the last output and sent to self, so sigScript will just be for your own pubkey
    public override string ToString()
    {
        return $"{ID} \nInputs: {Inputs} \nOutputs:{Outputs}";
    }
}