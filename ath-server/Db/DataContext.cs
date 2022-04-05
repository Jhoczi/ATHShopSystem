﻿using ath_server.Models;
using Microsoft.EntityFrameworkCore;

namespace ath_server.Db;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductInShop> ProductInShops { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDescription> OrdersDescriptions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInShop>()
            .HasKey(ps => new { ps.ProductId, ps.ShopId });

        modelBuilder.Entity<Shop>()
            .HasMany(p => p.Products);

        modelBuilder.Entity<Product>()
            .HasKey(pr => pr.Id);
    }
}