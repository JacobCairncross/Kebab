using System.Security.Cryptography;
using System.Text;
using Kebab.Data.Models;
using Kebab.Models;
using Microsoft.VisualBasic;

namespace Kebab.Managers;

public class TransactionManager
{
    public readonly BlockChainManager _blockChainManager;
    public TransactionManager(BlockChainManager blockChainManager){
        _blockChainManager = blockChainManager;
    }

    public bool AddTransaction(TransactionRequest transaction)
    {
        if(VerifyTransaction(transaction))
        {
            return _blockChainManager.AddTransaction(transaction);
        }
        return false;
    }
    // Could be better to return something else, an explanation
    // but for now we're not doing anything complicated enough to warrant it
    public bool VerifyTransaction(TransactionRequest transaction)
    {
        bool inputsSigned = transaction.Inputs.All(i => VerifyInput(i));

        int totalInputValue = transaction.Inputs.Select(i => _blockChainManager.GetTransaction(i.BlockId, i.TransactionId)?.Outputs[i.OutputIndex])
                            .Sum(o => o.Value);
        int totalOutputValue = transaction.Outputs.Sum(o => o.Value);
        return totalInputValue >= totalOutputValue && inputsSigned;
        
    }

    public bool VerifyInput(TransactionInput input)
    {
        // TODO: Null check the fuck out of this
        // Get txout from hash and index
        Transaction? transaction = _blockChainManager.GetTransaction(input.BlockId, input.TransactionId);
        if(transaction == null)
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

        using(RSACryptoServiceProvider rsa = new())
        using(HMACSHA256 hmac = new(hmacKey.ToArray()))
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