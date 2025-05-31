## THINGS TO DO
- implement QR code generating and scanning for easy reading of public keys
    - to make this work we probably want to rework client so that it can store the details for many users
    - this does want to be on the client, we want to keep the miner separate from user data. That way someone can host their own client for just them if they're more security concious 
- I suppose some unit tests wouldnt hurt, and maybe an integration test if you're feeling really masochistic
- actually put at least one server up somewhere
- In Client, get rid of TransactionController and merge with WalletController, keep the managers though
- put the client into this repo then rework to use dotnet aspire. Having a monorepo makes sense even if you dont aspire up

Running:
- docker compose up
log says database system shut down before the 'ready to accept connections', maybe check if its alive?

Running while testing:
- docker run -e POSTGRES_PASSWORD=S3cret -e POSTGRES_DB=kebab_miner -p 5432:5432 inited-db


-Notes:
- old connection: "DefaultConnection": "Host=127.0.0.1;Port=5432;Database=kebab_miner;;Username=user;Password=nYRlgCzHBsSeyrcI4p2e"
now



Common Issues
- PGAdmin might run on startup, if so you'll have trouble connecting to the docker db cause of port conflicts. Open task manager, search for postgres, and end the task before running the container
