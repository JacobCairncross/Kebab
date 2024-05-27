using Kebab.Models;

// Should this be the singleton, or should the model be the singleton?
// TODO: Make a generic block making function
namespace Kebab.Managers;
public class BlockChainManager
{
    private const int MINIMUM_TRANSACTIONS_TO_BLOCK = 0;
    // Change this to be alterable based on rate of transactions
    private const int DIFFICULTY_LEVEL = 1;
    private const int MINIMUM_HASH_BYTE_LENGTH = 32; 
    private readonly Models.BlockChain chain;
    private List<Transaction> transactions = new ();
    private readonly IHttpClientFactory _httpClientFactory;

    public BlockChainManager(IHttpClientFactory httpClientFactory, Options? options)
    {
        _httpClientFactory = httpClientFactory;
        // TODO: Optionaly accept an existing blockchain to initialise chain
        chain = new BlockChain();
        // If no blockchain provided add a genesis block

        if(options?.GenesisPubKey is null)
        {
            // TODO: Dont throw exception, 
            throw new Exception("No public key path provided");
        }
        string publicKey = File.ReadAllText(options.GenesisPubKey);
        Console.WriteLine(publicKey);
        Random rnd = new();
        Transaction genesisTransaction = new(){
            ID="0",
            Inputs=[],
            Outputs=[
                new TransactionOutput(){
                    Value=int.MaxValue,
                    PublicKey=publicKey,
                    Nonce=rnd.Next()
                }
            ]
        };
        Block genesis = CreateBlock(new Guid(),DateTime.Now, new byte[32], "doner",[genesisTransaction]);
        chain.AddBlock(genesis);
    }   

    // TODO: Add a function to verify a new block / new chain for when its docked
    public bool AddTransaction(Transaction transaction)
    {
        
        // TODO: Maybe keep a store of a minimum number of transactions before creating a block
        transactions.Add(transaction);

        if(transactions.Count > MINIMUM_TRANSACTIONS_TO_BLOCK){
            AddBlock();
        }
        return true;
    }

    public Transaction? GetTransaction(int BlockId, string txid)
    {
        Transaction[] transactions = chain[BlockId].Transactions;
        return transactions.FirstOrDefault(t => t.ID == txid);
    }

    public int AddBlock()
    {
        Guid id = new();
        DateTime timestamp = DateTime.Now;
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
        // TODO: So like, if someone just keeps putting 00000 they'll get it and not actually have to 
        // do a hash, add in a minimum hash value too
        
        if(chain.AddBlock(newBlock)){
            transactions.Clear();
            return chain.Count();
        }
        return -1;
    }

    public BlockChain GetChain()
    {
        return chain;
    }
    // Maybe move this into blockchain model???
    private Block CreateBlock(Guid id, DateTime timestamp, byte[] prevHash, string nonce, Transaction[] transactions)
    {
        byte[] hashCode = Block.GetHash(id, timestamp, prevHash, nonce, transactions);
        Block newBlock = new()
        {
            BlockId = chain.Count(),
            Timestamp = timestamp,
            BlockHash = hashCode,
            PreviousHash = prevHash,
            Nonce = nonce,
            Transactions = transactions
        };
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
}