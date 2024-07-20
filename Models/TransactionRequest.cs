namespace Kebab.Models;
public class TransactionRequest{
    public List<TransactionInput> Inputs {get;set;} = new List<TransactionInput>();
    // TODO: This very likely needs changing so outputs dont need to be built.
    // May be able to change it so TransactionOutput doesnt need all its fields?
    public List<TransactionOutput> Outputs {get;set;} = new List<TransactionOutput>();
}