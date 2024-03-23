﻿using System.Net.Http;
using Kebab.Managers;
using Kebab.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<BlockChainManager>();
builder.Services.AddSingleton<TransactionManager>();
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