var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgWeb();
var postgresdb = postgres.AddDatabase("postgresdb");

var migrations = builder.AddProject<Projects.Kebab_MigrationService>("migrations")
       .WithReference(postgresdb)
       .WaitFor(postgresdb);

var miner = builder.AddProject<Projects.kebab>("miner")
       .WithReference(postgresdb)
       .WithReference(migrations)
       .WaitForCompletion(migrations);
       // .WaitFor(postgresdb);

builder.AddProject<Projects.kebabClient>("client")
       .WithExternalHttpEndpoints()
       .WithReference(miner)
       .WaitFor(miner);

builder.Build().Run();