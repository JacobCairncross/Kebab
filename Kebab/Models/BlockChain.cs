using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Kebab.Data.Contexts;
using Kebab.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kebab.Models;

// Maybe change this to not need a db? That way it can be used in the client
// TODO: Add an abstractions library then extend it in here to actually use the db
public class BlockChain(AppDbContext db) : IEnumerable
{
    // public List<Block> chain {get;set;} = new List<Block>();

    // public BlockChain{}
    
    public Block this[int index] => db.Blocks
        .Include("Transactions.Outputs")
        .Include("Transactions.Inputs")
        .First( b => b.BlockId == index );

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
        List<Block> chain = db.Blocks
            .Include("Transactions.Outputs")
            .Include("Transactions.Inputs")
            .ToList();
        return JsonSerializer.Serialize(chain, options);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // TODO: Unsure what this does, check it out
        return db.Blocks.AsEnumerable().GetEnumerator();
    }
}