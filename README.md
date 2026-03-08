## Running:
- This is using dotnet aspire, this means you should be able to run `dotnet run` in the Kebab.AppHost directory. Alternatively open vscode and hit f5. Ensure you have docker running.

## Common Issues
- PGAdmin might run on startup, if so you'll have trouble connecting to the docker db cause of port conflicts. Open task manager, search for postgres, and end the task before running the project

## THINGS TO DO 
- I suppose some unit tests wouldnt hurt, and maybe an integration test if you're feeling really masochistic
- actually put at least one server up somewhere
- In Client, get rid of TransactionController and merge with WalletController, keep the managers though
- put the client into this repo then rework to use dotnet aspire. Having a monorepo makes sense even if you dont aspire up
    - can maybe bring back the db connection string later? Would be good to wrap in a if(production) so we can reference a separate db in live environments
- implement results pattern (NET 10 disciminated unions) instead of using bools for success and failure
    - this isnt in 10 so we'll probs have to wait for 11 or maybe even 12 for them
    - A few libraries for results pattern, or you can make your own. Would be good to do
- turn client backend back into a pure api since we have react now

## Current issues
- Block hashes and nonces are wrong? Need to have their leading 0s
- seems to be an issue with handling multiple transactions in a block? May override eachother. Try sending multiple fast and only last one showed
    - Its because once you've spent the original input, you cant then spend it again, and need to wait for the next block to be made to send the next bit
    - is this fixable or just how it is?
        - Can maybe add a way on the front end to allow sending multiple at once, that way you can use the one input without having to wait?
- Can send transactions with a value of 0, these dont require inputs. This could allow network to be flooded with Spam. Make it so every transaction needs to have a value > 0. Maybe do some checking for negative numbers too
