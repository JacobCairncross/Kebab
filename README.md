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

## Current issues
- Block hashes and nonces are wrong? Need to have their leading 0s
- Something i the transaction manager (line 118), its trying to get block id 0 (they start at 1), give it a check to see why
