using Kebab.Models;
using Microsoft.EntityFrameworkCore;

// Should this be the singleton, or should the model be the singleton?
// TODO: Make a generic block making function
namespace Kebab.Managers;
public class BlockChainManager
{
    private const int MINIMUM_TRANSACTIONS_TO_BLOCK = 1;
    // Change this to be alterable based on rate of transactions
    private const int DIFFICULTY_LEVEL = 1;
    private const int MINIMUM_HASH_BYTE_LENGTH = 32; 
    private readonly Models.BlockChain chain;
    private List<TransactionRequest> transactions = new ();
    private readonly IHttpClientFactory _httpClientFactory;

    public BlockChainManager(IHttpClientFactory httpClientFactory, Options? options, BlockChain blockChain)
    {
        _httpClientFactory = httpClientFactory;
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
        chain = blockChain;
        if(chain.Count() == 0)
        {
            if(options?.GenesisPubKey is null)
            {
                // TODO: Dont throw exception, 
                throw new Exception("No public key path provided");
            }
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
            chain.AddBlock(genesis);
        }

        
    }   

    // TODO: Add a function to verify a new block / new chain for when its docked
    public bool AddTransaction(TransactionRequest transaction)
    {
        
        // TODO: Maybe keep a store of a minimum number of transactions before creating a block
        transactions.Add(transaction);

        // May need to ensure theres a lock of sorts on this? but that would require 
        // the transactions list to be shared amongst managers
        if(transactions.Count >= MINIMUM_TRANSACTIONS_TO_BLOCK){
            AddBlock();
        }
        return true;
    }

    public Transaction? GetTransaction(int BlockId, int txid)
    {
        // Block block = chain[BlockId];
        // ICollection<Transaction> transactions = block.Transactions;
        // Transaction? transaction =  transactions.FirstOrDefault(t => t.Id == txid);
        // return transaction;
        // return chain.GetEnumerator().FirstOrDefault().Transactions.FirstOrDefault(t => t.Id == txid);
        return chain[BlockId].Transactions.FirstOrDefault(t => t.Id == txid);
    }

    public int AddBlock()
    {
        int id = chain.Count() + 1;
        DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        byte[] prevHash = chain.Last().BlockHash;
        //Hard code for now, will make a loop to find a good one later
        Block newBlock;
        int nonce = 0;        
        do
        {
            newBlock = CreateBlock(id, timestamp, prevHash, nonce.ToString(), transactions.ToArray());
            nonce++;
            Console.WriteLine(newBlock.BlockHash);
        }
        while(newBlock.BlockHash[0..DIFFICULTY_LEVEL].All(b => b == 0));
        
        if(chain.AddBlock(newBlock)){
            transactions.Clear();
            return chain.Count();
        }
        return -1;
    }

    public BlockChain GetChain()
    {
        Console.WriteLine("Returning chain");
        return chain;
    }
    // Maybe move this into blockchain model???
    private Block CreateBlock(int id, DateTimeOffset timestamp, byte[] prevHash, string nonce, TransactionRequest[] transactionRequests)
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