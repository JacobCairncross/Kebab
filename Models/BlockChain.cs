using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace Kebab.Models;
public class BlockChain : IEnumerable
{
    public List<Block> chain {get;set;} = new List<Block>();

    public BlockChain(){}
    
    public Block this[int index] => chain[index];

    public bool AddBlock(Block block){
        // Validate block
        chain.Add(block);
        return true;
    }

    public Block Last()
    {
        return chain.Last();
    }

    public int Count()
    {
        return chain.Count();
    }

    public override string ToString()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return chain.GetEnumerator();
    }
}