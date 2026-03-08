using System.Security.Cryptography;
using Kebab.Data.Models;
using KebabClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KebabClient.Managers;

public class WalletManager(Options options, MinerManager minerManager)
{
    private Options _options => options;
    public enum Key
    {
        Public,
        Private
    }

    public async Task<Tuple<string, string>> CreateWallet()
    {
        RSA rsa = RSA.Create();
        Console.WriteLine(rsa.ExportRSAPublicKeyPem());
        Console.WriteLine(rsa.ExportPkcs8PrivateKeyPem());
        Task publicWriteFinished = File.WriteAllTextAsync("./PublicKey", rsa.ExportRSAPublicKeyPem());
        Task privateWriteFinished = File.WriteAllTextAsync("./PrivateKey", rsa.ExportRSAPrivateKeyPem());
        await Task.WhenAll(privateWriteFinished, publicWriteFinished);
        Console.WriteLine("Your wallet has been made, pls check your files");
        return new Tuple<string, string>(rsa.ExportRSAPublicKeyPem(), rsa.ExportRSAPrivateKeyPem());
    }

    // TODO: Change this to return a string. Most places using it convert it anyway and is only used as char array in SignOutput
    public async Task<char[]> ReadKey(Key key)
    {
        string? keyPath = key == Key.Public ? _options.publicKeyPath : _options.privateKeyPath;
        if (keyPath is null)
        {
            throw new Exception("Key path is null");
        }
        if (File.Exists(keyPath))
        {
            return (await File.ReadAllTextAsync(keyPath)).ToCharArray();
        }
        else
        {
            // TODO: Maybe rework this, if I'm going to throw this anyway then maybe theres no need to check
            throw new FileNotFoundException("File does not exist, generate wallet before attempting to read");
        }
    }

    public async Task<int> GetBalance()
    {
        // TODO: Can chain these so they await together to improve performance
        char[] pubKey = await ReadKey(Key.Public);
        string pubKeyString = new string(pubKey);

        List<Block> chain = await minerManager.GetChain();

        // TODO: Theres gotta be a better way of doing this via some db queries. May need doing on the miner but thats fine
        // TODO: Need to account for "burnt" outputs. You're double counting
        long balance = 0;
        Console.WriteLine($"pubKeyString: {pubKeyString}");
        foreach(var block in chain)
        {
            foreach (var transaction in block.Transactions)
            {
                Console.WriteLine($"bid: {transaction.BlockId}, tid: {transaction.Id}");
                foreach(var output in transaction.Outputs)
                {
                    Console.WriteLine($"outputs: {output.BlockId}, {output.TransactionId}, {output.OutputIndex}");
                    if(output.PublicKey == pubKeyString)
                    {
                        Console.WriteLine($"✅: {output.BlockId}, {output.TransactionId}, {output.OutputIndex}");
                        balance += output.Value;
                    }
                }

                foreach(var input in transaction.Inputs)
                {
                    // TODO: maybe do this better. see if you can rewrite blockchain class to work without a db too
                    var output = chain.First(b => b.BlockId == input.BlockId)[input.TransactionId].Outputs[input.OutputIndex];
                    Console.WriteLine($"inputs: {input.BlockId}, {input.TransactionId}, {input.OutputIndex}");
                    if(output.PublicKey == pubKeyString)
                    {
                        Console.WriteLine($"✅: {input.BlockId}, {input.TransactionId}, {input.OutputIndex}");
                        balance -= output.Value;
                    }
                }
            }
        };

        if(balance > int.MaxValue || balance < int.MinValue)
        {
            // TODO: Make this a real log
            Console.WriteLine("Balance is outside of int bound");
        }
        return (int)balance;
    }
    
}