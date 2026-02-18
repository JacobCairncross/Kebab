using System.Security.Cryptography;
using System.Text;
using Kebab.Data.Models;
using Kebab.Models;
using Microsoft.VisualBasic;

namespace Kebab.Managers;

public class TransactionManager
{
    public readonly BlockChainManager _blockChainManager;
    public readonly List<TransactionRequest> _transactionRequests;
    public TransactionManager(BlockChainManager blockChainManager, [FromKeyedServices("transactionRequest")] List<TransactionRequest> transactionRequests)
    {
        _blockChainManager = blockChainManager;
        _transactionRequests = transactionRequests;
    }

    public bool AddTransaction(TransactionRequest transaction)
    {
        if (VerifyTransaction(transaction))
        {
            _transactionRequests.Add(transaction);
            return true;
        }
        return false;
    }

    // Consider making transaction requests public and just giving it a specified getter in the declaration
    public List<TransactionRequest> PendingTransactionRequests()
    {
        return _transactionRequests;
    }
    // Could be better to return something else, an explanation
    // but for now we're not doing anything complicated enough to warrant it
    public bool VerifyTransaction(TransactionRequest transaction)
    {
        // Check all inputs are valid
        bool inputsSigned = transaction.Inputs.All(i => VerifyInput(i));

        // Check outputs <= Sum(inputs)
        int totalInputValue = transaction.Inputs.Select(i => _blockChainManager.GetTransaction(i.BlockId, i.TransactionId)?.Outputs[i.OutputIndex])
                            .Sum(o => o.Value);
        int totalOutputValue = transaction.Outputs.Sum(o => o.Value);

        // Also need to check incase any inputs are used in the currently pending transaction requests
        // _transactionRequests.Where(tr => tr.)
        bool inputAlreadyUsed = transaction.Inputs.Any(i =>
            _transactionRequests.Any(r =>
                r.Inputs.Any(j => i.BlockId == j.BlockId && i.TransactionId == j.TransactionId && i.OutputIndex == j.OutputIndex)
        ));
        return totalInputValue >= totalOutputValue && inputsSigned && !inputAlreadyUsed;

    }

    // <summary>
    // Checks to see if transaction input has the correct hash to confirm they own the wallet 
    // the specified output was sent to  
    // </summary>
    public bool VerifyInput(TransactionInput input)
    {
        // TODO: Null check the fuck out of this
        // Get txout from hash and index
        Transaction? transaction = _blockChainManager.GetTransaction(input.BlockId, input.TransactionId);
        if (transaction == null)
        {
            throw new Exception("A claimed transaction does not exist");
        }
        TransactionOutput txOut = transaction.Outputs[input.OutputIndex];
        // Get public key from txout
        string publicKey = txOut.PublicKey;
        // RSACryptoServiceProvider rsaCrypSerPro = new ();
        // RSAParameters rsaParams = new RSAParameters(publicKey);
        UTF8Encoding encoder = new();
        Console.WriteLine(txOut.ToString());
        byte[] plainTransaction = encoder.GetBytes(txOut.ToString());
        // Should stick this as a const somewhere better, probs appsettings? 
        // It doesnt need to be secret as we're just using it to shorten the transaction
        // ReadOnlySpan<byte> hmacKey = "garlic sauce"u8;
        byte[] hmacKey = Encoding.ASCII.GetBytes("garlic sauce");

        using (RSACryptoServiceProvider rsa = new())
        using (HMACSHA256 hmac = new(hmacKey.ToArray()))
        {
            rsa.ImportFromPem(publicKey);
            // We may not need this hmac, if the 
            byte[] hashMessage = hmac.ComputeHash(plainTransaction);
            bool verified = rsa.VerifyHash(hashMessage, HashAlgorithmName.SHA256.Name, input.Signature);
            return verified;
        }

        // So find a way to make rsaParams object out of just public key (probably)
        // then put that into rsaCrypSerPro (that is an awful var name)
        // then we need to verify this private key has signed the trans
        // so we hash the data using a known algo (SHA is good)
        // Then we do a rsaCrypSerPro.VerifyHash(hashedData, AlgoName(theres an object for this), signature);
        // this will undo the signing the private key did (if it truly did it) and should match the provided hashed data
    }
}