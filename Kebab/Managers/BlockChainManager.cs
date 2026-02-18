using Kebab.Data.Models;
using Kebab.Models;
using Microsoft.EntityFrameworkCore;

// Should this be the singleton, or should the model be the singleton?
// TODO: Make a generic block making function
namespace Kebab.Managers;
public class BlockChainManager
{
    // Change this to be alterable based on rate of transactions
    private const int DIFFICULTY_LEVEL = 1;
    private const int MINIMUM_HASH_BYTE_LENGTH = 32; 
    private readonly Models.BlockChain _chain;
    // TODO: Rename this to _transactionRequests
    private List<TransactionRequest> _transactions = new();
    private readonly IHttpClientFactory _httpClientFactory;

    public BlockChainManager(IHttpClientFactory httpClientFactory, Options? options, BlockChain blockChain, [FromKeyedServices("transactionRequest")] List<TransactionRequest> transactionRequests)
    {
        _httpClientFactory = httpClientFactory;
        _transactions = transactionRequests;
        // chain = new BlockChain();

        // Check if DB has block data to init from
        // Check db here to see if its workin how you expect
        // Will the blocks be in order? If I cant guarantee it then first order by datetime
        // could also be best to make ID an int instead of guid to make ordering easier
        // foreach(var block in db.Blocks)
        // {
        //     chain.AddBlock(block);
        // }
        // TODO: Optionaly accept an existing blockchain to initialise chain

        // If no blocks in db and none provided add a genesis block
        _chain = blockChain;
        if(_chain.Count() == 0)
        {
            if(options?.GenesisPubKey is null)
            {
                // TODO: Dont throw exception, 
                throw new Exception("No public key path provided");
            }
            // string publicKey = File.ReadAllText(options.GenesisPubKey).Replace("\r", "");
            string publicKey = File.ReadAllText(options.GenesisPubKey);
            Random rnd = new();
            TransactionRequest genesisTransaction = new(){
                Inputs=[],
                Outputs=[
                    new TransactionProvisionalOutput(){
                        Value=int.MaxValue,
                        PublicKey=publicKey,
                        Nonce=rnd.Next()
                    }
                ]
            };
            Block genesis = CreateBlock(1 ,DateTimeOffset.UtcNow, new byte[32], "doner",[genesisTransaction]);
            _chain.AddBlock(genesis);
        }

        
    }

    // TODO: Add a function to verify a new block / new chain for when its docked
    // TODO: Return false if something goes wrong
    // public bool AddTransaction(TransactionRequest transaction)
    // {

    //     // TODO: Maybe keep a store of a minimum number of transactions before creating a block
    //     _transactions.Add(transaction);

    //     // May need to ensure theres a lock of sorts on this? but that would require 
    //     // the transactions list to be shared amongst managers
    //     if (_transactions.Count >= MINIMUM_TRANSACTIONS_TO_BLOCK)
    //     {
    //         Console.WriteLine($"start that block at {DateTime.Now}");
    //         var _ = AddBlock();
    //     }
    //     Console.WriteLine($"Exited add block at {DateTime.Now}");
    //     return true;
    // }

    public Transaction? GetTransaction(int BlockId, int txid)
    {
        // Block block = chain[BlockId];
        // ICollection<Transaction> transactions = block.Transactions;
        // Transaction? transaction =  transactions.FirstOrDefault(t => t.Id == txid);
        // return transaction;
        // return chain.GetEnumerator().FirstOrDefault().Transactions.FirstOrDefault(t => t.Id == txid);
        return _chain[BlockId].Transactions.FirstOrDefault(t => t.Id == txid);
    }

    // TODO: Check if this is needed or if we can just have the one function
    public bool AddBlock()
    {
        return AddBlock(_chain, _transactions);
    }

    public static bool AddBlock(BlockChain? chain, List<TransactionRequest> transactions)
    {
        if(chain is null || !transactions.Any()) return false;

        int id = chain.Count() + 1;
        DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        byte[] prevHash = chain.Last().BlockHash;
        //Hard code for now, will make a loop to find a good one later
        Block newBlock;
        int nonce = 0;
        // TODO: Add a cancellation token here, that way when we receive news someone else has solved the block we arent stuck still solving this one 
        do
        {
            newBlock = CreateBlock(id, timestamp, prevHash, nonce.ToString(), transactions.ToArray());
            nonce++;
            Console.WriteLine($"nonce: {nonce}");
            newBlock.BlockHash[0..DIFFICULTY_LEVEL].Select(b => { Console.Write(b); return true; });
        }
        while (!newBlock.BlockHash[0..DIFFICULTY_LEVEL].All(b => b == 0));
        Console.WriteLine($"Added that block at {DateTime.Now}");
        if (chain.AddBlock(newBlock))
        {
            transactions.Clear();
            return true;
        }
        return false;
    }

    public BlockChain GetChain()
    {
        Console.WriteLine("Returning chain");
        return _chain;
    }
    // Maybe move this into blockchain model???
    private static Block CreateBlock(int id, DateTimeOffset timestamp, byte[] prevHash, string nonce, TransactionRequest[] transactionRequests)
    {
        Block newBlock = new(id, timestamp, prevHash, nonce, transactionRequests);
        return newBlock;
    }

    // Do we accept chain or a URL to get chain from? I'm leaning towards url
    public bool UpdateChain(string url)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.GetAsync(url);
        //Json Shit
        return true;
    }

    public bool ValidateTransaction(Transaction transaction)
    {
        // Check all txIns have not been previously spent
        // foreach(var input in transaction.Inputs)
        // {
            
        // }
        throw new NotImplementedException();
    }
}