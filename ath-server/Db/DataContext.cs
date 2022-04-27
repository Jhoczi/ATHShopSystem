using ath_server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ath_server.Db;

public class DataContext : IdentityDbContext
{
    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductInShop> ProductInShops { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDescription> OrdersDescriptions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(
                "Server=127.0.0.1;User ID=ath;Password=docker;Host=localhost;Port=5433;Database=ath-shop;Pooling=true;");
    } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInShop>()
            .HasKey(ps => new { ps.ProductId, ps.ShopId });

        modelBuilder.Entity<Shop>()
            .HasMany(p => p.Products);

        modelBuilder.Entity<Shop>()
            .HasKey(x=>x.Id);
        
        modelBuilder.Entity<Product>()
            .HasKey(pr => pr.Id);

        base.OnModelCreating(modelBuilder);
    }
}