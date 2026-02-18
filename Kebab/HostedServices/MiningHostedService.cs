
using System.Threading.Channels;
using Kebab.Data.Models;
using Kebab.Managers;
using Kebab.Models;

// wanna do something like this where the controller just adds the item to the queue https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio#queued-background-tasks

namespace Kebab.HostedServices;

public class MiningHostedService(IServiceProvider Services) : BackgroundService, IDisposable
{
    private IServiceProvider Services { get; } = Services;

    public async Task AddBlock(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // TODO: investigate how heavy this is as an operation. Might be fine to just keep the List / manager reference or maybe it is best practise to make them each time
            using (var scope = Services.CreateScope())
            {
                // var transactionRequestList =
                //     scope.ServiceProvider.GetRequiredKeyedService<List<TransactionRequest>>("transactionRequest");
                // if (transactionRequestList.Count == 0)
                // {
                //     Console.WriteLine("Transaction list empty, waiting 1 minute");
                // }
                // else
                // {
                    var manager =
                        scope.ServiceProvider
                            .GetRequiredService<BlockChainManager>();

                    manager.AddBlock();
                // }
                // If this were a competitive miner we wouldnt want to wait but I dont want my azure bill to bankrupt me
                await Task.Delay(TimeSpan.FromMinutes(1));

            }
        }
        return;

    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await AddBlock(cancellationToken);
        }
    }
}
