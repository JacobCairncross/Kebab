using Kebab.Managers;
using Kebab.Models;
using Microsoft.AspNetCore.Mvc;

public class BlockChainController : Controller{
    private readonly BlockChainManager _blockChainManager;
    private readonly TransactionManager _transactionManager;

    public BlockChainController(BlockChainManager blockChainManager, TransactionManager transactionManager)
    {
        _blockChainManager = blockChainManager;
        _transactionManager = transactionManager;
    }

    [HttpPost]
    public string Transaction([FromBody] Transaction transaction)
    {
        bool status = _transactionManager.AddTransaction(transaction);
        return $"Received transaction {transaction}. Transaction Status {status}";
    }

    public string Chain(){
        return _blockChainManager.GetChain().ToString();
    }
}