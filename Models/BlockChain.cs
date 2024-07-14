using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace Kebab.Models;
public class BlockChain(AppDbContext db) : IEnumerable
{
    // public List<Block> chain {get;set;} = new List<Block>();

    // public BlockChain{}
    
    public Block this[int index] => db.Blocks.First( b => b.BlockId == index );

    public bool AddBlock(Block block){
        // Validate block
        // chain.Add(block);
        db.Blocks.Add(block);
        db.SaveChanges();
        return true;
    }

    public Block Last()
    {
        return db.Blocks.First(b => b.BlockId == db.Blocks.Max(b => b.BlockId));
    }

    public int Count()
    {
        return db.Blocks.Count();
    }

    public override string ToString()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        return JsonSerializer.Serialize(db.Blocks.ToList(), options);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // TODO: Unsure what this does, check it out
        return db.Blocks.AsEnumerable().GetEnumerator();
    }
}