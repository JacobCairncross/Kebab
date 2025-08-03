using Kebab.MigrationService;
using Kebab.Data.Contexts;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

// builder.AddSqlServerDbContext<TicketContext>("sqldata");
builder.AddNpgsqlDbContext<AppDbContext>("postgresdb");

var host = builder.Build();
host.Run();