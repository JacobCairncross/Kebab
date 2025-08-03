## Running:
- This is using dotnet aspire, this means you should be able to run `dotnet run` in the Kebab.AppHost directory. Alternatively open vscode and hit f5 

## Common Issues
- PGAdmin might run on startup, if so you'll have trouble connecting to the docker db cause of port conflicts. Open task manager, search for postgres, and end the task before running the project

## THINGS TO DO 
- I suppose some unit tests wouldnt hurt, and maybe an integration test if you're feeling really masochistic
- actually put at least one server up somewhere
- In Client, get rid of TransactionController and merge with WalletController, keep the managers though
- put the client into this repo then rework to use dotnet aspire. Having a monorepo makes sense even if you dont aspire up
    - can maybe bring back the db connection string later? Would be good to wrap in a if(production) so we can reference a separate db in live environments
