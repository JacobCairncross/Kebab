using Kebab.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<TransactionInput> TransactionInputs { get; set; }
    public DbSet<TransactionOutput> TransactionOutputs { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Block> Blocks{ get; set; }
}