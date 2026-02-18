using System.Net.Http;
using System.Text.Json.Serialization;
using Kebab.Data.Contexts;
using Kebab.Data.Models;
using Kebab.HostedServices;
using Kebab.Managers;
using Kebab.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

if (builder.Environment.IsDevelopment())
{
    builder.AddNpgsqlDbContext<AppDbContext>("postgresdb");
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        string password = $";Password={builder.Configuration["POSTGRES_PASSWORD"]}";
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("kebab"));
    });
}

// TODO: Big one, make interfaces for these
builder.Services.AddScoped<BlockChain>();
builder.Services.AddScoped<BlockChainManager>();
builder.Services.AddScoped<TransactionManager>();
builder.Services.AddKeyedSingleton<List<TransactionRequest>>("transactionRequest");
builder.Services.AddHostedService<MiningHostedService>();
builder.Services.AddHttpClient();
Options options = new(){
    GenesisPubKey= builder.Configuration.GetValue<string>("GenesisPubKey")
};
// Find out why singleton lets us do this but not scoped
builder.Services.AddSingleton<Options>(options);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapGet("/hi", () => "Hello!");

app.MapDefaultControllerRoute();
app.MapControllerRoute(
        name: "Default",
        pattern: "{controller}/{action}",
        defaults: new { controller = "Home", action = "Index" }
    );
app.MapRazorPages();

app.Run();