using ath_server.Models;
namespace ath_server.Db;
public class Seeder
{
    public static async Task Seed(DataContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        if (context.Shops.Any())
        {
            return;
        }

        List<Product> products = new()
        {
            new()
            {
                //Id = 1,
                Name = "Coca Cola",
                Description = "Very sweet drink",
                Price = 3.30m,
            },
            new()
            {
                //Id = 2,
                Name = "Coca Cola Light",
                Description = "Very sweet drink with zero calories",
                Price = 3.50m,
            },
            new()
            {
                //Id = 3,
                Name = "Suszar X1.0",
                Description = "Super Suszar 1.0",
                Price = 120.00m
            },
            new()
            {
                //Id = 4,
                Name = "Suszar X2.0",
                Description = "Super Suszar 2.0",
                Price = 260.00m
            },
            new()
            {
                //Id = 5,
                Name = "Suszar X2.5",
                Description = "Super Suszar 2.5",
                Price = 275.00m
            },
            new()
            {
                //Id = 6,
                Name = "Soplica Biała 0.5",
                Description = "Polish vodka",
                Price = 26.00m,
            },
            new()
            {
                //Id = 7,
                Name = "Soplica Biała 0.7",
                Description = "Polish vodka",
                Price = 32.00m,
            },
            new()
            {
                //Id = 8,
                Name = "Żubrowka Biała",
                Description = "Polish vodka 0.5",
                Price = 21.00m,
            }
        };
        List<Shop> shops = new List<Shop>() 
        {
            new ()
            {
                Name = "HoczBud",
                Description = "Some description 1",
                Products = new List<ProductInShop>
                {
                    new()
                    {
                        
                        ProductId = 1,
                        PriceInShop = 5.00m
                    }
                }
            },
            new ()
            {
                Name = "SuszarX",
                Description = "Some description 2",
                Products = new List<ProductInShop>
                {
                    new()
                    {
                        ProductId = 1,
                        PriceInShop = 5.00m
                    }
                }
            },
            new ()
            {
                Name = "Soplicopol",
                Description = "Some description 3",
                Products = new List<ProductInShop>
                {
                    new()
                    {
                        ProductId = 1,
                        PriceInShop = 5.00m
                    }
                }
            }
        };

        await context.AddRangeAsync(products);
        await context.AddRangeAsync(shops);
        await context.SaveChangesAsync();
    }
}